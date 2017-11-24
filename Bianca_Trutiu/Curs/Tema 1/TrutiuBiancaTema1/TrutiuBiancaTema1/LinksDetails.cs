using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrutiuBiancaTema1
{
    public class LinksDetails
    {
        public Self self;
        public Self style;
        public Self brewery;

        public LinksDetails()
        {
            self = new Self();
            style = new Self();
            brewery = new Self();
        }
    }
}
