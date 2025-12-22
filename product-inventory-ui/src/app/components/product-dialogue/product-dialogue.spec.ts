import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductDialogue } from './product-dialogue';

describe('ProductDialogue', () => {
  let component: ProductDialogue;
  let fixture: ComponentFixture<ProductDialogue>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductDialogue]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductDialogue);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
