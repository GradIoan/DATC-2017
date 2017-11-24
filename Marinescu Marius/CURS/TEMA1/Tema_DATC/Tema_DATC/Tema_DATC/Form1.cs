using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Tema_DATC
{
    public partial class Form1 : Form
    {
        String href = "http://datc-rest.azurewebsites.net/breweries";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String jsonData;
            String singleParsed = "";
            String finalParsed = "";
            using(var client = new WebClient())
            {
                client.Headers.Add("Accept: application/hal+json");
                client.Headers.Add("Content-type: application/json");
                jsonData = client.DownloadString(href);
        
                Newtonsoft.Json.Linq.JObject o = Newtonsoft.Json.Linq.JObject.Parse(jsonData);

                var jPerson = JsonConvert.DeserializeObject<dynamic>(jsonData);
         

                foreach(var obj in jPerson._embedded.brewery)
                {
                    singleParsed = singleParsed + "Id: " + (string)obj.SelectToken("Id") + "\n" +
                        "Name: " + (string)obj.SelectToken("Name") + "\n" + "\n";

                    finalParsed = finalParsed + singleParsed + "\n";

                }
          
                richTextBox1.Text = singleParsed;
              
            }
        }
    }
}

