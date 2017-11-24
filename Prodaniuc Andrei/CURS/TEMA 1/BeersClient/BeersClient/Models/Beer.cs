using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeersClient.Models
{
    public class Beer
    {
        public int Id { get; protected set; }
        public string Name { get; set; }
        public BeerStyle Style { get; set; }
        public Brewery Brewery { get; set; }
    }

    public partial class Beers
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
