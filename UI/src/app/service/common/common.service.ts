import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';

export interface FilterItem {
  id: number;
  name: string;
}

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  constructor(
    private http: HttpClient,
    private con: ConnectionService,
  ) {}

  GetReport(reportCode: number): Observable<any[]> {
    return this.http.get<any>(`${this.con.apiurl}/Report?id=${reportCode}`);
  }

  GetReportWS(reportCode: number): Observable<any[]> {
    return this.http.get<any>(
      `${this.con.apiurl}/WagesSummaryReport?id=${reportCode}`,
    );
  }

  GetUnit(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/Common/GetUnit/' + localStorage.getItem('LoginUnit'),
    );
  }

  GetWeeklyHoliday(): Observable<any[]> {
    return this.http.get<any[]>(this.con.apiurl + '/Common/GetWeeklyHoliday');
  }

  GetPLKInterval(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/Common/GetPluckingInterval',
    );
  }

  GetPruneType(): Observable<any[]> {
    return this.http.get<any[]>(this.con.apiurl + '/Common/GetPruneType');
  }

  GetIsActive(): Observable<any[]> {
    return this.http.get<any[]>(this.con.apiurl + '/Common/GetIsActive');
  }

  GetDivision(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetDivision/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetWorkerGrouplist(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetWorkerGrouplist/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetJobStation(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetJobStation/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetKamjari(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetKamjariType/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetMasterTypes(): Observable<any[]> {
    return this.http.get<any[]>(this.con.apiurl + '/Common/GetMasterTypes');
  }

  GetKamjariData(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetKamjari/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetKamjariforUnit(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetKamjariforUnit/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetAttendance(): Observable<any[]> {
    return this.http.get<any[]>(this.con.apiurl + '/Common/GetAttendance');
  }

  GetShit(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/Common/GetShift/' + localStorage.getItem('LoginUnit'),
    );
  }

  GetGender(): Observable<any[]> {
    return this.http.get<any[]>(this.con.apiurl + '/Common/GetGender');
  }

  GetWorkerType(): Observable<any[]> {
    return this.http.get<any[]>(this.con.apiurl + '/Common/GetWorkerType');
  }

  GetBook(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/Common/GetBook/' + localStorage.getItem('LoginUnit'),
    );
  }

  GetDate(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/Common/GetDate/' + localStorage.getItem('LoginUnit'),
    );
  }

  GetEmployee(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetEmployee/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetAttendanceTypeForReport(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/Common/GetAttendanceTypeForReport',
    );
  }

  GetSection(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetSection/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetBatch(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/Common/GetBatch/' + localStorage.getItem('LoginUnit'),
    );
  }

  GetWeighment(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetWeighment/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetDevice(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetDevice/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetOperator(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetOperator/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetDevicelist(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetDevice/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetWorkerlist(): Observable<any[]> {
    return this.http.get<any[]>(this.con.apiurl + '/Common/GetWorkerlist');
  }

  GetLeaveTypelist(): Observable<any[]> {
    return this.http.get<any[]>(this.con.apiurl + '/Common/GetLeaveTypelist');
  }

  GetLeaveTypeValuelist(valueget: any): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.con.apiurl}/Common/GetLeaveTypeValuelist/${valueget}/${localStorage.getItem('LoginUnit')}`,
    );
  }

  GetReportListMaster(categoryId: number): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.con.apiurl}/Common/GetReportListMaster/${categoryId}`,
    );
  }

  GetCategoryListMaster(): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.con.apiurl}/Common/GetCategoryListMaster`,
    );
  }

  GetReportListTransaction(categoryId: number): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.con.apiurl}/Common/GetReportListTransaction/${categoryId}`,
    );
  }

  GetCategoryListTransaction(): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.con.apiurl}/Common/GetCategoryListTransaction`,
    );
  }

  GetReportFilters(): Observable<any[]> {
    return this.http.get<any[]>(`${this.con.apiurl}/Common/GetReportFilters`);
  }

  GetReportAssignedFilters(reportId: number): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.con.apiurl}/Common/GetReportAssignedFilters/${reportId}`,
    );
  }

  // NEW METHOD
  GetLeftFilterData(filterId: number): Observable<FilterItem[]> {
    const unitId = Number(localStorage.getItem('LoginUnit'));

    return this.http.get<FilterItem[]>(
      `${this.con.apiurl}/Common/GetLeftFilterData/${filterId}/${unitId}`,
    );
  }

  GetBookPermanent(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetBookPermanent/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetEmployeePermanent(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetEmployeePermanent/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetShiftForReport(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl +
        '/Common/GetShiftForReport/' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetLeaveTypeForReport(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/Common/GetLeaveTypeForReport',
    );
  }
}
