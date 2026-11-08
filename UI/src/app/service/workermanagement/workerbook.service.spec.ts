import { TestBed } from '@angular/core/testing';

import { WorkerbookService } from './workerbook.service';

describe('WorkerbookService', () => {
  let service: WorkerbookService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkerbookService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
