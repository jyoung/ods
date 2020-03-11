import { Injectable } from '@angular/core';
import { Observable, of, throwError, } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { CatalogClient, FeaturedProduct } from '@core/clients/catalog-client';
import { ObservableStore } from '@codewithdan/observable-store';

export interface CatalogState {
  featuredProducts: FeaturedProduct[];
}

export enum CatalogActions {
  InitState = 'INIT_STATE',
  Load = 'LOAD_FEATURED_PRODUCTS'
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

  private loadFeaturedProducts(): Observable<FeaturedProduct[]> {
    return this.catalog.featuredproducts('1').pipe(map(fp => {
      this.setState({ featuredProducts: fp }, CatalogActions.Load);
      return fp;
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
