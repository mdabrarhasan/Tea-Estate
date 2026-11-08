import { TestBed } from '@angular/core/testing';

import { PrunetypemanagementService } from './prunetypemanagement.service';

describe('PrunetypemanagementService', () => {
  let service: PrunetypemanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PrunetypemanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
