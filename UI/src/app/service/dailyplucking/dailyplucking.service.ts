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

export interface GridJsModel {
  id: string;
  name: string;
  kamjaritype: string;
  bookempno: string;
  batch: string;
  shift: string;
  total: string;
  attendent: string;
  pluckeddate: string;
  isselected: boolean;
  unitid: number;
}

interface SearchResult {
  countries: GridJsModel[];
  total: number;
}

interface State {
  page: number;
  pageSize: number;
  searchTerm: string;
  startIndex: number;
  endIndex: number;
  totalRecords: number;
}

function matches(list: GridJsModel, term: string, pipe: PipeTransform) {
  return (
    list.name.toLowerCase().includes(term.toLowerCase()) ||
    list.kamjaritype.toLowerCase().includes(term.toLowerCase()) ||
    list.bookempno.toLowerCase().includes(term.toLowerCase()) ||
    list.batch.toLowerCase().includes(term.toLowerCase()) ||
    list.shift.toLowerCase().includes(term.toLowerCase()) ||
    list.total.toLowerCase().includes(term.toLowerCase()) ||
    list.attendent.toLowerCase().includes(term.toLowerCase()) ||
    list.pluckeddate.toLowerCase().includes(term.toLowerCase())
  );
}

@Injectable({
  providedIn: 'root',
})
export class DailypluckingService {
  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _countries$ = new BehaviorSubject<any[]>([]);
  private _total$ = new BehaviorSubject<number>(0);

  private _state: State = {
    page: 1,
    pageSize: 5,
    searchTerm: '',
    startIndex: 0,
    endIndex: 9,
    totalRecords: 0,
  };

  Delete(id: number) {
    return this.http.delete<any>(
      this.con.apiurl +
        '/DailyPlucking?id=' +
        id +
        '&&enroll=' +
        localStorage.getItem('Enroll') +
        '&unitid=' +
        localStorage.getItem('LoginUnit'),
    );
  }

  constructor(
    private pipe: DecimalPipe,
    private con: ConnectionService,
    private http: HttpClient,
    private pagination: PaginationService,
  ) {}

  UpdateWeighment(id: number, weighment: number): Observable<any> {
    const unitid = localStorage.getItem('LoginUnit');
    const enroll = localStorage.getItem('Enroll');

    return this.http.get<any>(
      `${this.con.apiurl}/DailyPlucking/UpdateWeighment?id=${id}&weighment=${weighment}&unitid=${unitid}&enroll=${enroll}`,
    );
  }

  GetDataByID(id: number, pluckingdate: string): Observable<any[]> {
    return this.http.get<any[]>(
      `${
        this.con.apiurl
      }/DailyPlucking/GetDataByID/${id}/${pluckingdate}/${localStorage.getItem(
        'Enroll',
      )}/${localStorage.getItem('LoginUnit')}`,
    );
  }

  GetWeighmentData(pluckingdate: string, bookid: number): Observable<any[]> {
    return this.http.get<any[]>(
      `${
        this.con.apiurl
      }/DailyPlucking/GetQtyWeighmentWise/${pluckingdate}/${bookid}/${localStorage.getItem(
        'LoginUnit',
      )}`,
    );
  }

  GetDataEmpCountDateWise(
    pluckingdate: string,
    bookid: number,
  ): Observable<any[]> {
    return this.http.get<any[]>(
      `${
        this.con.apiurl
      }/DailyPlucking/GetDataEmpCountDateWise/${pluckingdate}/${bookid}/${localStorage.getItem(
        'LoginUnit',
      )}`,
    );
  }

  GetDailyPlucking(
    pluckingdate: string,
    kamjaritypeid: number = 0,
    bookid: number = 0,
    operatorid: number = 0,
    deviceid: number = 0,
    batchid: number = 0,
    weighmentid: number = 0,
    divisionid: number = 0,
  ): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/DailyPlucking?pluckingdate=' +
        pluckingdate +
        '&&kamjaritypeid=' +
        kamjaritypeid +
        '&&bookid=' +
        bookid +
        '&&operatorid=' +
        operatorid +
        '&&deviceid=' +
        deviceid +
        '&&batchid=' +
        batchid +
        '&&deviceid=' +
        deviceid +
        '&&weighmentid=' +
        weighmentid +
        '&&divisionid=' +
        divisionid +
        '&&unitid=' +
        localStorage.getItem('LoginUnit') +
        '&&enroll=' +
        localStorage.getItem('Enroll'),
    );
  }

  Save(master: any) {
    return this.http.post(this.con.apiurl + '/DailyPlucking', master);
  }
}
