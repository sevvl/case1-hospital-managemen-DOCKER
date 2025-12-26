import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AiPrediction } from '../models/prediction.model';

@Injectable({
  providedIn: 'root'
})
export class AiService {
  private apiUrl = 'http://localhost:5149/api/prediction';

  constructor(private http: HttpClient) {}

  getPrediction(patientId: number): Observable<AiPrediction> {
    return this.http.get<AiPrediction>(`${this.apiUrl}/${patientId}`);
  }
}
