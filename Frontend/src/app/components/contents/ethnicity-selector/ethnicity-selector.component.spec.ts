import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EthnicitySelectorComponent } from './ethnicity-selector.component';

describe('EthnicitySelectorComponent', () => {
  let component: EthnicitySelectorComponent;
  let fixture: ComponentFixture<EthnicitySelectorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EthnicitySelectorComponent]
    });
    fixture = TestBed.createComponent(EthnicitySelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
