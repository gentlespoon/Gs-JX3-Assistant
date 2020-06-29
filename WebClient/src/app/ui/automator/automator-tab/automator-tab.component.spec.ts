import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomatorTabComponent } from './automator-tab.component';

describe('AutomatorTabComponent', () => {
  let component: AutomatorTabComponent;
  let fixture: ComponentFixture<AutomatorTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutomatorTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutomatorTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
