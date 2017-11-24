using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVTema1DATC
{
    class Beers
    {
        private int id;
        private string name;
        private int idBerarie;
        private string nameBerarie;
        private int idStil;
        private string nameStil;
        private string linkToBeer;
        private string linkToBrewerie;
        private string linkToStyle;
        private string linkToReview;

        public Beers(int id, string name, int idBerarie, string nameBerarie, int idStil, string nameStil, string linkToBeer, string linkToBrewerie, string linkToStyle, string linkToReview)
        {
            this.id = id;
            this.name = name;
            this.idBerarie = idBerarie;
            this.nameBerarie = nameBerarie;
            this.idStil = idStil;
            this.nameStil = nameStil;
            this.linkToBeer = linkToBeer;
            this.linkToBrewerie = linkToBrewerie;
            this.linkToStyle = linkToStyle;
            this.linkToReview = linkToReview;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int IdBerarie { get => idBerarie; set => idBerarie = value; }
        public string NameBerarie { get => nameBerarie; set => nameBerarie = value; }
        public string LinkToBeer { get => linkToBeer; set => linkToBeer = value; }
        public string LinkToBrewerie { get => linkToBrewerie; set => linkToBrewerie = value; }
        public string LinkToStyle { get => linkToStyle; set => linkToStyle = value; }
        public string LinkToReview { get => linkToReview; set => linkToReview = value; }
        public int IdStil { get => idStil; set => idStil = value; }
        public string NameStil { get => nameStil; set => nameStil = value; }
    }
}
