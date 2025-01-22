import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';

@Component({
  selector: 'app-inventario',
  templateUrl: './inventario.component.html',
  styleUrls: ['./inventario.component.css']
})
export class InventarioComponent implements OnInit {
  medios: any[] = [];
  medioForm: FormGroup;
  isFormVisible = false;
  isEditMode = false;
  currentMedio: any = null;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.medioForm = this.fb.group({
      nombre: ['', Validators.required],
      tipo: ['', Validators.required],
      estado: [''],
      ubicacion: [''],
      asignatura: [''],
      costoMantenimiento: [''],
      fechasMantenimiento: this.fb.array([]),
      fechaReemplazo: ['']
    });

    // Datos de ejemplo
    this.medios = [
      {
        id: '1',
        nombre: 'Proyector',
        tipo: 'tecnologico',
        estado: 'funcionando',
        ubicacion: 'Aula 101',
        asignatura: 'Matemáticas',
        costoMantenimiento: 50,
        fechasMantenimiento: [{ fecha: '2023-01-01' }, { fecha: '2023-06-01' }],
        fechaReemplazo: '2024-01-01',
        isEditing: false,
        showFechasMantenimiento: false,
        newFechaMantenimiento: ''
      },
      {
        id: '2',
        nombre: 'Computadora',
        tipo: 'tecnologico',
        estado: 'enMantenimiento',
        ubicacion: 'Laboratorio de Informática',
        asignatura: 'Informática',
        costoMantenimiento: 100,
        fechasMantenimiento: [{ fecha: '2023-02-01' }, { fecha: '2023-07-01' }],
        fechaReemplazo: '2024-02-01',
        isEditing: false,
        showFechasMantenimiento: false,
        newFechaMantenimiento: ''
      },
      {
        id: '3',
        nombre: 'Pizarra',
        tipo: 'materialDidactico',
        estado: 'funcionando',
        ubicacion: 'Aula 202',
        asignatura: 'Historia',
        costoMantenimiento: 20,
        fechasMantenimiento: [{ fecha: '2023-03-01' }, { fecha: '2023-08-01' }],
        fechaReemplazo: '2024-03-01',
        isEditing: false,
        showFechasMantenimiento: false,
        newFechaMantenimiento: ''
      }
    ];
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

  onSubmit(): void {
    if (this.medioForm.valid) {
      this.medios.push(this.medioForm.value);
      this.toggleForm();
    }
  }
}