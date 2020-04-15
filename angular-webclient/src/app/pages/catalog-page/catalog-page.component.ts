import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CatalogService } from '@app/modules/core/services/catalog.service';
import { FeaturedProduct } from '@app/modules/core/clients/catalog-client';

@Component({
  selector: 'app-catalog-page',
  templateUrl: './catalog-page.component.html',
  styleUrls: ['./catalog-page.component.scss']
})
export class CatalogPageComponent implements OnInit {

  private catalogService: CatalogService;

  public products$: Observable<FeaturedProduct[]>;
  public showSideBar = true;

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
