using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVTema1DATC
{
    class Breweries
    {
        private int id;
        private string name;
        private string linkToBeers;
        private string linkToBrewerie;

        public Breweries(int id, string name, string linkToBeers, string linkToBrewerie)
        {
            this.id = id;
            this.name = name;
            this.linkToBeers = linkToBeers;
            this.linkToBrewerie = linkToBrewerie;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string LinkToBeers { get => linkToBeers; set => linkToBeers = value; }
        public string LinkToBrewerie { get => linkToBrewerie; set => linkToBrewerie = value; }
    }
}
