import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LsysComponent } from './lsys.component';

describe('LsysComponent', () => {
  let component: LsysComponent;
  let fixture: ComponentFixture<LsysComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LsysComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LsysComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
