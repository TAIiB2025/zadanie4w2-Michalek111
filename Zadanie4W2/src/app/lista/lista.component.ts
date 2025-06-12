import { Component, inject } from '@angular/core';
import { ListaService } from '../lista.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { Usluga } from '../../models/usluga';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-lista',
  imports: [CommonModule, RouterLink,FormsModule],
  templateUrl: './lista.component.html',
  styleUrl: './lista.component.css'
})
export class ListaComponent {
  private readonly listaService = inject(ListaService);
  public dane$ = new BehaviorSubject<Usluga[]>([]);  public searchTerm: string = '';
  public errorMessage: string = '';
  public nowaUsluga: Usluga = { id: 0, nazwa: '', wykonawca: '', rodzaj: '', rok: 2025 };

  constructor() {
    this.fetchData();
  }

  fetchData() {
    this.listaService.get(this.searchTerm).subscribe({
      next: data => this.dane$.next(data), 
      error: err => this.errorMessage = err.message
    });
  }
  
  put(id: number, updatedUsluga: Usluga) {
    this.listaService.put(id, updatedUsluga).subscribe({
      next: () => this.fetchData(),
      error: err => this.errorMessage = err.message
    });
  }

  post() {
    this.listaService.post(this.nowaUsluga).subscribe({
      next: (newUsluga) => {
        this.dane$.next([...this.dane$.value, newUsluga]); 
        this.nowaUsluga = { id: 0, nazwa: '', wykonawca: '', rodzaj: '', rok: 2025 }; 
      },
      error: err => this.errorMessage = err.message
    });
  }

  
}
