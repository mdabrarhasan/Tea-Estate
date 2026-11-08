import { TestBed } from '@angular/core/testing';

import { WorkerbatchService } from './workerbatch.service';

describe('WorkerbatchService', () => {
  let service: WorkerbatchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkerbatchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
