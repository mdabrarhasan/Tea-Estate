import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KamjarimanagementComponent } from './kamjarimanagement.component';

describe('KamjarimanagementComponent', () => {
  let component: KamjarimanagementComponent;
  let fixture: ComponentFixture<KamjarimanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [KamjarimanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(KamjarimanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
