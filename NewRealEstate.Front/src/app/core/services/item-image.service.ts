import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { Image } from '../models/item';

@Injectable({
  providedIn: 'root'
})
export class ItemImageService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/Images`;

  create(data: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, data);
  }

  updateStatus(id: number): Observable<Image> {
    return this.http.put<Image>(`${this.apiUrl}/status/${id}`, {});
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
