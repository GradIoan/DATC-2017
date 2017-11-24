using Microsoft.WindowsAzure.Storage.Table.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbumPhoto.Service.Entities
{
    public class FileEntity:TableServiceEntity
    {
        public FileEntity()
        {

        }

        public FileEntity(string userName, string fileName)
        {
            this.PartitionKey = userName;
            this.RowKey = fileName;
        }

        public long Size { get; set; }
        public DateTime PublishDate { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Url { get; set; }
    }
}