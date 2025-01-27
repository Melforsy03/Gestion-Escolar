import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MediosTecnologicosComponent } from './medios-tecnologicos.component';

describe('MediosTecnologicosComponent', () => {
  let component: MediosTecnologicosComponent;
  let fixture: ComponentFixture<MediosTecnologicosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MediosTecnologicosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MediosTecnologicosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
