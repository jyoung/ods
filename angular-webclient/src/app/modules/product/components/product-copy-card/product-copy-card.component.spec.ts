import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCopyCardComponent } from './product-copy-card.component';

describe('ProductCopyCardComponent', () => {
  let component: ProductCopyCardComponent;
  let fixture: ComponentFixture<ProductCopyCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductCopyCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductCopyCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
