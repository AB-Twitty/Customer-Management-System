import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerBarComponent } from './customer-bar.component';

describe('CustomerBarComponent', () => {
  let component: CustomerBarComponent;
  let fixture: ComponentFixture<CustomerBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerBarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomerBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
