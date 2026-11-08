import { TestBed } from '@angular/core/testing';

import { PluckingtypemanagementService } from './pluckingtypemanagement.service';

describe('PluckingtypemanagementService', () => {
  let service: PluckingtypemanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PluckingtypemanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
