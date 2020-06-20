import { TestBed } from '@angular/core/testing';

import { ItemPreviewService } from './item-preview.service';

describe('ItemPreviewService', () => {
  let service: ItemPreviewService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ItemPreviewService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
