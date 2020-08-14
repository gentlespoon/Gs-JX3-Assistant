import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomatorDYComponent } from './automator-dy.component';

describe('AutomatorDYComponent', () => {
  let component: AutomatorDYComponent;
  let fixture: ComponentFixture<AutomatorDYComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutomatorDYComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutomatorDYComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
