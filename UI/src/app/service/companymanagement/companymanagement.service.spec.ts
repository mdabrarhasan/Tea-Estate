import { TestBed } from '@angular/core/testing';

import { CompanymanagementService } from './companymanagement.service';

describe('CompanymanagementService', () => {
  let service: CompanymanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CompanymanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
