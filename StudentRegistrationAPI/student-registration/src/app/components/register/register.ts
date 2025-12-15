import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { StudentService } from '../../services/student-service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  email = signal('');
  password = signal('');
  error = signal('');
  success = signal('');

  constructor(private service: StudentService, public router: Router) {}

  register() {
    // Basic validation
    if (!this.email() || !this.password()) {
      this.error.set('Please enter both email and password.');
      return;
    }

    this.service.register(this.email(), this.password()).subscribe({
      next: (res) => {
        this.error.set(''); // Clear errors
        this.success.set('Registration successful! Redirecting to login...');
        
        // Wait 1.5 seconds so user sees message, then go to login
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 1500);
      },
      error: (err) => {
        console.error('Register failed:', err);
        console.error('Error Body:', err.error);
        
        // Display specific error from API if available, else generic
        const msg = err.error?.message || 'Registration failed. Try again.';
        this.error.set(msg);
      }
    });
  }
}