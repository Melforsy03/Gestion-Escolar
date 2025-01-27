import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthGuard } from 'src/app/components/Autentificacion/auth.service';

export interface TechnologicalMeans {
  idMean?: number;
  nameMean: string;
  state: string;
}

@Injectable({
  providedIn: 'root',
})
export class TechnologicalMeansService {
  private baseUrl = 'http://localhost:5164/technologicalmeans'; // Contexto correcto

  constructor(private http: HttpClient, private Authservice: AuthGuard) {}

  createTechnologicalMeans(technologicalMeans: TechnologicalMeans): Observable<TechnologicalMeans> {
    const headers = this.getAuthHeaders();
    return this.http.post<TechnologicalMeans>(`${this.baseUrl}/create`, technologicalMeans, { headers });
  }

  listTechnologicalMeans(): Observable<TechnologicalMeans[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<TechnologicalMeans[]>(`${this.baseUrl}/list`, { headers }).pipe(
      tap((data) => {
        console.log('Datos recibidos del backend (listTechnologicalMeans):', data);
      })
    );
  }

  deleteTechnologicalMeans(id: number): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.delete<any>(`${this.baseUrl}/delete?technologicalMeansId=${id}`, { headers });
  }

  private getAuthHeaders(): HttpHeaders {
    const token = this.Authservice.getToken();
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
  }
}
