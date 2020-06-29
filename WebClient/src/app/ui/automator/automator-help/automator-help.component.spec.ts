import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomatorHelpComponent } from './automator-help.component';

describe('AutomatorHelpComponent', () => {
  let component: AutomatorHelpComponent;
  let fixture: ComponentFixture<AutomatorHelpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutomatorHelpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutomatorHelpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
