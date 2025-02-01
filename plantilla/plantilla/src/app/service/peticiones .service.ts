import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthGuard } from '../components/Autentificacion/auth.service';

@Injectable({
  providedIn: 'root',
})
export class ProfessorService {
  private baseUrl = 'http://localhost:5164/professorClassRoom';

 constructor(private http: HttpClient , private Authtoken :AuthGuard) {}
  private token = this.Authtoken.getToken(); 
  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      Authorization: `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
  }

  getSpecs(): Observable<{ spec: string[] }> {
    return this.http.get<{ spec: string[] }>(`${this.baseUrl}/getSpecs` ,{ headers: this.getHeaders() });
  }

  getProfessorsBySpec(spec: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/getProfessorsBySpec?spec=${spec}`, { headers: this.getHeaders() });
  }
}
