import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { StudentService } from '../../services/student-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,                 
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  username = signal('');
  password = signal('');
  error = signal('');

  constructor(private service: StudentService, public router: Router) {}

   login() {
    this.service.login(this.username(), this.password()).subscribe({
      next: (res) => {
        localStorage.setItem('token', res.token);
        this.router.navigate(['/components/students']); 
      },
       error: (err) => {
              console.error('Login failed:', err);
              console.error('Status:', err.status);
              console.error('Status Text:', err.statusText);
              console.error('Error Body:', err.error);
              console.error('Full URL:', err.url);
              
              this.error.set('Invalid credentials');
      }
    });
  }
}
