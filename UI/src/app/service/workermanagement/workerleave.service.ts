//import { Injectable } from '@angular/core';
import { Injectable, PipeTransform } from '@angular/core';
import { BehaviorSubject, debounceTime, delay, Observable, of, Subject, switchMap, tap } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';
import { DecimalPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { PaginationService } from '../pagination/pagination.service';

export interface GridleaveJsModel {
  id: number;
  leavecode: string;
  attendancename: string;
  maxallowed: number;
  attendance: number;
  enroll: number;
  unitid: number;
}

interface SearchResult {
  countries: GridleaveJsModel[];
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

function matches(list: GridleaveJsModel, term: string, pipe: PipeTransform) {
  return  list.leavecode.toLowerCase().includes(term.toLowerCase())
    || list.attendancename.toLowerCase().includes(term.toLowerCase())
    || list.maxallowed.toString().toLowerCase().includes(term.toLowerCase())
    || list.attendance.toString().toLowerCase().includes(term.toLowerCase())
}
@Injectable({
  providedIn: 'root'
})
export class WorkerleaveService {
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
               
               GetDataByID(id:number) : Observable<any[]>{
                return this.http.get<any[]>(this.con.apiurl+"/WorkLeave/GetDataByID/"+id);
              }
               constructor(private pipe: DecimalPipe,private con: ConnectionService,private http:HttpClient,private pagination: PaginationService) { 
                 this.GetData();
               }
               WLCreate(master :any)
             {
               return this.http.post(this.con.apiurl+"/WorkLeave", master);
             }
             Update(master :any)
             {
               return this.http.put<any>(this.con.apiurl+"/WorkLeave?id="+master.ID,master);
             }
             Delete(id :number,unitid:any)
             {
               return this.http.delete<any>(this.con.apiurl+"/WorkLeave?id="+id+"&unitid="+unitid);
             }
             GetData(){
              this.http.get<any[]>(this.con.apiurl+"/WorkLeave?unitid="+localStorage.getItem('LoginUnit')+"&enroll="+localStorage.getItem('Enroll')).subscribe((data: any[])=>{
                 this._search$.pipe(
                   tap(() => this._loading$.next(true)),
                   debounceTime(200),
                   switchMap(() => this._search(data)),
                   delay(200),
                   tap(() => this._loading$.next(false))
                 ).subscribe(result => {
                   this._countries$.next(result.countries);
                   this._total$.next(result.total);
                 });
             
                 this._search$.next()
               })  
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
                            countries = countries.filter((data: GridleaveJsModel) => matches(data, searchTerm, this.pipe));
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
