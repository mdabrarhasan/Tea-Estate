import { DecimalPipe } from '@angular/common';
import { Injectable, PipeTransform } from '@angular/core';
import { BehaviorSubject, debounceTime, delay, Observable, of, Subject, switchMap, tap } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';
import { PaginationService } from '../pagination/pagination.service';
import { Console } from 'console';


export interface GridJsModel {
  id: string;
  name: string;
  code: string;
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
  return  list.name.toLowerCase().includes(term.toLowerCase())
    || list.code.toLowerCase().includes(term.toLowerCase())
}

@Injectable({
  providedIn: 'root'
})
export class DivisionmanagementService {

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
    totalRecords: 0
  };
  

  constructor(private pipe: DecimalPipe,private con: ConnectionService,private http:HttpClient,private pagination: PaginationService) { 
    this.GetDivisionInfo();
  }

  Save(master :any)
  {
    return this.http.post(this.con.apiurl+"/Division", master);
  }

  Update(master :any)
  {
    return this.http.put<any>(this.con.apiurl+"/Division?id="+master.ID,master);
  }


  Edit(master :any)
  {
    return this.http.post(this.con.apiurl+"/Division", master);
  }

  Delete(id :number)
  {
    return this.http.delete<any>(this.con.apiurl+"/Division?id="+id);
  }

  GetDataByID(id:number) : Observable<any[]>{
    return this.http.get<any[]>(this.con.apiurl+"/Division/GetDataByID/"+id);
  }

  GetDivisionInfo() : Observable<any[]>{
   return this.http.get<any[]>(this.con.apiurl+"/Division?unitid="+localStorage.getItem('LoginUnit'))
  }
  

  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  private _search(data:any): Observable<SearchResult> {

    const { pageSize, page, searchTerm } = this._state;

    // 1. sort
    let countries = data;

    // 2. filter
    countries = countries.filter((data: GridJsModel) => matches(data, searchTerm, this.pipe));
    const total = countries.length;

    // 3. paginate
    this.totalRecords = countries.length;
    this._state.startIndex = (page - 1) * this.pageSize + 1;
    this._state.endIndex = (page - 1) * this.pageSize + this.pageSize;
    if (this.endIndex > this.totalRecords) {
      this.endIndex = this.totalRecords;
    }
    countries = countries.slice(this._state.startIndex - 1, this._state.endIndex);
    return of({ countries, total });
  }

  get countries$() { return this._countries$.asObservable(); }
  get total$() { return this._total$.asObservable(); }
  get loading$() { return this._loading$.asObservable(); }
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get searchTerm() { return this._state.searchTerm; }
  get startIndex() { return this._state.startIndex; }
  get endIndex() { return this._state.endIndex; }
  get totalRecords() { return this._state.totalRecords; }

  set page(page: number) { this._set({ page }); }
  set pageSize(pageSize: number) { this._set({ pageSize }); }
  set searchTerm(searchTerm: string) { this._set({ searchTerm }); }
  set startIndex(startIndex: number) { this._set({ startIndex }); }
  set endIndex(endIndex: number) { this._set({ endIndex }); }
  set totalRecords(totalRecords: number) { this._set({ totalRecords }); }
}
