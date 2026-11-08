import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeafcontrolComponent } from './leafcontrol.component';

describe('LeafcontrolComponent', () => {
  let component: LeafcontrolComponent;
  let fixture: ComponentFixture<LeafcontrolComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LeafcontrolComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeafcontrolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
