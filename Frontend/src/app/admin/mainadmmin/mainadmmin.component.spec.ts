import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainadmminComponent } from './mainadmmin.component';

describe('MainadmminComponent', () => {
  let component: MainadmminComponent;
  let fixture: ComponentFixture<MainadmminComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MainadmminComponent]
    });
    fixture = TestBed.createComponent(MainadmminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
