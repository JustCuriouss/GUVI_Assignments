import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { StudentService } from '../../services/student-service';
import { Student } from '../../Models/Student';

@Component({
  selector: 'app-students',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './students.html',
  styleUrl: './students.css',
})
export class Students {
  // Signals for state management
  students = signal<Student[]>([]);
  selectedStudent = signal<Student>({ id: 0, name: '', class: '', section: '' });

  constructor(private studentService: StudentService) {
    this.loadStudents();
  }

  loadStudents() {
    this.studentService.getStudents().subscribe(data => {
      this.students.set(data);
    });
  }

  saveStudent() {
    const student = this.selectedStudent();

    if (student.id === 0) {
      this.studentService.addStudent(student).subscribe(() => {
        this.loadStudents();
        this.resetForm();
      });
    } else {
      this.studentService.updateStudent(student).subscribe(() => {
        this.loadStudents();
        this.resetForm();
      });
    }
  }

  editStudent(s: Student) {
    // Clone object to avoid direct mutation in table
    this.selectedStudent.set({ ...s });
  }

  deleteStudent(id: number) {
    if(confirm('Are you sure you want to delete this student?')) {
      this.studentService.deleteStudent(id).subscribe(() => this.loadStudents());
    }
  }

  resetForm() {
    this.selectedStudent.set({ id: 0, name: '', class: '', section: '' });
  }
}