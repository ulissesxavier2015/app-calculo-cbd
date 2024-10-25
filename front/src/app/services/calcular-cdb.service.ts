import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CalcularCdb } from '../models/calcular-cdb.model';
import { Observable } from 'rxjs';
import { ResultadoCalculoCdbDto } from '../models/resultado-calculo-cdb-dto.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class CalcularCdbService {

  constructor(private http: HttpClient) { }

  calcularCdb(calcularCdb: CalcularCdb): Observable<ResultadoCalculoCdbDto> {
    return this.http.post<ResultadoCalculoCdbDto>(`${environment.serviceUrl}/calcular-cdb`, calcularCdb);
  }
}
