import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StudentGradingService {
  private apiUrl = 'http://localhost:5164';
  private headers: HttpHeaders;

  constructor(private http: HttpClient) {
    const token = 'your-auth-token'; // Replace with your token retrieval logic
    this.headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    });
  }

  getSubjects(userName: string): Observable<any[]> {
    const url = `${this.apiUrl}/professorStudentSubject/getsubjects`;
    const params = new HttpParams().set('userName', userName);
    return this.http.get<any[]>(url, { headers: this.headers, params });
  }

  getStudents(subjectId: number): Observable<any[]> {
    const url = `${this.apiUrl}/professorStudentSubject/getstudents`;
    const params = new HttpParams().set('subjectId', subjectId.toString());
    return this.http.get<any[]>(url, { headers: this.headers, params });
  }

  submitGrade(payload: any): Observable<void> {
    const url = `${this.apiUrl}/professorStudentSubject/update`;
    return this.http.post<void>(url, payload, { headers: this.headers });
  }

  listResults(): Observable<any[]> {
    const url = `${this.apiUrl}/profstudsubcourse/list`;
    return this.http.get<any[]>(url, { headers: this.headers });
  }
}
