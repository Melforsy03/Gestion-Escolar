import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private students = [
    {
      name: 'Dakota Rice',
      age: 16,
      course: 'Primero',
      activities: 'Fútbol, Teatro',
      subjects: [
        { name: 'Matemáticas', average: 85, absences: 2 },
        { name: 'Lengua', average: 90, absences: 0 },
      ],
    },
    {
      name: 'Minerva Hooper',
      age: 15,
      course: 'Segundo',
      activities: 'Música',
      subjects: [
        { name: 'Historia', average: 88, absences: 1 },
        { name: 'Ciencias', average: 80, absences: 0 },
      ],
    },
  ];

  getStudents(): Observable<any[]> {
    return of(this.students);
  }
}
