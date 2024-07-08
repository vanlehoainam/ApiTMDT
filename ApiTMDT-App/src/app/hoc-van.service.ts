import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HocVanService {

  private apiUrl = 'https://localhost:8080/api/HocVan/GetAll';

  constructor(private http: HttpClient) { }

  getHocVans(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}
