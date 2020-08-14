import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { AutomatorService } from './automator.service';

describe('AutomatorService', () => {
  let service: AutomatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(AutomatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
