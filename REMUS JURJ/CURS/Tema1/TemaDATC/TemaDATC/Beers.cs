using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//CLASS DEFINED TO CREATE A LIST OF OBJECTS

namespace TemaDATC
{
    class Beers
    {
        public string _brewery;
        public string _beer;
        public int Id;
        public string Name;
        public Beers(string _brewery, string _beer)
        {
            this._brewery = _brewery;
            this._beer = _beer;
        }
        public Beers(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
    }
}
