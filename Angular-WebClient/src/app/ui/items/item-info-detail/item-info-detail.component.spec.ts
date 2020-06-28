import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemInfoDetailComponent } from './item-info-detail.component';

describe('ItemInfoDetailComponent', () => {
  let component: ItemInfoDetailComponent;
  let fixture: ComponentFixture<ItemInfoDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemInfoDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemInfoDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
