import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageformComponent } from './manageform.component';

describe('ManageformComponent', () => {
  let component: ManageformComponent;
  let fixture: ComponentFixture<ManageformComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageformComponent]
    });
    fixture = TestBed.createComponent(ManageformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
