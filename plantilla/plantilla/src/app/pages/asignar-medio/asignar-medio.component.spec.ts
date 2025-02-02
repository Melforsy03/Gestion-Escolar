import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignarMedioComponent } from './asignar-medio.component';

describe('AsignarMedioComponent', () => {
  let component: AsignarMedioComponent;
  let fixture: ComponentFixture<AsignarMedioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AsignarMedioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AsignarMedioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
