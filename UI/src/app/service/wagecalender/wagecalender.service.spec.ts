import { TestBed } from '@angular/core/testing';

import { WagecalenderService } from './wagecalender.service';

describe('WagecalenderService', () => {
  let service: WagecalenderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WagecalenderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
