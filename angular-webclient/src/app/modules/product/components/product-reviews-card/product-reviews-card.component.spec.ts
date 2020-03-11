import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductReviewsCardComponent } from './product-reviews-card.component';

describe('ProductReviewsCardComponent', () => {
  let component: ProductReviewsCardComponent;
  let fixture: ComponentFixture<ProductReviewsCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductReviewsCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductReviewsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
