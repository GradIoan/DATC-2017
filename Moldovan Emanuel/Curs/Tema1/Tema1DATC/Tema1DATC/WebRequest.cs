using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tema1DATC.Models;

namespace Tema1DATC
{
    class WebRequest
    {
        public static string Get(string url)
        {
            const string WEBSERVICE_URL = "http://datc-rest.azurewebsites.net";
            try
            {
                var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL + url);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 12000;
                    webRequest.ContentType = "application/hal+json";

                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                            return jsonResponse;
                        }
                    }
                }
                return null; ;
            }
            catch (Exception ex)
            {                
                return null;
            }
        }        
        public static bool Post(string url, string numeBere)
        {
            const string WEBSERVICE_URL = "http://datc-rest.azurewebsites.net";
            try
            {
                var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL + url);
                if (webRequest != null)
                {
                    webRequest.Method = "Post";
                    webRequest.ContentType = "application/json";

                    using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        string json = "{\"Name\":\"" + numeBere + "\"}";

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)webRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        if(result == "")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                
                return false; ;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
