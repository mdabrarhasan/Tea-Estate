import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AbsentsummaryComponent } from './absentsummary.component';

describe('AbsentsummaryComponent', () => {
  let component: AbsentsummaryComponent;
  let fixture: ComponentFixture<AbsentsummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AbsentsummaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AbsentsummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
