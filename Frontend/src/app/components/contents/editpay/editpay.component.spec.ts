import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditpayComponent } from './editpay.component';

describe('EditpayComponent', () => {
  let component: EditpayComponent;
  let fixture: ComponentFixture<EditpayComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditpayComponent]
    });
    fixture = TestBed.createComponent(EditpayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
