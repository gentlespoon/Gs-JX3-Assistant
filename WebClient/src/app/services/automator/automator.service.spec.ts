import { TestBed } from '@angular/core/testing';

import { AutomatorService } from './automator.service';

describe('AutomatorService', () => {
  let service: AutomatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AutomatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
