using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrutiuBiancaTema1
{
    public class Self
    {
        private int id;
        private string name;
        private string href;

        public string Href { get => href; set => href = value; }
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
    }
}
