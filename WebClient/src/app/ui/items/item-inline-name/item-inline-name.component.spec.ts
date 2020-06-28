import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemInlineNameComponent } from './item-inline-name.component';

describe('ItemInlineNameComponent', () => {
  let component: ItemInlineNameComponent;
  let fixture: ComponentFixture<ItemInlineNameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemInlineNameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemInlineNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
