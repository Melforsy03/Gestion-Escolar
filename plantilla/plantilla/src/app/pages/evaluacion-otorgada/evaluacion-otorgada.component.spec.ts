import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluacionOtorgadaComponent } from './evaluacion-otorgada.component';

describe('EvaluacionOtorgadaComponent', () => {
  let component: EvaluacionOtorgadaComponent;
  let fixture: ComponentFixture<EvaluacionOtorgadaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EvaluacionOtorgadaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EvaluacionOtorgadaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
