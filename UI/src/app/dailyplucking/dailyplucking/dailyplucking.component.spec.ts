import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailypluckingComponent } from './dailyplucking.component';

describe('DailypluckingComponent', () => {
  let component: DailypluckingComponent;
  let fixture: ComponentFixture<DailypluckingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DailypluckingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DailypluckingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
