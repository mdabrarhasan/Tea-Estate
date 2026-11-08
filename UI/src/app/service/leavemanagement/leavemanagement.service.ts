import { DecimalPipe } from '@angular/common';
import { Injectable } from '@angular/core';
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';
import { PaginationService } from '../pagination/pagination.service';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, debounceTime, delay, Observable, of, Subject, switchMap, tap } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class LeavemanagementService {

  constructor(private pipe: DecimalPipe,private con: ConnectionService,private http:HttpClient,private pagination: PaginationService,private toastr: ToastrService ) 
  {

  }
  Save(master :any)
    {
      return this.http.post(this.con.apiurl+"/LeaveManagement", master);
    }
    LeaveGet(workerid :any,leavetyeid:any)
    {
      //return this.http.get<any>(this.con.apiurl+"/LeaveManagement/LeaveGet?workerid="+workerid+"&leavetyeid="+leavetyeid);
      return this.http.get<any[]>(`${this.con.apiurl}/LeaveManagement/LeaveGet/${workerid}/${leavetyeid}`)
    }
    
     GetData() : Observable<any[]>{
            return this.http.get<any[]>(this.con.apiurl+"/LeaveManagement?unitid="+localStorage.getItem("LoginUnit"));
                
            }

    Delete(id :number)
    {
      //alert(id)
      return this.http.delete<any>(this.con.apiurl+"/LeaveManagement?id="+id+"&&enroll="+localStorage.getItem("Enroll")+"&unitid="+localStorage.getItem("LoginUnit"));
    }

}
