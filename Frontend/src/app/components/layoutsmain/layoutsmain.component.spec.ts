import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutsmainComponent } from './layoutsmain.component';

describe('LayoutsmainComponent', () => {
  let component: LayoutsmainComponent;
  let fixture: ComponentFixture<LayoutsmainComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LayoutsmainComponent]
    });
    fixture = TestBed.createComponent(LayoutsmainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
