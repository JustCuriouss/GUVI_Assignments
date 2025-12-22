import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../services/auth-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink, MatCardModule, MatInputModule, MatButtonModule, CommonModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly cdr = inject(ChangeDetectorRef);

  // Variables to hold messages
  successMessage: string = '';
  errorMessage: string = '';
  isSubmitting: boolean = false;

  onRegister(userData: any) {
    // 1. Clear previous messages
    this.successMessage = '';
    this.errorMessage = '';
    this.isSubmitting = true;

    this.authService.register(userData).subscribe({
      next: () => {
        // 2. SUCCESS: Show Green Message
        this.successMessage = 'Registration successful! Redirecting to login...';
        this.cdr.detectChanges();
        // 3. Wait 2 seconds so they can read it, then navigate
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      },
      error: (err: HttpErrorResponse) => {
        // 4. FAILURE: Show Red Message
        this.isSubmitting = false; // Re-enable button
        
        // Your existing error parsing logic
        let msg = 'Registration failed.';
        if (err.error) {
          if (Array.isArray(err.error)) {
            msg = err.error.map((e: any) => e.description).join('\n');
          }
          else if (typeof err.error === 'string') {
            msg = err.error;
          }
          else if (err.error.errors) {
             const validationErrors = Object.values(err.error.errors);
             msg = validationErrors.flat().join('\n');
          }
        }
        this.errorMessage = msg;
        this.cdr.detectChanges();
      }
    });
  }
}