using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;

namespace rest_API_DATC
{
    public enum httpVerb
    {
        GET,
        POST, 
        PUT, 
        DELETE
    }

    class RESTClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod{ get; set; }

        public RESTClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }

        public string makeRequest()
        {
            string strResponseValue = string.Empty;
            HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create(endPoint);
            newRequest.Accept = "application/hal+json";
            newRequest.Method = httpMethod.ToString();

            using (HttpWebResponse newResponse = (HttpWebResponse)newRequest.GetResponse())
            {

                if (newResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("Response error: " + newResponse.StatusCode);
                }
                // Process the response stream (JSON)

                using (Stream responseStream = newResponse.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        } // End of StreamReader
                    }
                } // End of using ResponseStream
            }

                return strResponseValue;
        }
    }
}
