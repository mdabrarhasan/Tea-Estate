import { TestBed } from '@angular/core/testing';

import { DivisionmanagementService } from './divisionmanagement.service';

describe('DivisionmanagementService', () => {
  let service: DivisionmanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DivisionmanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
