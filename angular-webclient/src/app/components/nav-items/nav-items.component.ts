import { Component, OnInit } from '@angular/core';
import { CatalogService } from '@core/services/catalog.service';
import { CategoryTreeItem } from '@core/clients/catalog-client';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav-items',
  templateUrl: './nav-items.component.html',
  styleUrls: ['./nav-items.component.scss']
})
export class NavItemsComponent implements OnInit {

  private catalog: CatalogService;

  public categories$: Observable<CategoryTreeItem[]>;

  constructor(catalog: CatalogService) {
    this.catalog = catalog;
  }

  ngOnInit(): void {
    this.getCategories();
  }

  private getCategories() {
    this.catalog.getCategories()
      .pipe(c => this.categories$ = c);
  }
}
