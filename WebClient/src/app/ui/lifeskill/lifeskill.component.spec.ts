import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LifeskillComponent } from './lifeskill.component';

describe('LifeskillComponent', () => {
  let component: LifeskillComponent;
  let fixture: ComponentFixture<LifeskillComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [LifeskillComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LifeskillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
