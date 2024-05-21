import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HosonhanvienComponent } from './hosonhanvien.component';

describe('HosonhanvienComponent', () => {
  let component: HosonhanvienComponent;
  let fixture: ComponentFixture<HosonhanvienComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [HosonhanvienComponent]
    });
    fixture = TestBed.createComponent(HosonhanvienComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
