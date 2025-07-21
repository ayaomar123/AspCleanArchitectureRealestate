import { inject, Injectable } from '@angular/core';
import { City } from '../models/city';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/Cities`;

  get(): Observable<City[]> {
    return this.http.get<City[]>(`${this.apiUrl}`);
  }

  create(data: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, data);
  }

  update(id: number, data: FormData) {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }

  updateStatus(id: number): Observable<City> {
    return this.http.put<City>(`${this.apiUrl}/status/${id}`, {});
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
