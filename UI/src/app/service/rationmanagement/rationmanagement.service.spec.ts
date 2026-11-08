import { TestBed } from '@angular/core/testing';

import { RationmanagementService } from './rationmanagement.service';

describe('RationmanagementService', () => {
  let service: RationmanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RationmanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
