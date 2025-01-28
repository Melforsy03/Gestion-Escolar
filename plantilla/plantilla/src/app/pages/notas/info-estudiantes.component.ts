import { Component, OnInit } from '@angular/core';
import { StudentGradingService } from 'src/app/service/generaStud.service';

@Component({
  selector: 'app-info-estudiantes',
  templateUrl: './info-estudiantes.component.html',
  styleUrls: ['./info-estudiantes.component.css']
})
export class InfoEstudiantesComponent implements OnInit {
  subjects: any[] = [];
  students: any[] = [];
  results: any[] = [];

  constructor(private gradingService: StudentGradingService) {}

  ngOnInit(): void {
    this.getSubjects('currentUser'); // Replace 'currentUser' with the actual username logic
  }

  getSubjects(userName: string): void {
    this.gradingService.getSubjects(userName).subscribe(
      (subjects) => (this.subjects = subjects),
      (error) => console.error('Error fetching subjects:', error)
    );
  }

  getStudents(subjectId: number): void {
    this.gradingService.getStudents(subjectId).subscribe(
      (students) => (this.students = students),
      (error) => console.error('Error fetching students:', error)
    );
  }

  submitGrade(student: any): void {
    const payload = {
      idProf: student.idProf,
      idStudSub: student.idStudSub,
      studentGrades: student.grade,
      idProfStudSub: student.idProfStudSub,
    };

    this.gradingService.submitGrade(payload).subscribe(
      () => {
        console.log('Grade submitted successfully');
        this.listResults();
      },
      (error) => console.error('Error submitting grade:', error)
    );
  }

  listResults(): void {
    this.gradingService.listResults().subscribe(
      (results) => (this.results = results),
      (error) => console.error('Error fetching results:', error)
    );
  }
}

