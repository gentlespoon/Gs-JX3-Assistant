import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomatorComponent } from './automator.component';

describe('AutomatorComponent', () => {
  let component: AutomatorComponent;
  let fixture: ComponentFixture<AutomatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutomatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutomatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
