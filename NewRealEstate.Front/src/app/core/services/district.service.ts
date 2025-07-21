import { Injectable } from '@angular/core';
import { BaseCrudService } from './base-crud.service';
import { District } from '../models/district';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DistrictService extends BaseCrudService<District> {

  constructor(http: HttpClient) {
    super(http, `${environment.apiUrl}/Districts`);
  }
}
