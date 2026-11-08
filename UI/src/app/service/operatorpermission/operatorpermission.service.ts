// service/operatorpermission/operatorpermission.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConnectionService } from '../connection/connection.service';

@Injectable({ providedIn: 'root' })
export class OperatorPermissionService {
  constructor(
    private con: ConnectionService,
    private http: HttpClient,
  ) {}

  GetPermissions() {
    return this.http.get<any[]>(this.con.apiurl + '/OperatorPermission');
  }

  SavePermission(dto: any) {
    return this.http.post<any>(this.con.apiurl + '/OperatorPermission', dto);
  }
}
