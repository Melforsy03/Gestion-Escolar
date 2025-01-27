import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { TechnologicalMeansService } from 'src/app/service/inventario.service';
@Component({
  selector: 'app-medioAuxiliar',
  templateUrl: './mediosAuxiliares.component.html',
  styleUrls: ['./mediosAuxiliares.component.css']
})
export class MedioAuxiliarComponent implements OnInit {
  medios: any[] = [];
  medioForm: FormGroup;
  isFormVisible = false;
  isEditMode = false;
  currentMedio: any = null;

  constructor(private fb: FormBuilder , private medio : TechnologicalMeansService) {}

  
    ngOnInit(): void {
      this.medioForm = this.fb.group({
        nameMean: ['', Validators.required],
        state: ['', Validators.required],
        type: ['', Validators.required]
      });
  
      // ... rest of your existing code ...
    }
  
    onSubmit(): void {
      if (this.medioForm.valid) {
        const mediumData = this.medioForm.value;
        
        this.medio.createMean(mediumData).subscribe(
          response => {
            console.log('Medium created successfully:', response);
            this.medios.push(response);
            this.toggleForm();
          },
          error => {
            console.error('Error creating medium:', error);
            // Handle error (e.g., show error message to user)
          }
        );
      }
    }
  
  get fechasMantenimiento(): FormArray {
    return this.medioForm.get('fechasMantenimiento') as FormArray;
  }

  addFechaMantenimiento(): void {
    this.fechasMantenimiento.push(this.fb.group({ fecha: [''] }));
  }

  removeFechaMantenimiento(index: number): void {
    this.fechasMantenimiento.removeAt(index);
  }

  addFechaMantenimientoToMedio(medio: any): void {
    if (medio.newFechaMantenimiento) {
      medio.fechasMantenimiento.push({ fecha: medio.newFechaMantenimiento });
      medio.newFechaMantenimiento = '';
    }
  }

  removeFechaMantenimientoFromMedio(medio: any, index: number): void {
    medio.fechasMantenimiento.splice(index, 1);
  }

  toggleForm(): void {
    this.isFormVisible = !this.isFormVisible;
    this.medioForm.reset();
  }

  editMedio(medio: any): void {
    medio.isEditing = true;
  }

  saveMedio(medio: any): void {
    medio.isEditing = false;
  }

  deleteMedio(medio: any): void {
    this.medios = this.medios.filter(m => m !== medio);
  }

  toggleFechasMantenimiento(medio: any): void {
    medio.showFechasMantenimiento = !medio.showFechasMantenimiento;
  }


}