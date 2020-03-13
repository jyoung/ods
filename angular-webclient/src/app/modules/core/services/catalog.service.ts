import { Injectable } from '@angular/core';
import { Observable, of, throwError, } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { CatalogClient, FeaturedProduct, CategoryTreeItem } from '@core/clients/catalog-client';
import { ObservableStore } from '@codewithdan/observable-store';

export interface CatalogState {
  featuredProducts: FeaturedProduct[];
  categories: CategoryTreeItem[];
}

export enum CatalogActions {
  InitState = 'INIT_STATE',
  LoadFeaturedProducts = 'LOAD_FEATURED_PRODUCTS',
  LoadCategories = 'LOAD_CATEGORIES'
}

@Injectable({
  providedIn: 'root'
})
export class CatalogService extends ObservableStore<CatalogState> {

  private catalog: CatalogClient;

  constructor(catalog: CatalogClient) {
    super({ trackStateHistory: true, logStateChanges: true });

    const initialState = {
      featuredProducts: null
    };

    this.setState(initialState, CatalogActions.InitState);
    this.catalog = catalog;
  }

  public getFeaturedProducts(): Observable<FeaturedProduct[]> {
    const products = this.getState().featuredProducts;
    if (products) {
      return of(products);
    } else {
      return this.loadFeaturedProducts();
    }
  }

  public getCategories(): Observable<CategoryTreeItem[]> {
    const categories = this.getState().categories;
    if (categories) {
      return of(categories);
    } else {
      return this.loadCategories();
    }
  }

  private loadFeaturedProducts(): Observable<FeaturedProduct[]> {
    return this.catalog.featuredproducts('1').pipe(map(fp => {
      this.setState({ featuredProducts: fp }, CatalogActions.LoadFeaturedProducts);
      return fp;
    }), catchError(this.handleError));
  }

  private loadCategories(): Observable<CategoryTreeItem[]> {
    return this.catalog.categories('1').pipe(map(c => {
      this.setState({ categories: c }, CatalogActions.LoadCategories);
      return c;
    }), catchError(this.handleError));
  }

  private handleError(error: any) {
    console.error('server error:', error);
    if (error.error instanceof Error) {
      const errMessage = error.error.message;
      return throwError(errMessage);
    }
    return throwError(error || 'Server error');
  }
}
