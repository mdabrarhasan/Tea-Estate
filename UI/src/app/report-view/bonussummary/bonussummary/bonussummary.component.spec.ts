import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BonussummaryComponent } from './bonussummary.component';

describe('BonussummaryComponent', () => {
  let component: BonussummaryComponent;
  let fixture: ComponentFixture<BonussummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BonussummaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BonussummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
