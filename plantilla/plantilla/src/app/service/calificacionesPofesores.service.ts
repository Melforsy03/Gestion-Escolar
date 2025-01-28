import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthGuard } from 'src/app/components/Autentificacion/auth.service'; // Importa tu servicio de autenticación

export interface Calificacion {
  profesor: string;
  estudiante: string;
  asignatura: string;
  curso: string;
  evaluacion: string;
}

@Injectable({
  providedIn: 'root',
})
export class CalificacionesService {
  private baseUrl = 'http://localhost:5164/profstudsubcourse'; // Cambia la URL según tu configuración

  constructor(private http: HttpClient, private Authservice: AuthGuard) {}

  // Listar calificaciones
  listCalificaciones(): Observable<Calificacion[]> {
    const headers = this.getAuthHeaders();
    console.log('Haciendo la petición con los encabezados:', headers);
    return this.http.get<Calificacion[]>(`${this.baseUrl}/list`, { headers });
  }

  // Obtener los encabezados de autenticación
  private getAuthHeaders(): HttpHeaders {
    const token = this.Authservice.getToken(); // Obtiene el token desde AuthGuard
    console.log('Token de autenticación:', token);
    return new HttpHeaders({
      Authorization: `Bearer ${token}`, // Incluye el token en los encabezados
    });
  }
}
