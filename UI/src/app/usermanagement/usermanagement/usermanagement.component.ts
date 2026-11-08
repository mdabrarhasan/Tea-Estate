import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UsermanagementService } from '../../service/usermanagement/usermanagement.service';
import { PaginationService } from '../../service/pagination/pagination.service';

@Component({
  selector: 'app-usermanagement',
  templateUrl: './usermanagement.component.html',
  styleUrl: './usermanagement.component.css',
})
export class UsermanagementComponent implements OnInit {
  griddata: any;
  usertypelist: any;
  submitted = false;
  PID: number = 0;

  constructor(
    public service: UsermanagementService,
    private sortService: PaginationService,
    private toastr: ToastrService,
  ) {}

  ngOnInit() {
    this.Loadlist();
    this.GetUserType();
  }

  Loadlist() {
    this.service.GetUserInfo().subscribe((data: any[]) => {
      this.griddata = data;
    });
  }

  GetUserType() {
    this.service.GetUserType().subscribe((data: any[]) => {
      this.usertypelist = data;
    });
  }

  onSort(column: any) {
    this.griddata = this.sortService.onSort(column, this.griddata);
  }

  MasterForm: FormGroup = new FormGroup({
    Name: new FormControl('', Validators.required),
    Email: new FormControl(''),
    Contact: new FormControl(''),
    Usertype: new FormControl('0', Validators.required),
    Division: new FormControl(''),
    Book: new FormControl(''),
    Login: new FormControl(''),
    Password: new FormControl(''),
  });

  EMasterForm: FormGroup = new FormGroup({
    EName: new FormControl('', Validators.required),
    EEmail: new FormControl(''),
    EContact: new FormControl(''),
    EUsertype: new FormControl('0', Validators.required),
    EDivision: new FormControl(''),
    EBook: new FormControl(''),
    ELogin: new FormControl(''),
    EPassword: new FormControl(''),
  });

  Reset() {
    this.MasterForm.reset({ Usertype: '0' });
  }

  UserCreate(): void {
    this.submitted = true;
    if (this.MasterForm.value.Name == '') {
      this.toastr.error('Enter User Name.', 'close');
      this.submitted = false;
    } else if (
      this.MasterForm.value.Usertype == '0' ||
      this.MasterForm.value.Usertype == '--Select--'
    ) {
      this.toastr.error('Please Select User Type.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      var obj = {
        Username: this.MasterForm.value.Name,
        Emailid: this.MasterForm.value.Email,
        Contactnumber: this.MasterForm.value.Contact,
        Usertypeid: this.MasterForm.value.Usertype,
        Loginid: this.MasterForm.value.Login,
        Passwords: this.MasterForm.value.Password,
        Division: this.MasterForm.value.Division,
        Book: this.MasterForm.value.Book,
        UnitId: localStorage.getItem('LoginUnit'),
      };
      this.service.Save(obj).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item1, res.item2);
          this.Reset();
          this.Loadlist();
        } else {
          this.toastr.error(res.item1, res.item2);
        }
      });
    }
  }

  GetDataByID(id: any) {
    this.service.GetDataByID(id).subscribe((data: any[]) => {
      this.EMasterForm.controls['EName'].setValue(data[0].username);
      this.EMasterForm.controls['EEmail'].setValue(data[0].emailid);
      this.EMasterForm.controls['EContact'].setValue(data[0].contactnumber);
      this.EMasterForm.controls['EUsertype'].setValue(data[0].usertypeid);
      this.EMasterForm.controls['EDivision'].setValue(data[0].division);
      this.EMasterForm.controls['EBook'].setValue(data[0].book);
      this.EMasterForm.controls['ELogin'].setValue(data[0].loginid);
      this.EMasterForm.controls['EPassword'].setValue(data[0].passwords);
      this.PID = data[0].id;
    });
  }

  Update(): void {
    this.submitted = true;
    if (this.EMasterForm.value.EName == '') {
      this.toastr.error('Enter User Name.', 'close');
      this.submitted = false;
    } else if (
      this.EMasterForm.value.EUsertype == '0' ||
      this.EMasterForm.value.EUsertype == '--Select--'
    ) {
      this.toastr.error('Please Select User Type.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      var obj = {
        Id: this.PID,
        Username: this.EMasterForm.value.EName,
        Emailid: this.EMasterForm.value.EEmail,
        Contactnumber: this.EMasterForm.value.EContact,
        Usertypeid: this.EMasterForm.value.EUsertype,
        Loginid: this.EMasterForm.value.ELogin,
        Passwords: this.EMasterForm.value.EPassword,
        Division: this.EMasterForm.value.EDivision,
        Book: this.EMasterForm.value.EBook,
      };
      this.service.Update(obj).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item1, res.item2);
          this.Loadlist();
        } else {
          this.toastr.error(res.item1, res.item2);
        }
      });
    }
  }

  Delete(id: any): void {
    this.service.Delete(id).subscribe((res: any) => {
      if (res.item2.includes('success')) {
        this.toastr.success(res.item1, 'Delete');
        this.Loadlist();
      } else {
        this.toastr.error(res.item1, 'Delete');
      }
    });
  }
}
