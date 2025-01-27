import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedioAuxiliarComponent } from './mediosAuxiliares.component';

describe('InventarioComponent', () => {
  let component: MedioAuxiliarComponent;
  let fixture: ComponentFixture<MedioAuxiliarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedioAuxiliarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MedioAuxiliarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
