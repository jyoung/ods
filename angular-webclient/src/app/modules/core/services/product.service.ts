import { Injectable } from '@angular/core';
import { Observable, throwError, of, } from 'rxjs';
import { CatalogClient, ProductDetail } from '@core/clients/catalog-client';
import { ObservableStore } from '@codewithdan/observable-store';
import { map, catchError } from 'rxjs/operators';

export interface ProductState {
  product: ProductDetail;
}

export enum ProductStoreActions {
  InitState = 'INIT_STATE',
  Load = 'LOAD_PRODUCT'
}

@Injectable({
  providedIn: 'root'
})
export class ProductService extends ObservableStore<ProductState> {

  private catalog: CatalogClient;

  constructor(client: CatalogClient) {
    super({ trackStateHistory: true, logStateChanges: true });

    const initialState = {
      product: null
    };

    this.setState(initialState, ProductStoreActions.InitState);
    this.catalog = client;
  }

  public getById(id: number): Observable<ProductDetail> {
    const product = this.getState().product;
    if (product && product.id === id) {
      return of(product);
    } else {
      return this.loadProduct(id);
    }
  }

  private loadProduct(id: number): Observable<ProductDetail> {
    return this.catalog.products(id.toString(), '1').pipe(map(p => {
      this.setState({ product: p }, ProductStoreActions.Load);
      return p;
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
