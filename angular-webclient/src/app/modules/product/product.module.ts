import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductDetailPageComponent } from './pages/product-detail-page/product-detail-page.component';
import { ProductDetailCardComponent } from './components/product-detail-card/product-detail-card.component';
import { ProductImagesCardComponent } from './components/product-images-card/product-images-card.component';
import { ProductCopyCardComponent } from './components/product-copy-card/product-copy-card.component';
import { ProductReviewsCardComponent } from './components/product-reviews-card/product-reviews-card.component';


@NgModule({
  declarations: [
    ProductDetailPageComponent,
    ProductDetailCardComponent,
    ProductImagesCardComponent,
    ProductCopyCardComponent,
    ProductReviewsCardComponent],
  imports: [
    CommonModule,
    ProductRoutingModule
  ]
})
export class ProductModule { }
