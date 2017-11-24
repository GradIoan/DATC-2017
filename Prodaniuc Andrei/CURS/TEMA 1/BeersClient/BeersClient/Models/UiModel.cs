using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeersClient.Models
{
    public class UiModel
    {
        public List<Brewery> Breweries { get; set; }
        public Brewery Brewery { get; set; }
        public List<Beer> Beers{ get; set; }
        public Beer Beer { get; set; }
        public Level Level{ get; set; }

    }

    public enum Level
    {
        None,
        Breweries,
        Brewery,
        Beers,
        Beer,
        Add
    };
}
