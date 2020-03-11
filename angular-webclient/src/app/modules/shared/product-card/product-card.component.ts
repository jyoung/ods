import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input()
  id: string;

  @Input()
  smallUrl: string;

  @Input()
  title: string;

  @Input()
  shortDescription: string;

  @Input()
  price: number;

  constructor() { }

  ngOnInit(): void {
  }

}
