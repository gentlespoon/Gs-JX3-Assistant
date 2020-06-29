import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomatorSetupComponent } from './automator-setup.component';

describe('AutomatorSetupComponent', () => {
  let component: AutomatorSetupComponent;
  let fixture: ComponentFixture<AutomatorSetupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutomatorSetupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutomatorSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
