import { ComponentFixture, TestBed } from '@angular/core/testing';

import { XemthongtinchitietComponent } from './xemthongtinchitiet.component';

describe('XemthongtinchitietComponent', () => {
  let component: XemthongtinchitietComponent;
  let fixture: ComponentFixture<XemthongtinchitietComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [XemthongtinchitietComponent]
    });
    fixture = TestBed.createComponent(XemthongtinchitietComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
