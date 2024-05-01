import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationHiringComponent } from './notification-hiring.component';

describe('NotificationHiringComponent', () => {
  let component: NotificationHiringComponent;
  let fixture: ComponentFixture<NotificationHiringComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NotificationHiringComponent]
    });
    fixture = TestBed.createComponent(NotificationHiringComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
