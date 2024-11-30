import { Component, OnInit, AfterViewInit } from "@angular/core";
import { Chart } from 'chart.js';

@Component({
  selector: "app-profesor",
  templateUrl: "profesor-info.component.html",
  styleUrls : ["./profesor-info.component.css"]
})
export class ProfesorComponent implements OnInit, AfterViewInit {
  profesores = [
    {
      nombre: 'Juan',
      apellidos: 'Pérez',
      especialidad: 'Matemáticas',
      contrato: 'Tiempo Completo',
      asignaturas: [
        { nombre: 'Álgebra', calificaciones: [8, 9, 7, 6] },
        { nombre: 'Cálculo', calificaciones: [9, 9.5, 8.7, 9] },
      ],
    },
    {
      nombre: 'Ana',
      apellidos: 'Gómez',
      especialidad: 'Física',
      contrato: 'Parcial',
      asignaturas: [
        { nombre: 'Mecánica', calificaciones: [7.5, 8.1, 7.9] },
        { nombre: 'Termodinámica', calificaciones: [8, 8.2, 8.5] },
      ],
    },
  ];
  graficosVisibles: boolean[] = [];

  filteredProfesores = this.profesores;
  searchQuery: string = '';
  showAddProfesorForm: boolean = false;
  newAsignatura: string = '';
  newProfesor = { nombre: '', apellidos: '', especialidad: '', contrato: 'Tiempo Completo' ,asignaturas: []  };
  selectedAsignaturas: string[] = []; // Para las asignaturas seleccionadas
    // Lista de asignaturas disponibles
    availableAsignaturas = [
      { id: 1, nombre: 'Matemáticas' },
      { id: 2, nombre: 'Física' },
      { id: 3, nombre: 'Química' },
      { id: 4, nombre: 'Historia' },
      { id: 5, nombre: 'Lengua y Literatura' }
    ];
  constructor() {}

  ngOnInit(): void {
    this.graficosVisibles = new Array(this.profesores.length).fill(false);
  }
  toggleAsignatura(event: any) {
    const asignatura = event.target.value;

    if (event.target.checked) {
      // Agregar la asignatura seleccionada
      this.newProfesor.asignaturas.push(asignatura);
    } else {
      // Eliminar la asignatura si se deselecciona
      const index = this.newProfesor.asignaturas.indexOf(asignatura);
      if (index > -1) {
        this.newProfesor.asignaturas.splice(index, 1);
      }
    }
  }
  filterProfesores(): void {
    if (this.searchQuery) {
      this.filteredProfesores = this.profesores.filter((profesor) =>
        `${profesor.nombre} ${profesor.apellidos}`
          .toLowerCase()
          .includes(this.searchQuery.toLowerCase())
      );
    } else {
      this.filteredProfesores = this.profesores;
    }
  }

  addProfesor(): void {
    if (this.newProfesor.nombre && this.newProfesor.apellidos && this.newProfesor.especialidad) {
      this.profesores.push({ ...this.newProfesor, asignaturas: [] });
      this.filteredProfesores = [...this.profesores];
      this.showAddProfesorForm = false;
      this.newProfesor = { nombre: '', apellidos: '', especialidad: '', contrato: 'Tiempo Completo' ,asignaturas: []  }; // Reset form
      this.newProfesor.asignaturas = [...this.selectedAsignaturas];
      this.showAddProfesorForm = false; // Cerrar el modal después de guardar
    }
  }
  ngAfterViewInit(): void {
    // Inicialización, si fuera necesario
  }

  toggleGrafico(index: number): void {
    this.graficosVisibles[index] = !this.graficosVisibles[index];
    if (this.graficosVisibles[index]) {
      setTimeout(() => {
        this.renderGrafico(index);
      }, 0);
    }
  }

  calcularPromedio(calificaciones: number[]): number {
    const suma = calificaciones.reduce((acc, curr) => acc + curr, 0);
    return suma / calificaciones.length;
  }

  renderGrafico(index: number): void {
    const profesor = this.profesores[index];
    const ctx = document.getElementById(`graficoProfesor${index}`) as HTMLCanvasElement;

    if (!ctx) return;

    // Calcular el promedio de las calificaciones de cada curso
    const promedios = profesor.asignaturas.map((asignatura) =>
      this.calcularPromedio(asignatura.calificaciones)
    );

    new Chart(ctx, {
      type: 'bar',
      data: {
        labels: profesor.asignaturas.map((a) => a.nombre),
        datasets: [
          {
            label: 'Promedio de Calificaciones',
           backgroundColor: '#42a5f5',
           borderColor: '#1e88e5',
            data: promedios,
          },
        ],
      },
      options: {
        responsive: true,
        plugins: {
          legend: { display: true },
          tooltip: { enabled: true },
        },
        scales: {
          x: { grid: { display: false } },
          y: { beginAtZero: true },
        },
      },
    });
  }
}


