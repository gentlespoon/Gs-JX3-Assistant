import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemInfoFloatContainerComponent } from './item-info-float-container.component';

describe('ItemInfoFloatContainerComponent', () => {
  let component: ItemInfoFloatContainerComponent;
  let fixture: ComponentFixture<ItemInfoFloatContainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemInfoFloatContainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemInfoFloatContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
