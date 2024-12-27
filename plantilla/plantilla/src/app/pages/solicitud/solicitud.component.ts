import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';

@Component({
  selector: 'app-solicitud',
  templateUrl: './solicitud.component.html',
  styleUrls: ['./solicitud.component.css'],
 
})
export class SolicitudComponent implements OnInit {
  solicitudForm: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.solicitudForm = this.fb.group({
      aula: ['', Validators.required],
      medios: this.fb.array([this.createMedio()])
    });
  }

  get medios(): FormArray {
    return this.solicitudForm.get('medios') as FormArray;
  }

  createMedio(): FormGroup {
    return this.fb.group({
      nombre: ['', Validators.required],
      cantidad: [1, [Validators.required, Validators.min(1)]]
    });
  }

  addMedio(): void {
    this.medios.push(this.createMedio());
  }

  removeMedio(index: number): void {
    this.medios.removeAt(index);
  }

  onSubmit(): void {
    if (this.solicitudForm.valid) {
      console.log(this.solicitudForm.value);
      // Aquí puedes manejar el envío del formulario, por ejemplo, enviarlo a una API
    }
  }
}