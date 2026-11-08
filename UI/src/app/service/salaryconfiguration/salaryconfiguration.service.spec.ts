import { TestBed } from '@angular/core/testing';

import { SalaryconfigurationService } from './salaryconfiguration.service';

describe('SalaryconfigurationService', () => {
  let service: SalaryconfigurationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SalaryconfigurationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
