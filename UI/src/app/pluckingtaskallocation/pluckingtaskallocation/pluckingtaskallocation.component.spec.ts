import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PluckingtaskallocationComponent } from './pluckingtaskallocation.component';

describe('PluckingtaskallocationComponent', () => {
  let component: PluckingtaskallocationComponent;
  let fixture: ComponentFixture<PluckingtaskallocationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PluckingtaskallocationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PluckingtaskallocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
