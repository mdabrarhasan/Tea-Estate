import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class SalarygenerateService {

  constructor(private con: ConnectionService,private http:HttpClient) { }

  GetDataByID(sdate:string,edate:string) : Observable<any[]>{
              return this.http.get<any[]>(this.con.apiurl+"/SalaryGenerate/GetDataByID?sdate="+sdate+"&edate="+edate+"&uniid="+localStorage.getItem("LoginUnit")+"&enroll="+localStorage.getItem("Enroll"));
            }
}
