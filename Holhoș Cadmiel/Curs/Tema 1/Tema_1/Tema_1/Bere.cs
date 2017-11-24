using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1
{
    public class Bere
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public int Id_Berarie { get; set; }
        public string NumeBerarie { get; set; }
        public int StyleID { get; set; }
        public string StyleName { get; set; }
        public string StyleLink { get; set; }
        public string SelfLink { get; set; }
        public string ReviewLink { get; set; }
    }
}
