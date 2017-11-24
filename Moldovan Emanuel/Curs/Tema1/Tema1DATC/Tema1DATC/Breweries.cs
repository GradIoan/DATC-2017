using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema1DATC.Models;

namespace Tema1DATC
{
    class Breweries
    {
        public BreweriesModel breweries { get; set; }
        public BreweryModel brewery { get; set; }
        public BeersModel beers { get; set; }
        public BeerModel beer { get; set; }
        public void GetBreweries()
        {
            this.breweries = new BreweriesModel(WebRequest.Get("/breweries"));
        }

        public void GetBrewery(string url)
        {
            this.brewery = new BreweryModel(WebRequest.Get(url));
        }

        public void GetBeers(string url)
        {
            this.beers = new BeersModel(WebRequest.Get(url));
        }

        public void GetBeer(string url)
        {
            this.beer = new BeerModel(WebRequest.Get(url));
        }
    }
}
