import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DangkigroupComponent } from './dangkigroup.component';

describe('DangkigroupComponent', () => {
  let component: DangkigroupComponent;
  let fixture: ComponentFixture<DangkigroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DangkigroupComponent]
    });
    fixture = TestBed.createComponent(DangkigroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
