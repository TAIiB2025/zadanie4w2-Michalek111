import { Injectable } from '@angular/core';
import { Usluga } from '../models/usluga';
import { Observable, catchError, of, throwError } from 'rxjs';
import { UslugaBody } from '../models/usluga-body';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ListaService {
  private apiUrl = 'http://localhost:5009/api/uslugi'; 

  constructor(private http: HttpClient) {}

  get(searchTerm: string = ''): Observable<Usluga[]> {
    return this.http.get<Usluga[]>(`${this.apiUrl}?search=${searchTerm}`)
      .pipe(catchError(this.handleError));
  }

  getByID(id: number): Observable<Usluga> {
    return this.http.get<Usluga>(`${this.apiUrl}/${id}`);
  }

  post(body: UslugaBody): Observable<Usluga> {
    return this.http.post<Usluga>(this.apiUrl, body)
      .pipe(catchError(this.handleError));
  }
  
  put(id: number, body: UslugaBody): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, body)
      .pipe(catchError(this.handleError));
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Wystąpił błąd!';
    
    if (error.status === 400 || error.status === 500) {
      errorMessage = error.error.errors ? Object.values(error.error.errors).join(', ') : error.error.message;
    }

    console.error('Błąd API:', errorMessage);
    return throwError(() => new Error(errorMessage));
  }

  
}
