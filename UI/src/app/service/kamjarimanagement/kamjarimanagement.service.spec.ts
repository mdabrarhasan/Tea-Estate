import { TestBed } from '@angular/core/testing';

import { KamjarimanagementService } from './kamjarimanagement.service';

describe('KamjarimanagementService', () => {
  let service: KamjarimanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KamjarimanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
