using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrutiuBiancaTema1
{
    public class BeweryDetails
    {
        int id;
        string name;
        public Links _links { get; set; }
        string href;

        public BeweryDetails()
        {
            _links = new Links();
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Href { get => href; set => href = value; }
    }
}
