import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';

@Injectable({
  providedIn: 'root',
})
export class UsermanagementService {
  constructor(
    private con: ConnectionService,
    private http: HttpClient,
  ) {}

  GetUserInfo(): Observable<any[]> {
    // return this.http.get<any[]>(this.con.apiurl + '/UserManagement');
    return this.http.get<any[]>(
      this.con.apiurl +
        '/UserManagement?unitid=' +
        localStorage.getItem('LoginUnit'),
    );
  }

  GetDataByID(id: number): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/UserManagement/GetDataByID/' + id,
    );
  }

  GetUserType(): Observable<any[]> {
    return this.http.get<any[]>(
      this.con.apiurl + '/UserManagement/GetUserType',
    );
  }

  Save(master: any) {
    return this.http.post(this.con.apiurl + '/UserManagement', master);
  }

  Update(master: any) {
    return this.http.put<any>(
      this.con.apiurl + '/UserManagement?id=' + master.Id,
      master,
    );
  }

  Delete(id: number) {
    return this.http.delete<any>(this.con.apiurl + '/UserManagement?id=' + id);
  }
}
