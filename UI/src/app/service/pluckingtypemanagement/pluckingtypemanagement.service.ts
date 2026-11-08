import { DecimalPipe } from '@angular/common';
import { Injectable, PipeTransform } from '@angular/core';
import { BehaviorSubject, debounceTime, delay, Observable, of, Subject, switchMap, tap } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';
import { PaginationService } from '../pagination/pagination.service';







@Injectable({
  providedIn: 'root'
})
export class PluckingtypemanagementService {


      
        constructor(private pipe: DecimalPipe,private con: ConnectionService,private http:HttpClient,private pagination: PaginationService) { 
          this.GetData();
        }
      
        Save(master :any)
        {
          return this.http.post(this.con.apiurl+"/PluckingType", master);
        }
      
        Update(master :any)
        {
          return this.http.put<any>(this.con.apiurl+"/PluckingType?id="+master.ID,master);
        }
      
        Edit(master :any)
        {
          return this.http.post(this.con.apiurl+"/PluckingType", master);
        }
      
        Delete(id :number,unitid:any)
        {
          return this.http.delete<any>(this.con.apiurl+"/PluckingType?id="+id+"&unitid="+unitid);
        }
      
        GetDataByID(id:number) : Observable<any[]>{
          return this.http.get<any[]>(this.con.apiurl+"/PluckingType/GetDataByID/"+id);
        }
      
        GetData(){
        //  this.http.get<any[]>(this.con.apiurl+"/PluckingType?unitid="+localStorage.getItem('LoginUnit')+"&enroll="+localStorage.getItem('Enroll')).subscribe((data: any[])=>{
        //     this._search$.pipe(
        //       tap(() => this._loading$.next(true)),
        //       debounceTime(200),
        //       switchMap(() => this._search(data)),
        //       delay(200),
        //       tap(() => this._loading$.next(false))
        //     ).subscribe(result => {
        //       this._countries$.next(result.countries);
        //       this._total$.next(result.total);
        //     });
        
        //     this._search$.next()
        //   })  
          return this.http.get<any[]>(this.con.apiurl+"/PluckingType?unitid="+localStorage.getItem('LoginUnit')+"&enroll="+localStorage.getItem('Enroll'))
        }
        
        
}
