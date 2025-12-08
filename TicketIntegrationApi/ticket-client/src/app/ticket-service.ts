import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  private apiUrl = 'http://localhost:5163/api/tickets';
  
  constructor(private http: HttpClient) {}
  getTickets(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

}
