import { TestBed } from '@angular/core/testing';

import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { ItemsService } from './items.service';

describe('ItemsService', () => {
  let service: ItemsService;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ItemsService],
    });
    service = TestBed.get(ItemsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
