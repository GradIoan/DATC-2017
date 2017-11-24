using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class beersInfo
    {
public class Self
{
    public string href { get; set; }
}

public class Beers
{
    public string href { get; set; }
}

public class Links
{
    public Self self { get; set; }
    public Beers beers { get; set; }
}

public class RootObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Links _links { get; set; }
}
    }
}
