import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemNameComponent } from './item-name.component';

describe('ItemNameComponent', () => {
  let component: ItemNameComponent;
  let fixture: ComponentFixture<ItemNameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemNameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
