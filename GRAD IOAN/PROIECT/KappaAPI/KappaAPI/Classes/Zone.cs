using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KappaAPI.Classes
{
    public class Zone
    {
        [JsonProperty("Zona")]
        public int Zona { get; set; }

        [JsonProperty("X1")]
        public int X1 { get; set; }

        [JsonProperty("Y1")]
        public int Y1 { get; set; }

        [JsonProperty("X2")]
        public int X2{ get; set; }

        [JsonProperty("Y2")]
        public int Y2 { get; set; }
    }
}
