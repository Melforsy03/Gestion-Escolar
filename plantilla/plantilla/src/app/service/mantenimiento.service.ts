import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthGuard } from '../components/Autentificacion/auth.service';

@Injectable({
  providedIn: 'root',
})
export class MantenimientoService {
  private baseUrl = 'http://localhost:5164';

  constructor(private http: HttpClient, private authToken: AuthGuard) {}

  private token = this.authToken.getToken();

  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      Authorization: `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
  }

  listMantenimientos(): Observable<any> {
    return this.http.get(`${this.baseUrl}/maintenance/list`, { headers: this.getHeaders() });
  }

  createMantenimiento(mantenimiento: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/maintenance/create`, mantenimiento, { headers: this.getHeaders() });
  }

  updateMantenimiento(mantenimiento: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/mantenimiento/update`, mantenimiento, { headers: this.getHeaders() });
  }

  deleteMantenimiento(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/mantenimiento/delete?idM=${id}`, { headers: this.getHeaders() });
  }
}
