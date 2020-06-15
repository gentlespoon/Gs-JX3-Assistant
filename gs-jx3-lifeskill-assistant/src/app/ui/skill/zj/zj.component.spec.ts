import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ZjComponent } from './zj.component';

describe('ZjComponent', () => {
  let component: ZjComponent;
  let fixture: ComponentFixture<ZjComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ZjComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ZjComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
