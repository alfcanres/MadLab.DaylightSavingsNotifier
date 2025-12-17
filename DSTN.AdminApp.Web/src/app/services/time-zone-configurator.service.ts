import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { EmptyApiResponse } from '../models/empty-api-response.model';
import { ServiceResult, ResultStatus } from '../models/service-result.model';
import { ApiResponse } from '../models/api-response.model';
import { PagedListResponse } from '../models/paged-list-response.model';
import { environment } from '../../environments/environment.development';
import { AddTimeZoneToObserve } from '../models/time-zones/add-time-zone-to-observe.model';
import { ObservedTimeZone } from '../models/time-zones/observed-time-zone.model';
import { ObservedTimeZoneForListParams } from '../models/time-zones/observed-time-zone-for-list-params.model';
import { ObservedTimeZoneForList } from '../models/time-zones/observed-time-zone-for-list.model';
import { EditTimeZoneToObserve } from '../models/time-zones/edit-time-zone-to-observe.model';
import {
  createServiceResultFromApiResponse,
  createServiceResultFromError,
} from '../models/service-result.helper';
import { SelectListItem } from '../models/select-list-item.model';

@Injectable({
  providedIn: 'root',
})
export class TimeZoneConfiguratorService {
  private readonly baseEndPoint = `${environment.apiUrl}/api/ObservedTimeZones`;

  constructor(private http: HttpClient) {}

  addTimeZoneToObserve(model: AddTimeZoneToObserve): Observable<ServiceResult<ObservedTimeZone>> {
    return this.http.post<ApiResponse<ObservedTimeZone>>(this.baseEndPoint, model).pipe(
      map(createServiceResultFromApiResponse),
      catchError((error) =>
        throwError(() => createServiceResultFromError<ObservedTimeZone>(error.message))
      )
    );
  }

  deleteZoneToObserve(id: number): Observable<ServiceResult<EmptyApiResponse>> {
    return this.http.delete<ApiResponse<EmptyApiResponse>>(`${this.baseEndPoint}/${id}`).pipe(
      map(createServiceResultFromApiResponse),
      catchError((error) =>
        throwError(() => createServiceResultFromError<EmptyApiResponse>(error.message))
      )
    );
  }

  editTimeZoneToObserve(model: EditTimeZoneToObserve): Observable<ServiceResult<ObservedTimeZone>> {
    return this.http
      .put<ApiResponse<ObservedTimeZone>>(`${this.baseEndPoint}/${model.id}`, model)
      .pipe(
        map(createServiceResultFromApiResponse),
        catchError((error) =>
          throwError(() => createServiceResultFromError<ObservedTimeZone>(error.message))
        )
      );
  }

  getByTimeZoneToObserveId(timeZoneId: number): Observable<ServiceResult<ObservedTimeZone>> {
    return this.http.get<ApiResponse<ObservedTimeZone>>(`${this.baseEndPoint}/${timeZoneId}`).pipe(
      map(createServiceResultFromApiResponse),
      catchError((error) =>
        throwError(() => createServiceResultFromError<ObservedTimeZone>(error.message))
      )
    );
  }

  listObservedTimeZones(
    params: ObservedTimeZoneForListParams
  ): Observable<ServiceResult<PagedListResponse<ObservedTimeZoneForList>>> {
    // Build query string from params
    const queryParams = [];
    if (params.displayName)
      queryParams.push(`DisplayName=${encodeURIComponent(params.displayName)}`);
    if (params.isActive !== undefined && params.isActive !== null)
      queryParams.push(`IsActive=${params.isActive}`);
    if (params.timeZoneId) queryParams.push(`TimeZoneId=${encodeURIComponent(params.timeZoneId)}`);
    queryParams.push(`RecordsPerPage=${params.recordsPerPage}`);
    queryParams.push(`CurrentPage=${params.currentPage}`);
    const url = `${this.baseEndPoint}?${queryParams.join('&')}`;

    return this.http.get<ApiResponse<PagedListResponse<ObservedTimeZoneForList>>>(url).pipe(
      map(createServiceResultFromApiResponse),
      catchError((error) =>
        throwError(() => createServiceResultFromError<ObservedTimeZoneForList>(error.message))
      )
    );
  }

  getAllForSelect(): Observable<ServiceResult<SelectListItem[]>> {
    
    let response = this.http.get<ApiResponse<ObservedTimeZone[]>>(
      `${this.baseEndPoint}/active-only`
    );

    return response.pipe(
      map((apiResponse) => {
        const selectListItems: SelectListItem[] = apiResponse.data.map((otz) => ({
          value: otz.timeZoneId,
          text: otz.displayName,
        }));
        return {
          data: selectListItems,
          status: ResultStatus.Success,
          messages: [],
        } as ServiceResult<SelectListItem[]>;
      }),
      catchError((error) =>
        throwError(() => createServiceResultFromError<SelectListItem[]>(error.message))
      )
    );
  }
}
