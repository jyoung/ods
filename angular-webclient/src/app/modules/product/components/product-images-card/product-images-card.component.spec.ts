import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductImagesCardComponent } from './product-images-card.component';

describe('ProductImagesCardComponent', () => {
  let component: ProductImagesCardComponent;
  let fixture: ComponentFixture<ProductImagesCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductImagesCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductImagesCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
