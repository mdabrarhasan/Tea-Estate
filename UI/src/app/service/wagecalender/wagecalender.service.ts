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
import { Console } from 'console';

@Injectable({
  providedIn: 'root',
})
export class WagecalenderService {
  constructor(
    private pipe: DecimalPipe,
    private con: ConnectionService,
    private http: HttpClient,
    private pagination: PaginationService,
  ) {
    this.GetData();
  }

  SalaryGenerate(data: any) {
    return this.http.post(
      this.con.apiurl + '/WageCalender/SalaryGenerate',
      data,
    );
  }

  Save(master: any) {
    return this.http.post(this.con.apiurl + '/WageCalender', master);
  }

  Update(master: any) {
    console.log(master);

    return this.http.put<any>(
      this.con.apiurl + '/WageCalender?id=' + master.ID,
      master,
    );
  }

  Edit(master: any) {
    return this.http.post(this.con.apiurl + '/WageCalender', master);
  }

  Delete(id: number, unitid: any) {
    return this.http.delete<any>(
      this.con.apiurl + '/WageCalender?id=' + id + '&unitid=' + unitid,
    );
  }

  // GetDataByID(id: number): Observable<any[]> {
  //   return this.http.get<any[]>(
  //     this.con.apiurl + '/WageCalender/GetDataByID/' + id,
  //   );
  // }

  GetDataByID(id: number, unitid: any): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.con.apiurl}/WageCalender/GetDataByID/${id}/${unitid}`,
    );
  }

  GetData() {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/WageCalender?unitid=' +
        localStorage.getItem('LoginUnit') +
        '&enroll=' +
        localStorage.getItem('Enroll'),
    );
  }
  SalaryCancel(unitid: number = 0, bookid: number = 0) {
    return this.http.delete<any>(
      `${this.con.apiurl}/WageCalender/SalaryCancel/${unitid}/${bookid}`,
    );
  }
}
