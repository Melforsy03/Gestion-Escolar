import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthGuard } from 'src/app/components/Autentificacion/auth.service';

@Injectable({
  providedIn: 'root',
})
export class EstudentService {
  private apiBaseUrl = 'http://localhost:5164/professorStudentSubject'; // URL base del API

  constructor(private http: HttpClient, private Authservice: AuthGuard) {}

  // Obtener estudiantes de un profesor (usando el nombre de usuario)
  getStudentsByUser(userName: string): Observable<any[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<any[]>(`${this.apiBaseUrl}/getstudents?UserName=${userName}`, { headers });
  }

  // Obtener los encabezados de autenticación
  private getAuthHeaders(): HttpHeaders {
    const token = this.Authservice.getToken(); // Reemplaza esto con la lógica para obtener el token dinámico
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
  }
}
