import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaoaccountComponent } from './taoaccount.component';

describe('TaoaccountComponent', () => {
  let component: TaoaccountComponent;
  let fixture: ComponentFixture<TaoaccountComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TaoaccountComponent]
    });
    fixture = TestBed.createComponent(TaoaccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
