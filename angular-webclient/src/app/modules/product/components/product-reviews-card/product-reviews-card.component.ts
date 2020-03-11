import { Component, Input, OnInit } from '@angular/core';

import { ProductDetail } from '@core/clients/catalog-client';

@Component({
  selector: 'app-product-reviews-card',
  templateUrl: './product-reviews-card.component.html',
  styleUrls: ['./product-reviews-card.component.scss']
})
export class ProductReviewsCardComponent implements OnInit {

  @Input()
  product: ProductDetail;

  constructor() { }

  ngOnInit(): void {
  }

}
