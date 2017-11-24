import { Injectable } from '@angular/core';
import { Http, Headers, Response, Jsonp } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { IBrewery } from "../interfaces/IBrewery";

@Injectable()
export class BeerService {
    apiRoot: string = 'http://datc-rest.azurewebsites.net/breweries';

    constructor(private _http: Http, private jsonp: Jsonp) { }

    getBreweries(): Observable<IBrewery> {
        let apiURL = this.apiRoot;
    const headers = new Headers();
    headers.append('Accept', 'application/hal+json');
    return this._http.get(apiURL, { headers: headers })
        .map((res: Response) => <IBrewery>res.json())
        .catch(this.handleError);
    }

    getBrewery(url: string): Observable<IBrewery> {
        let apiURL = this.apiRoot;
        const headers = new Headers();
        headers.append('Accept', 'application/hal+json');
        return this._http.get('http://datc-rest.azurewebsites.net' + url, { headers: headers })
            .map((res: Response) => res.json())
            .catch(this.handleError);
    }

  private handleError(error: Response) {
      console.log(error);
      return Observable.throw(error.json().error || 'Server error');
  }
}
