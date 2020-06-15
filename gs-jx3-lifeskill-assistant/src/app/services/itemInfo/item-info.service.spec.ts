import { TestBed } from '@angular/core/testing';

import { ItemInfoService } from './item-info.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';

describe('ItemInfoService', () => {
  let service: ItemInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, FormsModule],
    });
    service = TestBed.inject(ItemInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
