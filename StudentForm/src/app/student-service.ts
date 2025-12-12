import { Injectable } from '@angular/core';
import { Student } from './Student';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private students: Student[] = [
    new Student("Rahul", "7", "Male", true, "Cricket", "Maths"),
    new Student("Asha", "8", "Female", false, "", "Science")
  ];

  getStudents(): Student[] {
    return this.students;
  }

  addStudent(student: Student) {
    this.students.push(student);
  }
}
