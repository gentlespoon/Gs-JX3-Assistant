import { TestBed } from '@angular/core/testing';

import { CheckUpdateService } from './check-update.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CheckUpdateService', () => {
  let service: CheckUpdateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(CheckUpdateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
