import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SectionmanagementComponent } from './sectionmanagement.component';

describe('SectionmanagementComponent', () => {
  let component: SectionmanagementComponent;
  let fixture: ComponentFixture<SectionmanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SectionmanagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SectionmanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
