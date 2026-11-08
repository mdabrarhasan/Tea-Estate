import { DecimalPipe } from '@angular/common';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';
import { PaginationService } from '../pagination/pagination.service';

@Injectable({
  providedIn: 'root',
})
export class SalaryconfigurationService {
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
    return this.http.post(this.con.apiurl + '/SalaryConfiguration', master);
  }

  Update(master: any) {
    return this.http.put<any>(
      this.con.apiurl + '/SalaryConfiguration?id=' + master.ID,
      master,
    );
  }

  Edit(master: any) {
    return this.http.post(this.con.apiurl + '/SalaryConfiguration', master);
  }

  Delete(id: number, unitid: any, enroll: any) {
    return this.http.delete<any>(
      this.con.apiurl +
        '/SalaryConfiguration?id=' +
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
        '/SalaryConfiguration/GetDataByID/' +
        id +
        '?unitid=' +
        unitid +
        '&enroll=' +
        enroll,
    );
  }

  GetData() {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/SalaryConfiguration?unitid=' +
        localStorage.getItem('LoginUnit') +
        '&enroll=' +
        localStorage.getItem('Enroll'),
    );
  }
}
