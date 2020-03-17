import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './modules/shared/shared.module';
import { CoreModule } from './modules/core/core.module';
import { ProductModule } from './modules/product/product.module';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { TopBarComponent } from './components/top-bar/top-bar.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavItemsComponent } from './components/nav-items/nav-items.component';
import { FeaturedProductsComponent } from './components/featured-products/featured-products.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { CartComponent } from './components/cart/cart.component';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { CatalogPageComponent } from './pages/catalog-page/catalog-page.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    TopBarComponent,
    FooterComponent,
    NavItemsComponent,
    FeaturedProductsComponent,
    HomePageComponent,
    CartComponent,
    SearchBarComponent,
    CatalogPageComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    SharedModule,
    CoreModule,
    ProductModule,
    AppRoutingModule,
    NgbModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
