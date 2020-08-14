import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LszjComponent } from './lszj.component';

describe('LszjComponent', () => {
  let component: LszjComponent;
  let fixture: ComponentFixture<LszjComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LszjComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LszjComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
