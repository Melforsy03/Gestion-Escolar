import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class ProfesorService {
  private baseUrl = 'http://localhost:5164'; // URL base del backend, ajusta el puerto si es necesario

  constructor(private http: HttpClient) {}

  // Obtener la lista de profesores
  listProfesores(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/professor/list`).pipe(
      tap(data => console.log('Datos recibidos del servicio:', data)),
      map(data =>
        data.map(profesor => ({
          nombre: profesor.nameProf,
          apellidos: '', // No existe en los datos originales, puedes asignar un valor predeterminado
          especialidad: profesor.isDean ? 'Decano' : 'Profesor',
          contrato: profesor.contract,
          asignaturas: [] // No existe en los datos originales, puedes ajustar según lo que necesites
        }))
      ),
      catchError(error => {
        console.error('Error al obtener profesores:', error);
        return of([]); // Devuelve un array vacío en caso de error
      })
    );
  }


  // Crear un nuevo profesor
  createProfesor(profesor: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/professor/create`, profesor);
  }

  // Actualizar la información de un profesor
  updateProfesor(profesor: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/professor/update`, profesor);
  }

  // Eliminar un profesor
  deleteProfesor(profesorId: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/professor/delete?professorId=${profesorId}`);
  }
}
