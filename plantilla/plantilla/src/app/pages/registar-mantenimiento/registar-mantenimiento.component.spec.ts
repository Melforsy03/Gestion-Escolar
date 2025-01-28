import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistarMantenimientoComponent } from './registar-mantenimiento.component';

describe('RegistarMantenimientoComponent', () => {
  let component: RegistarMantenimientoComponent;
  let fixture: ComponentFixture<RegistarMantenimientoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegistarMantenimientoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistarMantenimientoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
