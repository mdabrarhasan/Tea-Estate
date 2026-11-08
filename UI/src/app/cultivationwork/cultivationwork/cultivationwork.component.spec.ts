import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CultivationworkComponent } from './cultivationwork.component';

describe('CultivationworkComponent', () => {
  let component: CultivationworkComponent;
  let fixture: ComponentFixture<CultivationworkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CultivationworkComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CultivationworkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
