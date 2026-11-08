import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WagecalenderComponent } from './wagecalender.component';

describe('WagecalenderComponent', () => {
  let component: WagecalenderComponent;
  let fixture: ComponentFixture<WagecalenderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WagecalenderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WagecalenderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
