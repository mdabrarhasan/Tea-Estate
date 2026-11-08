import { TestBed } from '@angular/core/testing';

import { OperatorpermissionService } from './operatorpermission.service';

describe('OperatorpermissionService', () => {
  let service: OperatorpermissionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OperatorpermissionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
