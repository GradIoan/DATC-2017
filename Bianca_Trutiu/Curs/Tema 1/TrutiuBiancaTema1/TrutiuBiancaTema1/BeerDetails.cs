using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrutiuBiancaTema1
{
    public class BeerDetails
    {
        public LinksDetails _links;
        public Style style;
        int id;
        string name;
        int breweryid;
        string breweryname;
        string stylename;
        int styleid;
       
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Breweryid { get => breweryid; set => breweryid = value; }
        public string Breweryname { get => breweryname; set => breweryname = value; }
        public string Stylename { get => stylename; set => stylename = value; }
        public int Styleid { get => styleid; set => styleid = value; }      
        public Style Style { get => style; set => style = value; }        
        public LinksDetails Links { get => _links; set => _links = value; }

        public BeerDetails()
        {
            Links = new LinksDetails();
            style = new Style();            
        }
    }
}
