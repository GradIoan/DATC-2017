using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrutiuBiancaTema1
{
    public class Links
    {
        public Self self;
        public Self brewery;
        public Beers beers;
        public Style style;
        
        public Links()
        {
            self = new Self();
            brewery = new Self();
            beers = new Beers();
            style = new Style();
        }
    }
}
