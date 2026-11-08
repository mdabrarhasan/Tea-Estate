// pagination.service.ts
import { DecimalPipe } from '@angular/common';
import { Injectable, PipeTransform } from '@angular/core';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';



    
    interface SearchResult {
      countries: any;
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
    
    function matches(list: any, term: string, pipe: PipeTransform) {
      return  list.name.toLowerCase().includes(term.toLowerCase())
        || list.code.toLowerCase().includes(term.toLowerCase())
    }

    @Injectable({
        providedIn: 'root',
    })

export class PaginationService {
    direction: any = 'asc';

  private _state: State = {
    page: 1,
    pageSize: 5,
    searchTerm: '',
    startIndex: 0,
    endIndex: 9,
    totalRecords: 0
  };

    // Pagination
    changePage(alldata: any[]) {
        const startItem = (this.page - 1) * this.pageSize + 1;
        const endItem = (this.page - 1) * this.pageSize + this.pageSize;
        this.endIndex = endItem;
        if (this.endIndex > alldata.length) {
            this.endIndex = alldata.length;
        }
        return alldata.slice(startItem - 1, endItem);
    }

    // Sort Data
    onSort(column: any, dataList: any[]) {
        if (this.direction == 'asc') {
            this.direction = 'desc';
        } else {
            this.direction = 'asc';
        }
        const sortedArray = [...dataList]; // Create a new array
        sortedArray.sort((a, b) => {
            const res = this.compare(a[column], b[column]);
            return this.direction === 'asc' ? res : -res;
        });
        return dataList = sortedArray;
    }

    compare(v1: string | number, v2: string | number) {
        return v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
    }

    private _search$ = new Subject<void>();
    public _countries$ = new BehaviorSubject<any[]>([]);
    private _total$ = new BehaviorSubject<number>(0);
    private _loading$ = new BehaviorSubject<boolean>(true);

  constructor(private pipe: DecimalPipe) { }

  private _set(patch: Partial<State>) {
    Object.assign(this._state, patch);
    this._search$.next();
  }

  private _search(data:any): Observable<SearchResult> {
    const { pageSize, page, searchTerm } = this._state;
    // 1. sort
    let countries = data;

    // 2. filter
   // countries = countries.filter((data: any) => matches(data, searchTerm, this.pipe));
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
