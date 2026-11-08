// import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root'
// })
// export class NonpluckingactivitiesService {

//   constructor() { }
// }

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

//const compare = (v1: string | number, v2: string | number) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;

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
export class NonpluckingactivitiesService {
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
        '/DailyNonPlucking?id=' +
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
  ) {
    //this.GetDailyPlucking(pluckingdate,kamjaritypeid, bookid : number,operatorid : number, deviceid : number, batchid : number,weighmentid: number, divisionid : number);
  }

  GetDataByID(id: number, pluckingdate: string): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.con.apiurl}/DailyNonPlucking/GetDataByID/${id}/${pluckingdate}/${localStorage.getItem('Enroll')}/${localStorage.getItem('LoginUnit')}`,
    );
  }

  GetWeighmentData(pluckingdate: string): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.con.apiurl}/DailyNonPlucking/GetQtyWeighmentWise/${pluckingdate}/${localStorage.getItem('LoginUnit')}`,
    );
  }

  GetDataEmpCountDateWise(pluckingdate: string): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.con.apiurl}/DailyNonPlucking/GetDataEmpCountDateWise/${pluckingdate}/${localStorage.getItem('LoginUnit')}`,
    );
  }

  GetDailyPlucking(
    pluckingdate: string,
    kamjaritypeid: number | null,
    bookid: number | null,
    operatorid: number | null,
    deviceid: number | null,
    batchid: number | null,
    recordid: number | null,
    divisionid: number | null,
  ): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/DailyNonPlucking?pluckingdate=' +
        pluckingdate +
        '&&kamjaritypeid=' +
        kamjaritypeid +
        '&&bookid=' +
        bookid +
        '&&operatorid=' +
        operatorid +
        '&&batchid=' +
        batchid +
        '&&deviceid=' +
        deviceid +
        '&&recordid=' +
        recordid +
        '&&divisionid=' +
        divisionid +
        '&&unitid=' +
        localStorage.getItem('LoginUnit') +
        '&&enroll=' +
        localStorage.getItem('Enroll'),
    );
  }

  Save(master: any) {
    return this.http.post(this.con.apiurl + '/DailyNonPlucking', master);
  }
  KamjariUpdate(id: any, kamjariid: any, Teamid: any) {
    return this.http.post(this.con.apiurl + '/DailyNonPlucking/KamjariUpdate', {
      id: id,
      kamjariid: kamjariid,
      Teamid: Teamid,
    });
  }
  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  private _search(data: any): Observable<SearchResult> {
    const { pageSize, page, searchTerm } = this._state;

    // 1. sort
    let countries = data;

    // 2. filter
    countries = countries.filter((data: GridJsModel) =>
      matches(data, searchTerm, this.pipe),
    );
    const total = countries.length;

    // 3. paginate
    this.totalRecords = countries.length;
    this._state.startIndex = (page - 1) * this.pageSize + 1;
    this._state.endIndex = (page - 1) * this.pageSize + this.pageSize;
    if (this.endIndex > this.totalRecords) {
      this.endIndex = this.totalRecords;
    }
    countries = countries.slice(
      this._state.startIndex - 1,
      this._state.endIndex,
    );
    return of({ countries, total });
  }

  get countries$() {
    return this._countries$.asObservable();
  }
  get total$() {
    return this._total$.asObservable();
  }
  get loading$() {
    return this._loading$.asObservable();
  }
  get page() {
    return this._state.page;
  }
  get pageSize() {
    return this._state.pageSize;
  }
  get searchTerm() {
    return this._state.searchTerm;
  }
  get startIndex() {
    return this._state.startIndex;
  }
  get endIndex() {
    return this._state.endIndex;
  }
  get totalRecords() {
    return this._state.totalRecords;
  }

  set page(page: number) {
    this._set({ page });
  }
  set pageSize(pageSize: number) {
    this._set({ pageSize });
  }
  set searchTerm(searchTerm: string) {
    this._set({ searchTerm });
  }
  set startIndex(startIndex: number) {
    this._set({ startIndex });
  }
  set endIndex(endIndex: number) {
    this._set({ endIndex });
  }
  set totalRecords(totalRecords: number) {
    this._set({ totalRecords });
  }
}
