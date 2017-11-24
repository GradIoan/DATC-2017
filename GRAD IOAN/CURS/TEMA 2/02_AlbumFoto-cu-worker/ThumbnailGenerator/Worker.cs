using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using Microsoft.WindowsAzure.Storage.Table;
using AlbumPhoto.Service.Entities;
using System.Configuration;

namespace ThumbnailGenerator
{
    public class Worker
    {

        private CloudStorageAccount _account;
        private CloudBlobClient _blobClient;
        private CloudBlobContainer _photoContainer;
        private CloudBlobContainer _thumbnailContainer;
        private CloudTableClient _tableClient;
        private CloudTable _filesTable;
        private TableServiceContext _ctx;

        
        public void Init()
        {
                _account = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
                _blobClient = _account.CreateCloudBlobClient();
                _photoContainer = _blobClient.GetContainerReference("poze");
                if (_photoContainer.CreateIfNotExists())
                {
                    _photoContainer.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });
                }
                _thumbnailContainer = _blobClient.GetContainerReference("thumbnail");
                if (_thumbnailContainer.CreateIfNotExists())
                {
                    _thumbnailContainer.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });
                }
                
                _tableClient = _account.CreateCloudTableClient();
                _filesTable = _tableClient.GetTableReference("files");
                _filesTable.CreateIfNotExists();
                _ctx = _tableClient.GetTableServiceContext();

                // For information on handling configuration changes
                // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
        }

		public void Process()
		{
			while (true)
			{
				var filesNoThumbnail = _ctx.CreateQuery<FileEntity>(_filesTable.Name)
										//.Where(fe => fe.ThumbnailUrl == null)
										.AsTableServiceQuery<FileEntity>(_ctx).ToList().Where(fe => fe.ThumbnailUrl == null).ToList();

				if (filesNoThumbnail.Count > 0)
				{
					foreach (var file in filesNoThumbnail)
					{

						var blob = _photoContainer.GetBlockBlobReference(file.RowKey);

						var imageStream = new MemoryStream();
						blob.DownloadToStream(imageStream);
						imageStream.Seek(0, SeekOrigin.Begin);

						//load and resize image 
						var thumbnail = LoadImageFromStream(imageStream);
						imageStream.Close();
						imageStream.Dispose();

						//save the thumbnail
						var thumbnailStream = SaveImageToStream(thumbnail);
						var thumbnailBlob = _thumbnailContainer.GetBlockBlobReference(file.RowKey);
						thumbnailBlob.UploadFromStream(thumbnailStream);
						thumbnailStream.Close();
						thumbnailStream.Dispose();

						file.ThumbnailUrl = thumbnailBlob.Uri.ToString();
						_ctx.UpdateObject(file);
						_ctx.SaveChangesWithRetries();
					}
				}
				else
				{
					break;
				}
			}
		}

        #region image utilities
        private MemoryStream SaveImageToStream(BitmapSource image)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();

            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        private BitmapSource LoadImageFromStream(Stream stream)
        {
            var decoder = new JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            BitmapSource img = decoder.Frames[0];
            img.Freeze();
            return ResizeImage(img, 130);
        }

        private BitmapSource ResizeImage(BitmapSource pSource, double pNewWidth)
        {

            //scale image
            double r = pNewWidth / pSource.Width;
            ScaleTransform tr = new ScaleTransform(r, r);
            TransformedBitmap scalledImg = new TransformedBitmap(pSource, tr);

            //write to memory
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            MemoryStream stream = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create(scalledImg as BitmapSource));
            encoder.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);

            //read back resized image
            BmpBitmapDecoder decoder = new BmpBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            BitmapSource img = decoder.Frames[0];

            stream.Close();
            stream.Dispose();
            if (img.CanFreeze) img.Freeze();

            return img;

        }
        #endregion image utilities
    }
}
