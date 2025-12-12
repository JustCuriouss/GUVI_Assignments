import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Student } from '../Student';
import { StudentService } from '../student-service';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  students: Student[] = [];

  constructor(private studentService: StudentService) {}

  ngOnInit(): void {
    this.students = this.studentService.getStudents();
  }
}