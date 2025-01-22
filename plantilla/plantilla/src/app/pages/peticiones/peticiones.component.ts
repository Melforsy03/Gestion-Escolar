import { Component,  OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-peticiones',
  templateUrl: './peticiones.component.html',
  styleUrls: ['./peticiones.component.css']
})
export class PeticionesComponent implements OnInit {
  funcionSeleccionada: number = 1; // Por defecto, muestra la primera funcionalidad.
  dropdownOpen: boolean = false; // Estado del menú desplegable.
  datosProfesores: any[] = [];
  datosMantenimientos: any[] = [];
  selectedOption: string = 'profesores' // Opción seleccionada
  opciones: string[] = ['Especialización de Profesores', 'Mantenimientos por Aula' ,'funcionalidad3' ,'funcionalidad4','funcionalidad5', 'funcionalidad6']
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.updateTable();
  }
 
  tableOptions = [
    { value: 'profesores', label: 'Especialización de Profesores' },
    { value: 'mantenimientos', label: 'Mantenimientos por Aula' },
    { value: 'fun1',label : 'funcionalidad3'},
    { value: 'fun1',label : 'funcionalidad4 '},
    { value: 'fun1',label : 'funcionalidad5'},
    { value: 'fun1',label : 'funcionalidad6'}
  ]; // Opciones del filtro

  profesoresData = [
    { profesor: 'Juan Pérez', especializacion: 'Matemáticas', medioTecnologico: 'Proyector', estado: 'Funcionando' },
    { profesor: 'Ana López', especializacion: 'Ciencias', medioTecnologico: 'Pizarra Digital', estado: 'No funciona' }
  ]; // Datos de la tabla de profesores

  mantenimientosData = [
    { aula: '101', tipoMedio: 'Proyector', totalMantenimientos: 2 },
    { aula: '102', tipoMedio: 'Computadora', totalMantenimientos: 3 }
  ]; // Datos de la tabla de mantenimientos

  updateTable(): void {
    console.log('Opción seleccionada:', this.selectedOption); // Depuración
  }

  getTableTitle(option: string): string {
    const selected = this.tableOptions.find(o => o.value === option);
    return selected ? selected.label : '';
  }

}