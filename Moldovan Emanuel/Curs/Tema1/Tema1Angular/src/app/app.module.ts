import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import { AlertModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { NavbarComponent } from './navbar/navbar.component';
import { BeerService } from './services/beer.service';
import { HttpModule, JsonpModule } from '@angular/http';

const appRoutes: Routes = [
  {path: '', component: HomePageComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    RouterModule.forRoot(appRoutes),
    AlertModule.forRoot(),
    JsonpModule
  ],
  providers: [BeerService],
  bootstrap: [AppComponent]
})
export class AppModule { }
