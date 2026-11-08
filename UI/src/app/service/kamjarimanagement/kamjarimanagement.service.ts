import { DecimalPipe } from '@angular/common';
import { Injectable, PipeTransform } from '@angular/core';
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
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';
import { PaginationService } from '../pagination/pagination.service';

@Injectable({
  providedIn: 'root',
})
export class KamjarimanagementService {
  constructor(
    private con: ConnectionService,
    private http: HttpClient,
    private pagination: PaginationService,
  ) {
    this.GetData();
  }

  Save(master: any) {
    return this.http.post(this.con.apiurl + '/Kamjari', master);
  }

  Update(master: any) {
    return this.http.put<any>(
      this.con.apiurl + '/Kamjari?id=' + master.ID,
      master,
    );
  }

  Edit(master: any) {
    return this.http.post(this.con.apiurl + '/Kamjari', master);
  }

  Delete(id: number, unitid: any, enroll: any) {
    return this.http.delete<any>(
      this.con.apiurl +
        '/Kamjari?id=' +
        id +
        '&unitid=' +
        unitid +
        '&enroll=' +
        enroll,
    );
  }

  GetDataByID(id: number): Observable<any[]> {
    const unitid = localStorage.getItem('LoginUnit');
    const enroll = localStorage.getItem('Enroll');
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Kamjari/GetDataByID/' +
        id +
        '?unitid=' +
        unitid +
        '&enroll=' +
        enroll,
    );
  }

  GetData(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Kamjari?unitid=' +
        localStorage.getItem('LoginUnit') +
        '&enroll=' +
        localStorage.getItem('Enroll'),
    );
  }
}
