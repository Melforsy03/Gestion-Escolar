import { Component, OnInit, AfterViewInit, AfterViewChecked} from "@angular/core";
import { Chart } from "chart.js";
import { ProfesorService } from "src/app/service/profesor-info.service";

@Component({
  selector: "app-profesor",
  templateUrl: "profesor-info.component.html",
  styleUrls: ["./profesor-info.component.css"],
})
export class ProfesorComponent implements OnInit, AfterViewInit, AfterViewChecked {
  globalSearchQuery: string = "";
  profesores: any[] = [];
  filteredProfesores: any[] = [];
  graficosVisibles: boolean[] = [];
  renderedCharts: Set<number> = new Set();
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
    this.profesorService.listProfesores().subscribe(
      (data) => {
        console.log('Datos recibidos del servicio:', data);
        this.profesores = Array.isArray(data) ? data : [data]; // Verifica si es un arreglo
        this.filteredProfesores = [...this.profesores]; // Asegúrate de inicializar también los datos filtrados
      },
      (error) => {
        console.error('Error al obtener profesores:', error);
      }
    );
  }


  ngAfterViewChecked(): void {
    // Asegurarse de que los gráficos se renderizan al mostrar el contenedor
    this.graficosVisibles.forEach((visible, index) => {
      if (visible && !this.renderedCharts.has(index)) {
        const canvas = document.getElementById(`graficoProfesor${index}`) as HTMLCanvasElement;
        if (canvas) {
          this.renderGrafico(index); // Renderiza el gráfico
          this.renderedCharts.add(index); // Marca el gráfico como renderizado
        }
      }
    });
  }
  ngAfterViewInit(): void {
    // Inicialización, si fuera necesario
  }




  // Cargar profesores desde el servicio
  loadProfesores(): void {
    this.profesorService.listProfesores().subscribe(
      (data) => {
        console.log('Datos recibidos del servicio:', data);
        this.profesores = data; // Asigna los datos a la variable profesores
        this.filteredProfesores = [...this.profesores]; // Inicializa la lista filtrada
      },
      (error) => {
        console.error('Error al obtener profesores:', error);
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
        (`${profesor.nombre} ${profesor.apellidos}`)
          .toLowerCase()
          .includes(this.searchQuery.toLowerCase())
      );
    } else {
      this.filteredProfesores = [...this.profesores];
    }
  }

  addProfesor(): void {
    if (!this.newProfesor.nombre || !this.newProfesor.apellidos || !this.newProfesor.especialidad) {
      console.error("Faltan datos obligatorios para crear un profesor.");
      return;
    }

    this.profesorService.createProfesor(this.newProfesor).subscribe(
      (profesorCreado) => {
        this.profesores.push(profesorCreado);
        this.filteredProfesores = [...this.profesores];
        this.newProfesor = { nombre: "", apellidos: "", especialidad: "", contrato: "Tiempo Completo", asignaturas: [] };
        this.selectedAsignaturas = [];
        this.showAddProfesorForm = false;
      },
      (error) => {
        console.error("Error al agregar profesor:", error);
      }
    );
  }


  toggleGrafico(index: number): void {
    if (!this.profesores[index] || !this.profesores[index].asignaturas) {
      console.error('Datos incompletos para renderizar gráfico.');
      return;
    }

    this.graficosVisibles[index] = !this.graficosVisibles[index];
    if (this.graficosVisibles[index]) {
      setTimeout(() => {
        this.renderGrafico(index);
      }, 0); // Espera un ciclo para asegurar que el DOM se haya actualizado.
    }
  }



  calcularPromedio(calificaciones: number[]): number {
    const suma = calificaciones.reduce((acc, curr) => acc + curr, 0);
    return suma / calificaciones.length;
  }

  renderGrafico(index: number): void {


    const profesor = this.profesores[index];
    const canvasId = `graficoProfesor${index}`;
    const ctx = document.getElementById(canvasId) as HTMLCanvasElement;
    if (!profesor.asignaturas || profesor.asignaturas.length === 0) {
      console.warn(`El profesor ${profesor.nameProf} no tiene asignaturas.`);
      return;
    }
    if (!ctx) {
      console.error(`No se encontró el elemento con ID: ${canvasId}`);
      return;
    }

    // Resto del código para crear el gráfico
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

    this.filteredProfesores = this.profesores.filter((profesor) => {
      const nombre = profesor.nombre ? profesor.nombre.toLowerCase() : '';
      const apellidos = profesor.apellidos ? profesor.apellidos.toLowerCase() : '';

      return nombre.includes(query) || apellidos.includes(query);
    });
  }
}
