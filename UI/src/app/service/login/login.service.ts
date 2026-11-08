import { Injectable } from '@angular/core';
import { ConnectionService } from '../connection/connection.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { PermissionService } from '../permission/permission.service';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  controller = '/Login';
  submitted = false;
  loading = false;
  private userLoggedIn = new Subject<void>();
  userLoggedIn$ = this.userLoggedIn.asObservable();

  constructor(
    private con: ConnectionService,
    private http: HttpClient,
    private router: Router,
    private toastr: ToastrService,
    private perm: PermissionService,
  ) {}

  Login(user: any) {
    this.loading = true;
    this.http
      .post<any>(this.con.apiurl + this.controller, user)
      .subscribe((res: any) => {
        if (res == 404) {
          this.toastr.error('Invalid User Name Password.', 'Error');
          //this.snackbar.error("Invalid User Name Password.", 'close', 3000);
          this.submitted = false;
          this.loading = false;
          this.router.navigate(['/login']);
        } else {
          localStorage.clear();
          localStorage.setItem('token', res.token);
          localStorage.setItem('list', JSON.stringify(res.query));
          // localStorage.setItem('Department',res.query[0].strDepatrment);
          localStorage.setItem('Designation', res.query[0].designationName);
          localStorage.setItem('Name', res.query[0].name);
          localStorage.setItem('Enroll', res.query[0].enroll);
          localStorage.setItem('LoginUnit', res.query[0].unit);
          localStorage.setItem('TypeId', res.query[0].typeId); // ADDED
          localStorage.setItem('permissions', JSON.stringify(res.permissions));
          this.perm.refresh();
          this.submitted = true;
          this.loading = false;
          this.toastr.success('Successfully Logged In.', 'success');
          this.userLoggedIn.next();
          this.router.navigate(['/dashboard']);
        }
      });
  }
}
