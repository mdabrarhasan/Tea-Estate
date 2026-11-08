import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConnectionService } from '../connection/connection.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  controller="/Menu";
  constructor(private http:HttpClient,private con:ConnectionService) { }

  Menu(UserID:string):Observable<any[]>{
    //return this.http.get<any[]>(this.con.apiurl+this.controller+'/MenuList'+"?UserID="+UserID)
    return this.http.get<any[]>(this.con.apiurl+this.controller+"?UserID="+UserID)
  }

  SubMenu(UserID:string):Observable<any[]>{
    return this.http.get<any[]>(this.con.apiurl+this.controller+"/LoadGSubMenu"+"?UserID="+UserID)
  }
}
