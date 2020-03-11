import { Component, Input, OnInit } from '@angular/core';

import { ProductDetail } from '@core/clients/catalog-client';

@Component({
  selector: 'app-product-copy-card',
  templateUrl: './product-copy-card.component.html',
  styleUrls: ['./product-copy-card.component.scss']
})
export class ProductCopyCardComponent implements OnInit {

  @Input()
  product: ProductDetail;

  constructor() { }

  ngOnInit(): void {
  }

}
