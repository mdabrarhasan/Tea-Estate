import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NonpluckingactivitiesComponent } from './nonpluckingactivities.component';

describe('NonpluckingactivitiesComponent', () => {
  let component: NonpluckingactivitiesComponent;
  let fixture: ComponentFixture<NonpluckingactivitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NonpluckingactivitiesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NonpluckingactivitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
