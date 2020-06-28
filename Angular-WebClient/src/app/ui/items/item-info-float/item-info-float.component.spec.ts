import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemInfoFloatComponent } from './item-info-float.component';

describe('ItemInfoFloatComponent', () => {
  let component: ItemInfoFloatComponent;
  let fixture: ComponentFixture<ItemInfoFloatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemInfoFloatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemInfoFloatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
