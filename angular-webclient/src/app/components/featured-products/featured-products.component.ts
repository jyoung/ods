import { Component, OnInit } from '@angular/core';
import { CatalogService } from '@core/services/catalog.service';
import { FeaturedProduct } from '@core/clients/catalog-client';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-featured-products',
  templateUrl: './featured-products.component.html',
  styleUrls: ['./featured-products.component.scss']
})
export class FeaturedProductsComponent implements OnInit {

  private catalogService: CatalogService;

  public products$: Observable<FeaturedProduct[]>;

  constructor(catalogService: CatalogService) {
    this.catalogService = catalogService;
  }

  ngOnInit(): void {
    this.getProducts();
  }

  private getProducts() {
    this.catalogService.getFeaturedProducts()
      .pipe(p => this.products$ = p);
  }
}
