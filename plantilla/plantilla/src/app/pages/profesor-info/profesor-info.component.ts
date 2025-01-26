import { Component, OnInit, AfterViewInit } from "@angular/core";
import { Chart } from "chart.js";
import { ProfesorService } from "src/app/service/profesor-info.service";

@Component({
  selector: "app-profesor",
  templateUrl: "profesor-info.component.html",
  styleUrls: ["./profesor-info.component.css"],
})
export class ProfesorComponent implements OnInit, AfterViewInit {
  globalSearchQuery: string = "";
  profesores: any[] = [];
  filteredProfesores: any[] = [];
  graficosVisibles: boolean[] = [];
  searchQuery: string = "";
  showAddProfesorForm: boolean = false;
  newProfesor = {
    nombre: "",
    apellidos: "",
    especialidad: "",
    contrato: "Tiempo Completo",
    asignaturas: [],
  };
  selectedAsignaturas: string[] = []; // Para las asignaturas seleccionadas
  availableAsignaturas = [
    { id: 1, nombre: "Matemáticas" },
    { id: 2, nombre: "Física" },
    { id: 3, nombre: "Química" },
    { id: 4, nombre: "Historia" },
    { id: 5, nombre: "Lengua y Literatura" },
  ];

  constructor(private profesorService: ProfesorService) {}

  ngOnInit(): void {
    this.loadProfesores();
  }
  ngAfterViewInit(): void {
    // Inicialización, si fuera necesario
  }


  // Cargar profesores desde el servicio
  loadProfesores(): void {
    this.profesorService.listProfesores().subscribe(
      (data) => {
        this.profesores = data;
        this.filteredProfesores = [...this.profesores];
        this.graficosVisibles = new Array(this.profesores.length).fill(false);
      },
      (error) => {
        console.error("Error al cargar profesores:", error);
      }
    );
  }

  toggleAsignatura(event: any): void {
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
      this.filteredProfesores = [...this.profesores];
    }
  }

  addProfesor(): void {
    if (this.newProfesor.nombre && this.newProfesor.apellidos && this.newProfesor.especialidad) {
      this.profesorService.createProfesor(this.newProfesor).subscribe(
        (profesorCreado) => {
          this.profesores.push(profesorCreado);
          this.filteredProfesores = [...this.profesores];
          this.newProfesor = { nombre: "", apellidos: "", especialidad: "", contrato: "Tiempo Completo", asignaturas: [] }; // Reset form
          this.selectedAsignaturas = [];
          this.showAddProfesorForm = false; // Cerrar el modal después de guardar
        },
        (error) => {
          console.error("Error al agregar profesor:", error);
        }
      );
    }
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
    const promedios = profesor.asignaturas.map((asignatura: any) =>
      this.calcularPromedio(asignatura.calificaciones)
    );

    new Chart(ctx, {
      type: "bar",
      data: {
        labels: profesor.asignaturas.map((a: any) => a.nombre),
        datasets: [
          {
            label: "Promedio de Calificaciones",
            backgroundColor: "#42a5f5",
            borderColor: "#1e88e5",
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

  updateDisplayProfesor(): void {
    const query = this.globalSearchQuery.toLowerCase();
    this.filteredProfesores = this.profesores.filter(
      (profesor) =>
        profesor.nombre.toLowerCase().includes(query) ||
        profesor.apellidos.toLowerCase().includes(query)
    );
  }
}
