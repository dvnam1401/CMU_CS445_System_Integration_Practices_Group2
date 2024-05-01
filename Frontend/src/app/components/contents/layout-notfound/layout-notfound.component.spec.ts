import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutNotfoundComponent } from './layout-notfound.component';

describe('LayoutNotfoundComponent', () => {
  let component: LayoutNotfoundComponent;
  let fixture: ComponentFixture<LayoutNotfoundComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LayoutNotfoundComponent]
    });
    fixture = TestBed.createComponent(LayoutNotfoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
