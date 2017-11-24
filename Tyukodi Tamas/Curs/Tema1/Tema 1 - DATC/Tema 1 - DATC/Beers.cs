using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1___DATC
{
    class Beers
    {
        private int id;
        private string name;
        private int idBrewery;
        private string nameBrewery;
        private int idStyle;
        private string nameStyle;
        private string linkToBeer;
        private string linkToBrewery;
        private string linkToStyle;
        private string linkToReview;

        public Beers(int id, string name, int idBrewery, string nameBrewery, int idStyle, string nameStyle, string linkToBeer, string linkToBrewery, string linkToStyle, string linkToReview)
        {
            this.id = id;
            this.name = name;
            this.idBrewery = idBrewery;
            this.nameBrewery = nameBrewery;
            this.idStyle = idStyle;
            this.nameStyle = nameStyle;
            this.linkToBeer = linkToBeer;
            this.linkToBrewery = linkToBrewery;
            this.linkToStyle = linkToStyle;
            this.linkToReview = linkToReview;
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
        public int IdBrewery
        {
            get
            {
                return this.idBrewery;
            }
            set
            {
                this.idBrewery = value;
            }
        }
        public string NameBrewery
        {
            get
            {
                return this.nameBrewery;
            }
            set
            {
                this.nameBrewery = value;
            }
        }
        public int IdStyle
        {
            get
            {
                return this.idStyle;
            }
            set
            {
                this.idStyle = value;
            }
        }
        public string NameStyle
        {
            get
            {
                return this.nameStyle;
            }
            set
            {
                this.nameStyle = value;
            }
        }
        public string LinkToBeer
        {
            get
            {
                return this.linkToBeer;
            }
            set
            {
                this.linkToBeer = value;
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
        public string LinkToReview
        {
            get
            {
                return this.linkToReview;
            }
            set
            {
                this.linkToReview = value;
            }
        }

     

    }
}
