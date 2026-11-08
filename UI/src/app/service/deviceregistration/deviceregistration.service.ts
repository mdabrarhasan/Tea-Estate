import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';

@Injectable({
  providedIn: 'root'
})
export class DeviceregistrationService {

  constructor(
    private con: ConnectionService,
    private http: HttpClient
  ) { }


  Save(device: any) {
    return this.http.post(this.con.apiurl + "/DeviceRegistration", device);
  }

  Delete(code: string, unitid: number) {
    return this.http.delete<any>(
      this.con.apiurl + "/DeviceRegistration?code=" + code + 
      "&unitid=" + unitid
    );
  }
  


  GetDeviceInfo(){

            return this.http.get<any[]>(this.con.apiurl+"/DeviceRegistration?unitid="+localStorage.getItem('LoginUnit'))
          }
      
}