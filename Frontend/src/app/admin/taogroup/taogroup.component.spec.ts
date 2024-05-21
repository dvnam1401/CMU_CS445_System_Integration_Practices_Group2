import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaogroupComponent } from './taogroup.component';

describe('TaogroupComponent', () => {
  let component: TaogroupComponent;
  let fixture: ComponentFixture<TaogroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TaogroupComponent]
    });
    fixture = TestBed.createComponent(TaogroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
