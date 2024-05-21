import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutmanageComponent } from './layoutmanage.component';

describe('LayoutmanageComponent', () => {
  let component: LayoutmanageComponent;
  let fixture: ComponentFixture<LayoutmanageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LayoutmanageComponent]
    });
    fixture = TestBed.createComponent(LayoutmanageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
