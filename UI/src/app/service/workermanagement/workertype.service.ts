import { DecimalPipe } from '@angular/common';
import { Injectable, PipeTransform } from '@angular/core';
import { BehaviorSubject, debounceTime, delay, Observable, of, Subject, switchMap, tap } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';
import { PaginationService } from '../pagination/pagination.service';


export interface GridJsWorkerTypeModel {
  id: number;
  name: string;
  code: string;
  excludegardennameincard: boolean;
  paidweeklyoff: boolean;
  paidholidayapplicable: boolean;
  pfapplicable: boolean;
  deptallowance: boolean;
  isactive: boolean;

  attendenceincentive: boolean;
  description: boolean;
  otapplicable: boolean;

  createdby: boolean;
  createddate: boolean;
  updatedby: boolean;
  updateddate: boolean;
  unitid: boolean;
  enroll: number;
}


interface SearchResult {
  countries: GridJsWorkerTypeModel[];
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

function matches(list: GridJsWorkerTypeModel, term: string, pipe: PipeTransform) {
  return  list.name.toLowerCase().includes(term.toLowerCase())
    || list.code.toLowerCase().includes(term.toLowerCase())
    || list.isactive.toString().toLowerCase().includes(term.toLowerCase())
    || list.excludegardennameincard.toString().toLowerCase().includes(term.toLowerCase())
    || list.paidholidayapplicable.toString().includes(term.toLowerCase())
    || list.pfapplicable.toString().toLowerCase().includes(term.toLowerCase())
    || list.deptallowance.toString().toLowerCase().includes(term.toLowerCase())
    || list.otapplicable.toString().includes(term.toLowerCase())
}


@Injectable({
  providedIn: 'root'
})
export class WorkertypeService {

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
                 this.GetData();
               }
             
               Save(master :any)
               {
                 return this.http.post(this.con.apiurl+"/WorkerBook", master);
               }
             
               Update(master :any)
               {
                 return this.http.put<any>(this.con.apiurl+"/WorkerBook?id="+master.ID,master);
               }
             
               Edit(master :any)
               {
                 return this.http.post(this.con.apiurl+"/WorkerBook", master);
               }
             
               Delete(id :number,unitid:any)
               {
                 return this.http.delete<any>(this.con.apiurl+"/WorkerBook?id="+id+"&unitid="+unitid);
               }
             
               GetDataByID(id:number) : Observable<any[]>{
                 return this.http.get<any[]>(this.con.apiurl+"/WorkerType/GetDataByID/"+id);
               }
             
               GetData(){
                this.http.get<any[]>(this.con.apiurl+"/WorkerType?unitid="+localStorage.getItem('LoginUnit')+"&enroll="+localStorage.getItem('Enroll')).subscribe((data: any[])=>{
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
                 countries = countries.filter((data: GridJsWorkerTypeModel) => matches(data, searchTerm, this.pipe));
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
