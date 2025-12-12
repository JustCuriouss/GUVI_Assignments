import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { StudentService } from '../student-service';
import { Router } from '@angular/router';
import { Student } from '../Student';

@Component({
  selector: 'app-add-student',
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './add-student.html',
  styleUrl: './add-student.css',
})
export class AddStudent {

  studentForm!: FormGroup;
  classMessage = "";

  constructor(
    private fb: FormBuilder,
    private studentService: StudentService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.studentForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(5)]],
      classValue: ['', Validators.required],
      gender: ['', Validators.required],
      hasHobby: [false],
      hobby: [''],
      favouriteSubject: ['']
    });

    this.studentForm.get('classValue')?.valueChanges.subscribe(value => {
      if (value === '9') {
        this.classMessage = "You will appear in board exams soon. All the Best !!";
      } else if (value === '6') {
        this.classMessage = "Welcome to middle school!";
      } else if (value) {
        this.classMessage = "Education and hobby go hand in hand!";
      }
    });

    this.studentForm.get('hasHobby')?.valueChanges.subscribe(chk => {
      if (!chk) this.studentForm.get('hobby')?.setValue('');
    });
  }

  save(): void {
    const f = this.studentForm.value;

    const student = new Student(
      f.name!,
      f.classValue!,
      f.gender!,
      f.hasHobby!,
      f.hobby!,
      f.favouriteSubject!
    );

    this.studentService.addStudent(student);
    this.router.navigate(['/home']);
  }
}
