import { DecimalPipe } from '@angular/common';
import { Injectable, PipeTransform } from '@angular/core';
import { BehaviorSubject, debounceTime, delay, Observable, of, Subject, switchMap, tap } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';


export interface GridJsModel {
  id: string;
  name: string;
  email: string;
  position: string;
  company: string;
  country: string;
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

const compare = (v1: string | number, v2: string | number) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;


function matches(country: GridJsModel, term: string, pipe: PipeTransform) {
  return country.id.toLowerCase().includes(term.toLowerCase())
    || country.name.toLowerCase().includes(term.toLowerCase())
    || country.email.toLowerCase().includes(term.toLowerCase())
    || country.position.toLowerCase().includes(term.toLowerCase())
    || country.company.toLowerCase().includes(term.toLowerCase())
    || country.country.toLowerCase().includes(term.toLowerCase())
    ;
}

@Injectable({
  providedIn: 'root'
})
export class CompanymanagementService {
  unitname:string="";
  address:string="";
  email:string="";
  contactnumber:string="";
  replytoemail:string="";

  contactperson:string="";
  roundoffruleid:number=0;

  facerecognizationruleid:number=0;
  unitareapluckedid:number=0;
  unitforleafareaid:number=0;
  unitforpluckerperarea:number=0;


  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _countries$ = new BehaviorSubject<GridJsModel[]>([]);
  private _total$ = new BehaviorSubject<number>(0);

  private _state: State = {
    page: 1,
    pageSize: 5,
    searchTerm: '',
    startIndex: 0,
    endIndex: 9,
    totalRecords: 0
  };
  

  constructor(private pipe: DecimalPipe,private con: ConnectionService,private http:HttpClient) {
    this._search$.pipe(
      tap(() => this._loading$.next(true)),
      debounceTime(200),
      switchMap(() => this._search()),
      delay(200),
      tap(() => this._loading$.next(false))
    ).subscribe(result => {
      this._countries$.next(result.countries);
      this._total$.next(result.total);
    });

    this._search$.next();
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

  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  private _search(): Observable<SearchResult> {
    const { pageSize, page, searchTerm } = this._state;

    // 1. sort
    let countries = GridJs;

    // 2. filter
    countries = countries.filter(country => matches(country, searchTerm, this.pipe));
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
  
  changePage(alldata: any[]) {
    const startItem = (this.page - 1) * this.pageSize + 1;
    const endItem = (this.page - 1) * this.pageSize + this.pageSize;
    this.endIndex = endItem;
    if (this.endIndex > alldata.length) {
        this.endIndex = alldata.length;
    }
    return alldata.slice(startItem - 1, endItem);
}

  GetCompanyInfo() : Observable<any[]>{
    return this.http.get<any[]>(this.con.apiurl+"/CompanyManagement?unitid="+localStorage.getItem('LoginUnit'));
  }

  GetUnitAreaPlucked() : Observable<any[]>{
    return this.http.get<any[]>(this.con.apiurl+"/Common/GetUnitAreaPlucked");
  }

  GetFaceRecognization() : Observable<any[]>{
    return this.http.get<any[]>(this.con.apiurl+"/Common/GetFaceRecognization");
  }

  GetRoundOffRule() : Observable<any[]>{
    return this.http.get<any[]>(this.con.apiurl+"/Common/GetRoundOffRule");
  }
}

const GridJs = [
  {
    id: '01',
    name: 'Jonathan',
    email: 'jonathan@example.com',
    position: 'Senior Implementation Architect',
    company: 'Hauck Inc',
    country: 'Holy See',
  },
  {
    id: '02',
    name: 'Harold',
    email: 'harold@example.com',
    position: 'Forward Creative Coordinator',
    company: 'Metz Inc',
    country: 'Iran',
  },
  {
    id: '03',
    name: 'Shannon',
    email: 'shannon@example.com',
    position: 'Legacy Functionality Associate',
    company: 'Zemlak Group',
    country: 'South Georgia',
  },
  {
    id: '04',
    name: 'Robert',
    email: 'robert@example.com',
    position: 'Product Accounts Technician',
    company: 'Hoeger',
    country: 'San Marino',
  },
  {
    id: '05',
    name: 'Noel',
    email: 'noel@example.com',
    position: 'Customer Data Director',
    company: 'Howell - Rippin',
    country: 'Germany',
  },
  {
    id: '06',
    name: 'Traci',
    email: 'traci@example.com',
    position: 'Corporate Identity Director',
    company: 'Koelpin - Goldner',
    country: 'Vanuatu',
  },
  {
    id: '07',
    name: 'Kerry',
    email: 'kerry@example.com',
    position: 'Lead Applications Associate',
    company: 'Feeney, Langworth and Tremblay',
    country: 'Niger',
  },
  {
    id: '08',
    name: 'Patsy',
    email: 'patsy@example.com',
    position: 'Dynamic Assurance Director',
    company: 'Streich Group',
    country: 'Niue',
  },
  {
    id: '09',
    name: 'Cathy',
    email: 'cathy@example.com',
    position: 'Customer Data Director',
    company: 'Ebert, Schamberger and Johnston',
    country: 'Mexico',
  },
  {
    id: '10',
    name: 'Tyrone',
    email: 'tyrone@example.com',
    position: 'Senior Response Liaison',
    company: 'Raynor, Rolfson and Daugherty',
    country: 'Qatar',
  }
];

