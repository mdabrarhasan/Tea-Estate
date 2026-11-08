import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GreenleafsummaryComponent } from './greenleafsummary.component';

describe('GreenleafsummaryComponent', () => {
  let component: GreenleafsummaryComponent;
  let fixture: ComponentFixture<GreenleafsummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GreenleafsummaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GreenleafsummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
