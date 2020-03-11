import { Component, OnInit } from '@angular/core';

import { CartService } from '@core/services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  private cartService: CartService;

  itemCount = 0;

  constructor(cartService: CartService) {
    this.cartService = cartService;
  }

  ngOnInit(): void {
    this.cartService.itemCount$.subscribe(x => {
      this.itemCount += x;
    });
  }

}
