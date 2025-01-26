import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfesorService {
  private baseUrl = 'http://localhost:5000'; // URL base del backend, ajusta el puerto si es necesario

  constructor(private http: HttpClient) {}

  // Obtener la lista de profesores
  listProfesores(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/professor/list`);
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
