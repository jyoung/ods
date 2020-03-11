import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private itemCount: BehaviorSubject<number> = new BehaviorSubject<number>(0);

  readonly itemCount$ = this.itemCount.asObservable();

  constructor() { }

  addToCart(): void {
    this.itemCount.next(1);
  }
}
