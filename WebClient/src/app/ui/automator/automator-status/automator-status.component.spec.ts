import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomatorStatusComponent } from './automator-status.component';

describe('AutomatorStatusComponent', () => {
  let component: AutomatorStatusComponent;
  let fixture: ComponentFixture<AutomatorStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutomatorStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutomatorStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
