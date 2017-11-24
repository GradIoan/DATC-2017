using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1___DATC
{
    class Breweries
     {
         private int id;
         private string name;
         private string linkToBeers;
         private string linkToBrewery;
 
         public Breweries(int id, string name, string linkToBeers, string linkToBrewery)
         {
             this.id = id;
             this.name = name;
             this.linkToBeers = linkToBeers;
             this.linkToBrewery = linkToBrewery;
         }
 
         public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public string LinkToBeers
        {
            get
            {
                return this.linkToBeers;
            }
            set
            {
                this.linkToBeers = value;
            }
        }
        public string LinkToBrewery
        {
            get
            {
                return this.linkToBrewery;
            }
            set
            {
                this.linkToBrewery = value;
            }
        }
       
     }
}
