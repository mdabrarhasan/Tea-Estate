import { TestBed } from '@angular/core/testing';

import { SectionmanagementService } from './sectionmanagement.service';

describe('SectionmanagementService', () => {
  let service: SectionmanagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SectionmanagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
