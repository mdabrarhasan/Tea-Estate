import { TestBed } from '@angular/core/testing';

import { LeafcontrolService } from './leafcontrol.service';

describe('LeafcontrolService', () => {
  let service: LeafcontrolService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LeafcontrolService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
