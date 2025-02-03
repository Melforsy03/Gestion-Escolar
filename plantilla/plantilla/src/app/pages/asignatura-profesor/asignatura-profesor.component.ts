import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Solicitud } from 'src/app/service/asignatura-profesor.service';

@Component({
  selector: 'app-solicitud',
  templateUrl: './asignatura-profesor.component.html',
  styleUrls: ['./asignatura-profesor.component.css'],
})
export class AsignaturaPComponent implements OnInit {
  solicitudForm: FormGroup;
  subjects: any[] = [];
  professors: any[] = [];
  selectedSubject: any = null;
  professorSubjectAssignments: any[] = [];
  showAssignedSubjects = false;
  filteredSubjects: any[] = [];
  searchTerm: string = '';

  constructor(private fb: FormBuilder, private http: HttpClient, private solicitud: Solicitud) {
    this.solicitudForm = this.fb.group({
      subject: [''],
    });
  }

  ngOnInit(): void {
    this.cargarAsignaturas();
  }

  cargarAsignaturas() {
    this.solicitud.getSubjects().subscribe(
      (response) => {
        this.subjects = response || [];
        this.filteredSubjects = this.subjects; // Inicializar con todas las asignaturas
      },
      (error) => {
        console.error('Error al cargar asignaturas:', error);
      }
    );
  }

  filterSubjects() {
    this.filteredSubjects = this.subjects.filter(subject =>
      subject.nameSub.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  seleccionarAsignatura(subjectId: string) {
    if (!subjectId) {
      this.selectedSubject = null;
      this.professors = [];
      return;
    }

    const subjectIdNumber = Number(subjectId);
    this.selectedSubject = this.subjects.find(subject => subject.idSub === subjectIdNumber);

    this.solicitud.getProfessorsForSubject(subjectIdNumber).subscribe(
      (response) => {
        // Filtrar los profesores ya asignados para que no aparezcan en la lista
        this.professors = response.filter(professor =>
          !this.professorSubjectAssignments.some(assignment =>
            assignment.professorId === professor.id && assignment.subjectId === subjectIdNumber
          ));
      },
      (error) => {
        console.error('Error al obtener profesores:', error);
      }
    );
  }

  asignarProfesor(professorId: number) {
    if (!this.selectedSubject) {
      alert('Debe seleccionar una asignatura antes de asignar un profesor');
      return;
    }

    const data = {
      idProf: professorId,
      idSub: this.selectedSubject.idSub,
    };

    this.solicitud.assignProfessorToSubject(data).subscribe(
      (response) => {
        console.log('Profesor asignado exitosamente', response);
        // Actualizar la lista de asignaciones
        this.professorSubjectAssignments.push({
          professorId: professorId,
          subjectId: this.selectedSubject.idSub,
          professorName: response.professorName,
          subjectName: this.selectedSubject.nameSub
        });

        // Filtrar nuevamente los profesores disponibles
        this.seleccionarAsignatura(this.selectedSubject.idSub);
      },
      (error) => {
        console.error('Error al asignar el profesor:', error);
      }
    );
  }

  cargarAsignaciones() {
    this.solicitud.getProfessorSubjectAssignments().subscribe(
      (response) => {
        this.professorSubjectAssignments = response || [];
      },
      (error) => {
        console.error('Error al cargar asignaciones:', error);
      }
    );
  }

  eliminarAsignacion(idProfSub: number) {
    this.solicitud.deleteProfessorSubjectAssignment(idProfSub).subscribe(
      (response) => {
        console.log('Asignación eliminada exitosamente', response);
        // Eliminar de la lista de asignaciones locales
        this.professorSubjectAssignments = this.professorSubjectAssignments.filter(
          (assignment) => assignment.idProfSub !== idProfSub
        );

        // Filtrar nuevamente los profesores disponibles
        this.seleccionarAsignatura(this.selectedSubject.idSub);
      },
      (error) => {
        console.error('Error al eliminar asignación:', error);
      }
    );
  }

  toggleAssignedSubjects() {
    this.showAssignedSubjects = !this.showAssignedSubjects;
    this.selectedSubject = null;
    if (this.showAssignedSubjects) {
      this.cargarAsignaciones();
    }
  }
}
