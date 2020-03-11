import { Component, Input, OnInit } from '@angular/core';

import { CartService } from '@core/services/cart.service';
import { ProductDetail } from '@core/clients/catalog-client';

@Component({
  selector: 'app-product-detail-card',
  templateUrl: './product-detail-card.component.html',
  styleUrls: ['./product-detail-card.component.scss']
})
export class ProductDetailCardComponent implements OnInit {

  private cartService: CartService;

  @Input()
  product: ProductDetail;

  constructor(cartService: CartService) {
    this.cartService = cartService;
  }

  ngOnInit(): void {
  }

  public addToCart(): void {
    this.cartService.addToCart();
  }

  public addToWishlist(): void {
    alert('Add To Wishlist');
  }
}
