import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { BaseCrudService } from './base-crud.service';
import { Item } from '../models/item';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ItemService extends BaseCrudService<Item> {

  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Items`);
  }

  getById(id: number): Observable<Item> {
    return this.http.get<Item>(`${this.apiUrl}/` + id);
  }
}
