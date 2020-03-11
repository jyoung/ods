import { Component, Input, OnInit } from '@angular/core';

import { ProductDetail } from '@core/clients/catalog-client';

@Component({
  selector: 'app-product-images-card',
  templateUrl: './product-images-card.component.html',
  styleUrls: ['./product-images-card.component.scss']
})
export class ProductImagesCardComponent implements OnInit {

  @Input()
  product: ProductDetail;

  currentImage: string;

  constructor() { }

  ngOnInit(): void {
    this.currentImage = this.product.primaryImage.largeUrl;
  }

  public switchImage(event: Event): void {
    const img = event.target as Element;
    const src = 'src'; // keeps ts from complaining

    this.currentImage = img.attributes[src].value;
  }
}
