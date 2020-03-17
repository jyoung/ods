import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomePageComponent } from './pages/home-page/home-page.component';
import { CatalogPageComponent } from './pages/catalog-page/catalog-page.component';


const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'catalog', component: CatalogPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
