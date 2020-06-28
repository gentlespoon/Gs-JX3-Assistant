import { TestBed } from '@angular/core/testing';

import { ItemInfoFloatService } from './item-info-float.service';

describe('ItemInfoFloatService', () => {
  let service: ItemInfoFloatService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ItemInfoFloatService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
