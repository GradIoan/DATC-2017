using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//CLASS DEFINED TO CREATE A LIST OF OBJECTS

namespace TemaDATC
{
    class Brewery
    {
        public int _id;
        public string _name;
        public string _link;
        public Brewery(int _id,string _name, string _link)
        {
            this._id = _id;
            this._name = _name;
            this._link = _link;
        }
    }
}
