import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LsfrComponent } from './lsfr.component';

describe('LsfrComponent', () => {
  let component: LsfrComponent;
  let fixture: ComponentFixture<LsfrComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LsfrComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LsfrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
