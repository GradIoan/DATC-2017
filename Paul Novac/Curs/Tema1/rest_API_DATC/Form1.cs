using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;

namespace rest_API_DATC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RESTClient restClient = new RESTClient();
            
            restClient.endPoint = "http://datc-rest.azurewebsites.net/breweries";

            debugOutput("Rest Client Created!");

            string strResponse = string.Empty;
            strResponse = restClient.makeRequest();
            debugOutput(strResponse);
        }

        private void debugOutput(string strDebugText)
        {
            try
            {
                System.Diagnostics.Debug.Write(strDebugText + Environment.NewLine);
                txtResponse.Text = txtResponse.Text + strDebugText + Environment.NewLine;
                txtResponse.SelectionStart = txtResponse.TextLength;
                txtResponse.ScrollToCaret();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message, ToString() + Environment.NewLine);
            }
        }

        async static void PostRequest(string url, string bere)
        {
            
            IEnumerable<KeyValuePair<string, string>> querries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("query1", bere),
                //new KeyValuePair<string, string>("query2", "jamalyca")
            };
        HttpContent q = new FormUrlEncodedContent(querries);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string myContent = await content.ReadAsStringAsync();
                        HttpContentHeaders headers = content.Headers;
                        System.Diagnostics.Debug.Write(myContent + Environment.NewLine + Environment.NewLine);
                    }
                }
            }
            
        }

    private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // void
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string denumBere = txtBere.Text;

            // PostRequest("http://datc-rest.azurewebsites.net/beers", denumBere);

            PostRequest("http://posttestserver.com/post.php", denumBere);
        }
    }
}
