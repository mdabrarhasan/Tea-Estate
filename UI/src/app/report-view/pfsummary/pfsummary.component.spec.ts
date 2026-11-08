import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PfsummaryComponent } from './pfsummary.component';

describe('PfsummaryComponent', () => {
  let component: PfsummaryComponent;
  let fixture: ComponentFixture<PfsummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PfsummaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PfsummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
