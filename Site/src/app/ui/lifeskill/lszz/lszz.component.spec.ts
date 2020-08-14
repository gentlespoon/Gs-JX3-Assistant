import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LszzComponent } from './lszz.component';

describe('LszzComponent', () => {
  let component: LszzComponent;
  let fixture: ComponentFixture<LszzComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LszzComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LszzComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
