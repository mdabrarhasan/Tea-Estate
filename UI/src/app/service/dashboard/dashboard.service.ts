import { Injectable } from '@angular/core';
import { PaginationService } from '../pagination/pagination.service';
import { HttpClient } from '@angular/common/http';
import { ConnectionService } from '../connection/connection.service';
import { DecimalPipe } from '@angular/common';
import { Observable } from 'rxjs';

export interface DashboardStats {
  todayplk: number;
  prevdayplk: number;

  permpresentplk: number;
  permpresentnonplk: number;
  casualpresentplk: number;
  casualpresentnonplk: number;

  permabsent: number;
  casualabsent: number;

  permot: number;
  casualot: number;

  totalbasic: number;
  totalot: number;

  piepresent: number;
  pieabsent: number;
  pieleave: number;
}
export interface SectionPlucking {
  sectioncode: string;
  pluckedqty: number;
}
export interface PluckingTrend {
  pluckdate: string;
  totqty: number;
}

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  constructor(
    private pipe: DecimalPipe,
    private con: ConnectionService,
    private http: HttpClient,
    private pagination: PaginationService,
  ) {}

  GetDashboardStats(pluckingdate: string): Observable<DashboardStats> {
    return this.http.get<DashboardStats>(
      `${this.con.apiurl}/DashBoard/GetDashboardStats/${pluckingdate}/${localStorage.getItem('LoginUnit')}`,
    );
  }

  GetSectionPlucking(pluckingdate: string): Observable<SectionPlucking[]> {
    return this.http.get<SectionPlucking[]>(
      `${this.con.apiurl}/DashBoard/GetSectionPlucking/${pluckingdate}/${localStorage.getItem('LoginUnit')}`,
    );
  }

  GetPluckingTrend(pluckingdate: string): Observable<PluckingTrend[]> {
    return this.http.get<PluckingTrend[]>(
      `${this.con.apiurl}/DashBoard/GetPluckingTrend/${pluckingdate}/${localStorage.getItem('LoginUnit')}`,
    );
  }
}
