import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  private apiUrl = ' https://localhost:7092/api/students';
  
  constructor(private http: HttpClient) {}

  register(email: string, pass: string) {
  return this.http.post<any>('https://localhost:7092/api/auth/register', { 
    email: email, 
    password: pass 
  });
}

  login(email: string, password: string) {
  	return this.http.post<any>('https://localhost:7092/api/auth/login',
    { email, password });
  }

  private getAuthHeaders() {
    const token = localStorage.getItem('token');
    return { headers: { Authorization: `Bearer ${token}` } };
  }

  getStudents(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl, this.getAuthHeaders());
  }

  addStudent(student: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, student, this.getAuthHeaders());
  }

  updateStudent(student: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${student.id}`, student, this.getAuthHeaders());
  }

  deleteStudent(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`, this.getAuthHeaders());
  }
}
