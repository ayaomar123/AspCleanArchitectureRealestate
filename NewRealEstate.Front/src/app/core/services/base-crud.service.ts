import { HttpClient } from '@angular/common/http';
import { inject, Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseCrudService<T> {
  constructor(protected http: HttpClient, @Inject('API_URL') protected apiUrl: string) { }

  get(): Observable<T[]> {
    return this.http.get<T[]>(`${this.apiUrl}`);
  }

  create(data: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, data);
  }

  update(id: number, data: FormData) {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }

  updateStatus(id: number): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}/status/${id}`, {});
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
