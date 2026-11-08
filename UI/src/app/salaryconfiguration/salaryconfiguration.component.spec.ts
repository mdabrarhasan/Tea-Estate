import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalaryconfigurationComponent } from './salaryconfiguration.component';

describe('SalaryconfigurationComponent', () => {
  let component: SalaryconfigurationComponent;
  let fixture: ComponentFixture<SalaryconfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SalaryconfigurationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalaryconfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
