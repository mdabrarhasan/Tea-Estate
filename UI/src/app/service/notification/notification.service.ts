// src/app/service/notification.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ConnectionService } from '../connection/connection.service';

export interface NotificationSummary {
  sectionCount: number;
  presentCount: number;
  absentCount: number;
  leaveCount: number;
  pluckingQty: number;
  pruningQty: number;
}

@Injectable({ providedIn: 'root' })
export class NotificationService {
  constructor(
    private http: HttpClient,
    private con: ConnectionService,
  ) {}

  getSummary(): Observable<NotificationSummary[]> {
    const unitid = localStorage.getItem('LoginUnit');
    return this.http.get<NotificationSummary[]>(
      this.con.apiurl + '/Notification?unitid=' + unitid,
      //this.con.apiurl + '/Notification?unitid=' + unitid + '&date=2026-08-14',
    );
  }
}
