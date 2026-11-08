import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../service/common/common.service';
import { LeavemanagementService } from '../../service/leavemanagement/leavemanagement.service';
import { ToastrService } from 'ngx-toastr';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-leavemanagement',
  templateUrl: './leavemanagement.component.html',
  styleUrls: ['./leavemanagement.component.css'],
})
export class LeavemanagementComponent {
  submitted = false;
  leavelist: any;
  workerlist: any;
  workername: any;
  filteredSuppliers: any[] = [];
  leavetaken: any;
  leaveremening: any;
  getdata: any;
  divisionlist: any[] = [];
  searchText: string = '';
  tempitems: any;
  today: string;
  items: any[] = [];
  pageOfItems?: Array<any>;
  sortProperty: string = 'id';
  sortOrder = 1;
  loading = false;

  constructor(
    private cs: CommonService,
    private lms: LeavemanagementService,
    private toastr: ToastrService,
  ) {
    this.LoadWorker();
    this.LoadLeave();
    this.LoadDivision();
    this.GetData();
    this.today = this.getTodayDate();
  }

  ngOnInit(): void {
    this.today = this.getTodayDate();

    this.MasterForm = new FormGroup({
      WORKER: new FormControl(0),
      FROMDATE: new FormControl(this.today),
      TODATE: new FormControl(this.today),
      WORKERNAME: new FormControl(''),
      LEAVETYPEID: new FormControl(0),
      DIVISIONID: new FormControl({ value: 0, disabled: true }),
      UNITID: new FormControl(localStorage.getItem('LoginUnit')),
      ENROLL: new FormControl(localStorage.getItem('Enroll')),
    });
  }

  filterData() {
    const text = this.searchText.toLowerCase().trim();

    if (text) {
      this.items = this.items.filter((item) =>
        item.name.toLowerCase().includes(text),
      );
    } else {
      this.items = [...this.tempitems];
    }
  }

  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }

  sortBy(property: string) {
    this.sortOrder = property === this.sortProperty ? this.sortOrder * -1 : 1;
    this.sortProperty = property;
    this.items = [
      ...this.items.sort((a: any, b: any) => {
        let result = 0;
        if (a[property] < b[property]) {
          result = -1;
        }
        if (a[property] > b[property]) {
          result = 1;
        }
        return result * this.sortOrder;
      }),
    ];
  }

  sortIcon(property: string) {
    if (property === this.sortProperty) {
      return this.sortOrder === 1 ? '☝️' : '👇';
    }
    return '';
  }

  LoadWorker() {
    this.workername = this.MasterForm.controls['WORKERNAME'].value;

    if (this.workername && this.workername.length >= 0) {
      this.cs
        .GetLeaveTypeValuelist(this.workername)
        .pipe(debounceTime(300))
        .subscribe((data) => {
          this.filteredSuppliers = data.map((item) => ({
            label: item.name,
            id: item.id,
          }));
        });
    } else {
      this.filteredSuppliers = [];
    }
  }

  onSelectSupplier(supplier: any) {
    this.MasterForm.controls['WORKER'].setValue(supplier.id);
    this.MasterForm.controls['WORKERNAME'].setValue(supplier.label);
    this.filteredSuppliers = [];
  }

  LoadLeave() {
    this.cs.GetLeaveTypelist().subscribe((data: any[]) => {
      this.leavelist = data;
    });
  }

  LoadDivision() {
    this.cs.GetDivision().subscribe((data: any[]) => {
      this.divisionlist = data;
    });
  }

  LeaveGet() {
    const selectedLeaveType = this.leavelist.find(
      (leave: any) => leave.id == this.MasterForm.value.LEAVETYPEID,
    );

    if (selectedLeaveType && selectedLeaveType.name === 'PUBLIC ANNUAL LEAVE') {
      this.MasterForm.controls['WORKERNAME'].disable();
      this.MasterForm.controls['WORKER'].setValue(0);
      this.MasterForm.controls['WORKERNAME'].setValue('');
      this.filteredSuppliers = [];
      this.leavetaken = 'Applies to all active workers';
      this.leaveremening = '';
      this.MasterForm.controls['DIVISIONID'].enable();
      return;
    } else {
      this.MasterForm.controls['WORKERNAME'].enable();
      this.MasterForm.controls['DIVISIONID'].disable();
      this.MasterForm.controls['DIVISIONID'].setValue(0);

      if (this.MasterForm.value.WORKER && this.MasterForm.value.WORKER != 0) {
        this.lms
          .LeaveGet(
            this.MasterForm.value.WORKER,
            this.MasterForm.value.LEAVETYPEID,
          )
          .subscribe((data: any[]) => {
            this.leavetaken = 'Leave Taken Days : ' + data[0].leavetaken;
            this.leaveremening = 'Remaining Days : ' + data[0].remleave;
          });
      } else {
        this.leavetaken = '';
        this.leaveremening = '';
      }
    }
  }

  GetData() {
    this.lms.GetData().subscribe((data: any[]) => {
      this.items = data;
      this.tempitems = data;
    });
  }

  MasterForm: FormGroup = new FormGroup({
    WORKER: new FormControl(0),
    FROMDATE: new FormControl(''),
    TODATE: new FormControl(''),
    WORKERNAME: new FormControl(''),
    LEAVETYPEID: new FormControl(0),
    DIVISIONID: new FormControl(0),
    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  EMasterForm: FormGroup = new FormGroup({
    EID: new FormControl('0'),
    EWORKER: new FormControl(0),
    EDATE: new FormControl(''),
    ELEAVETYPEID: new FormControl(0),
  });

  Create(): void {
    this.submitted = true;

    const selectedLeaveType = this.leavelist.find(
      (leave: any) => leave.id == this.MasterForm.value.LEAVETYPEID,
    );
    const isPublicAnnualLeave =
      selectedLeaveType && selectedLeaveType.name === 'PUBLIC ANNUAL LEAVE';

    if (!isPublicAnnualLeave && this.MasterForm.value.WORKER == '0') {
      this.toastr.error('Enter Worker Code.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.FROMDATE == '') {
      this.toastr.error('Enter From Date.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.TODATE == '') {
      this.toastr.error('Enter To Date.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.LEAVETYPEID == '0') {
      this.toastr.error('Select LeaveType.', 'close');
      this.submitted = false;
    } else if (
      isPublicAnnualLeave &&
      this.MasterForm.controls['DIVISIONID'].value == '0'
    ) {
      this.toastr.error('Select Division.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      var obj = {
        WORKER: isPublicAnnualLeave ? 0 : this.MasterForm.value.WORKER,
        FROMDATE: this.MasterForm.value.FROMDATE,
        TODATE: this.MasterForm.value.TODATE,
        LEAVETYPEID: this.MasterForm.value.LEAVETYPEID,
        DIVISIONID: isPublicAnnualLeave
          ? this.MasterForm.controls['DIVISIONID'].value
          : 0,
        UNITID: localStorage.getItem('LoginUnit'),
        ENROLL: localStorage.getItem('Enroll'),
      };

      this.lms.Save(obj).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item2, res.item1);
          this.GetData();
          this.clear();
        } else {
          this.toastr.error(res.item2, res.item1);
          this.clear();
        }
      });
    }
  }

  clear() {
    this.MasterForm.controls['WORKER'].setValue('');
    this.MasterForm.controls['WORKERNAME'].setValue('');
    this.MasterForm.controls['WORKERNAME'].enable();
    this.MasterForm.controls['LEAVETYPEID'].setValue(0);
    this.MasterForm.controls['DIVISIONID'].setValue(0);
    this.MasterForm.controls['DIVISIONID'].disable();
    this.leavetaken = '';
    this.leaveremening = '';
  }

  Delete(id: any) {
    this.lms.Delete(id).subscribe((res: any) => {
      if (res.item2.includes('error')) {
        this.toastr.error(res.item1, 'Delete');
        this.GetData();
      }
    });
  }
  getTodayDate(): string {
    const today = new Date();
    return today.toISOString().substring(0, 10);
  }
}
