import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable, throwError } from 'rxjs';
import { ServiceResult } from '../models/service-result.model';
import { ApiResponse } from '../models/api-response.model';
import { createServiceResultFromApiResponse, createServiceResultFromError } from '../models/service-result.helper';

@Injectable({
  providedIn: 'root',
})
export class SystemTimeZoneService {
  private readonly baseEndPoint: string = `${environment.apiUrl}/api/SystemTimeZones`;
  private http = inject(HttpClient);
  

  findSystemTimeZoneById(id: string): Observable<ServiceResult<string>> {
    const escapedId = encodeURIComponent(id);

    return this.http.get<ApiResponse<string>>(
      `${this.baseEndPoint}/find-system-timezone-by-id?id=${escapedId}`
    ).pipe(
      map(createServiceResultFromApiResponse),
      catchError((error) =>
        throwError(() => createServiceResultFromError<string>(error.message))
      )
    );

  }
}
