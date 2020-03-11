import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { ProductService } from '@core/services/product.service';
import { ProductDetail } from '@core/clients/catalog-client';

@Component({
  selector: 'app-product-detail-page',
  templateUrl: './product-detail-page.component.html',
  styleUrls: ['./product-detail-page.component.scss']
})
export class ProductDetailPageComponent implements OnInit {

  private route: ActivatedRoute;
  private productService: ProductService;

  public product$: Observable<ProductDetail>;

  constructor(route: ActivatedRoute, productService: ProductService) {
    this.route = route;
    this.productService = productService;
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  private loadProduct() {
    this.product$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => this.productService.getById(+params.get('id')))
    );
  }
}
