import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/Categoreis`;

  get(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiUrl}`);
  }

  create(data: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, data);
  }

  update(id: number, data: FormData) {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }

  updateStatus(id: number): Observable<Category> {
    return this.http.put<Category>(`${this.apiUrl}/status/${id}`, {});
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
