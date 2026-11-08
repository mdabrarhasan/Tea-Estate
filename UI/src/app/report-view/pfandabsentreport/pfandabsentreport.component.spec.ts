import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PfandabsentreportComponent } from './pfandabsentreport.component';

describe('PfandabsentreportComponent', () => {
  let component: PfandabsentreportComponent;
  let fixture: ComponentFixture<PfandabsentreportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PfandabsentreportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PfandabsentreportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
