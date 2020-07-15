import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LsprComponent } from './lspr.component';

describe('LsprComponent', () => {
  let component: LsprComponent;
  let fixture: ComponentFixture<LsprComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LsprComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LsprComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
