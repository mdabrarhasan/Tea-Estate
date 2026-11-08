import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailygardensummaryComponent } from './dailygardensummary.component';

describe('DailygardensummaryComponent', () => {
  let component: DailygardensummaryComponent;
  let fixture: ComponentFixture<DailygardensummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DailygardensummaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DailygardensummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
