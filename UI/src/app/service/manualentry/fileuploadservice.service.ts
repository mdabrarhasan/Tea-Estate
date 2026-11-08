import { Injectable } from '@angular/core';
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class FileuploadserviceService {
  constructor(private con: ConnectionService, private http: HttpClient) {}
  uploadExcel(file: File, date: any, typeid: any, unitid: any) {
    const formData = new FormData();
    formData.append('file', file, file.name);
    formData.append('date', date);
    formData.append('typeid', typeid.toString());
    formData.append('unitid', unitid.toString());

    return this.http.post(this.con.apiurl + '/Excel/upload', formData);
  }
}
