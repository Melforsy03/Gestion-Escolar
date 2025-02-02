import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthGuard } from '../components/Autentificacion/auth.service';

@Injectable({
  providedIn: 'root',
})
export class SolicitudService {
  private apiUrl = 'http://localhost:5164';
  private headers: HttpHeaders;

  constructor(private http: HttpClient, private Authservice: AuthGuard) {
    const token = this.Authservice.getToken();
    this.headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    });
  }

  // Obtener las aulas
  getAvailableClassrooms(): Observable<any> {
    return this.http.get(`${this.apiUrl}/classroom/list`, { headers: this.headers });
  }

  // Obtener los medios tecnológicos disponibles para un aula seleccionada
  getTechnologicalMeansForClassroom(classroomId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/technologicalmeans/list`, { headers: this.headers });
  }

  // Asignar un medio tecnológico a un aula
  assignTechnologicalMeanToClassroom(data: { idClassRoom: number, idTechMean: number }): Observable<any> {
    return this.http.post(`${this.apiUrl}/classRoomTechMean/create`, data, { headers: this.headers });
  }
}

