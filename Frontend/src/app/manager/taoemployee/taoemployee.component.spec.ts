import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaoemployeeComponent } from './taoemployee.component';

describe('TaoemployeeComponent', () => {
  let component: TaoemployeeComponent;
  let fixture: ComponentFixture<TaoemployeeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TaoemployeeComponent]
    });
    fixture = TestBed.createComponent(TaoemployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
