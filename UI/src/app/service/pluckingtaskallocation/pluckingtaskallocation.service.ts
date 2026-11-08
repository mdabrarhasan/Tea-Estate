import {
  BehaviorSubject,
  debounceTime,
  delay,
  Observable,
  of,
  Subject,
  switchMap,
  tap,
} from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ConnectionService } from '../connection/connection.service';
import { Injectable, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';
@Injectable({
  providedIn: 'root',
})
export class PluckingtaskallocationService {
  constructor(
    private pipe: DecimalPipe,
    private con: ConnectionService,
    private http: HttpClient,
  ) {}

  GetDataByID(id: number, date: string): Observable<any[]> {
    const unitid = localStorage.getItem('LoginUnit');
    const url = `${this.con.apiurl}/PluckingAllocation/GetDataByID`;
    const params = new HttpParams()
      .set('unitid', unitid ? unitid : '0')
      .set('id', id)
      .set('date', date);

    return this.http.get<any[]>(url, { params });
  }

  Save(pl: any, Master: any) {
    const request = {
      Master: Master,
      pl: pl,
    };
    return this.http.post(
      this.con.apiurl + '/PluckingAllocation/Save',
      request,
    );
  }
}
