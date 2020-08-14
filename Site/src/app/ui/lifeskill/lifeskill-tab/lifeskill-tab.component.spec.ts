import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LifeskillTabComponent } from './lifeskill-tab.component';

describe('LifeskillTabComponent', () => {
  let component: LifeskillTabComponent;
  let fixture: ComponentFixture<LifeskillTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LifeskillTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LifeskillTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
