import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OperatorpermissionComponent } from './operatorpermission.component';

describe('OperatorpermissionComponent', () => {
  let component: OperatorpermissionComponent;
  let fixture: ComponentFixture<OperatorpermissionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OperatorpermissionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OperatorpermissionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
