using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KappaAPI.Classes
{
    public class Date
    {
        [JsonProperty("Zona")]
        public int Zona { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Valoare")]
        public double Valoare { get; set; }

        [JsonProperty("Data")]
        public DateTime Data { get; set; }
    }
}
