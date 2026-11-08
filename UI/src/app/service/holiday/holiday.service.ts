import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HolidayService {

  constructor(private con: ConnectionService,private http:HttpClient) { 
    this.GetData();
  }

  GetData() : Observable<any[]>{
   return this.http.get<any[]>(this.con.apiurl+"/Holiday?unitid="+localStorage.getItem('LoginUnit')+"&enroll="+localStorage.getItem('Enroll'));
 }
   Save(master :any)
    {
      //alert(master)
      return this.http.post(this.con.apiurl+"/Holiday", master);
    }
 Delete(id :number)
    {
      //alert(id)
      return this.http.delete<any>(this.con.apiurl+"/Holiday?id="+id+"&&enroll="+localStorage.getItem("Enroll")+"&unitid="+localStorage.getItem("LoginUnit"));
    }
}
