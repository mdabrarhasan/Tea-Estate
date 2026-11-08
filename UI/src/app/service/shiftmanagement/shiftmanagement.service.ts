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
export class ShiftmanagementService {
  private _loading$ = new BehaviorSubject<boolean>(true);
  constructor(
    private pipe: DecimalPipe,
    private con: ConnectionService,
    private http: HttpClient,
    private pagination: PaginationService,
  ) {
    this.GetData();
  }

  Save(master: any) {
    console.log(master);
    return this.http.post(this.con.apiurl + '/ShiftManagement', master);
  }

  Update(master: any) {
    return this.http.put<any>(
      this.con.apiurl + '/ShiftManagement?id=' + master.ID,
      master,
    );
  }

  Edit(master: any) {
    return this.http.post(this.con.apiurl + '/ShiftManagement', master);
  }

  Delete(id: number, unitid: any) {
    return this.http.delete<any>(
      this.con.apiurl + '/ShiftManagement?id=' + id + '&unitid=' + unitid,
    );
  }

  GetDataByID(id: number): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/ShiftManagement/GetDataByID/' + id,
    );
  }

  GetData() {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/ShiftManagement?unitid=' +
        localStorage.getItem('LoginUnit') +
        '&enroll=' +
        localStorage.getItem('Enroll'),
    );
  }
}
