import { TestBed } from '@angular/core/testing';

import { SalarygenerateService } from './salarygenerate.service';

describe('SalarygenerateService', () => {
  let service: SalarygenerateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SalarygenerateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
