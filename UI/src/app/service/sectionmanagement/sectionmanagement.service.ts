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
  id: number;
  sectionname: string;
  code: string;
  area: number;
  divisionid: number;
  divisionname: string;
  costcentretype: string;
  slno: number;
  pluckingid: number;
  pluckingsec: string;

  irrigationsection: string;
  irrigationid: number;

  usualcommandarea: string;
  usualsettings: string;
  prunetypeid: number;
  prunetype: string;
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

@Injectable({
  providedIn: 'root',
})
export class SectionmanagementService {
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

  constructor(
    private pipe: DecimalPipe,
    private con: ConnectionService,
    private http: HttpClient,
    private pagination: PaginationService,
  ) {
    this.GetData();
  }

  Save(master: any) {
    return this.http.post(this.con.apiurl + '/SectionManagement', master);
  }

  Update(master: any) {
    return this.http.put<any>(
      this.con.apiurl + '/SectionManagement?id=' + master.ID,
      master,
    );
  }

  Edit(master: any) {
    return this.http.post(this.con.apiurl + '/SectionManagement', master);
  }

  Delete(id: number, unitid: any) {
    return this.http.delete<any>(
      this.con.apiurl + '/SectionManagement?id=' + id + '&unitid=' + unitid,
    );
  }

  GetDataByID(id: number): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/SectionManagement/GetDataByID/' + id,
    );
  }

  GetData() {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/SectionManagement?unitid=' +
        localStorage.getItem('LoginUnit') +
        '&enroll=' +
        localStorage.getItem('Enroll'),
    );
  }
}
