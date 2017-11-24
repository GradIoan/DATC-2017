import { Component, OnInit } from '@angular/core';
import { BeerService } from "../services/beer.service";
import { IBrewery } from "../interfaces/IBrewery";

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
    breweries: IBrewery = {} as IBrewery;

    constructor(private _beerService: BeerService) {
        this.getBreweries();
    }

    getBreweries() {
        this._beerService.getBreweries().subscribe((breweries: IBrewery) => {
            this.breweries = breweries;
            console.log(this.breweries);
        }, error => console.log(error));
    }

    getBrewery(url: string) {
        this._beerService.getBrewery(url).subscribe((breweries) => {
            return breweries;            
        }, error => console.log(error));
    }

  ngOnInit() {
  }

}
