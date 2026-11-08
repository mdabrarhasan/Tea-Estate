import { Component } from '@angular/core';
import {
  BehaviorSubject,
  debounceTime,
  delay,
  first,
  Observable,
  of,
  Subject,
  switchMap,
  take,
  tap,
} from 'rxjs';
import { WagecalenderService } from '../../service/wagecalender/wagecalender.service';
import { PaginationService } from '../../service/pagination/pagination.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../../service/common/common.service';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { paginationsearchsort } from '../../service/common/paginationsearchsort.service';
export interface GridJsModel {
  id: number;
  fromdate: string;
  todate: string;
  weeknumber: string;
  weeklyholiday: string;
  disableweeklyholiday: boolean;
  wagedisbursed: boolean;
  isactive: number;
  enroll: number;
  unitid: number;
}
@Component({
  selector: 'app-wagecalender',
  templateUrl: './wagecalender.component.html',
  styleUrl: './wagecalender.component.css',
})
export class WagecalenderComponent {
  isDisabled: boolean = false;
  PID: number = 0;
  list: any;
  submitted = false;
  gardemlist: any;
  prunetypelist: any;
  isactivelist: any;
  divisionlist: any;

  searchText: string = '';
  tempitems: any;
  items: any[] = [];
  pageOfItems?: Array<any>;
  sortProperty: string = 'id';
  sortOrder = 1;
  loading = false;

  constructor(
    public pagination: paginationsearchsort,
    public service: WagecalenderService,
    private sortService: PaginationService,
    private modalService: NgbModal,
    private cs: CommonService,
    private toastr: ToastrService,
  ) {
    this.Loadlist();
  }

  Loadlist() {
    this.service.GetData().subscribe((data: any[]) => {
      this.items = data;
      this.tempitems = data;
    });
  }

  filterData() {
    const text = this.searchText.toLowerCase().trim();

    if (text) {
      this.items = this.items.filter((item) =>
        item.fromdate.toLowerCase().includes(text),
      );
    } else {
      this.items = [...this.tempitems]; // Reset to original
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
        // sort comparison function
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

  SalaryGenerate(data: any) {
    this.isDisabled = true;
    this.service.SalaryGenerate(data).subscribe((res: any) => {
      if (res.item2.includes('success')) {
        this.isDisabled = false;
        this.toastr.success(res.item2, res.item1);
        this.service.GetData();
      } else {
        this.isDisabled = false;
        this.toastr.error(res.item2, res.item1);
      }
    });
  }

  // GetDataByID(id: any) {
  //   this.service.GetDataByID(id).subscribe((data: any[]) => {
  //     this.EMasterForm.controls['EID'].setValue(data[0].id);
  //     this.EMasterForm.controls['EFROMDATE'].setValue(data[0].fromdate);
  //     this.EMasterForm.controls['ETODATE'].setValue(data[0].todate);
  //     this.EMasterForm.controls['EWEEKNUMBER'].setValue(data[0].weeknumber);

  //     this.EMasterForm.controls['EWEEKLYHOLIDAY'].setValue(
  //       data[0].weeklyholiday,
  //     );
  //     this.EMasterForm.controls['EDISABLEWEEKLYHOLIDAY'].setValue(
  //       data[0].disableweeklyholiday,
  //     );
  //     this.EMasterForm.controls['EWAGEDISBURSED'].setValue(
  //       data[0].wagedisbursed,
  //     );
  //     this.PID = data[0].id;
  //   });
  // }

  GetDataByID(id: any) {
    this.service
      .GetDataByID(id, localStorage.getItem('LoginUnit'))
      .subscribe((data: any[]) => {
        this.EMasterForm.controls['EID'].setValue(data[0].id);
        this.EMasterForm.controls['EFROMDATE'].setValue(data[0].fromdate);
        this.EMasterForm.controls['ETODATE'].setValue(data[0].todate);
        this.EMasterForm.controls['EWEEKNUMBER'].setValue(data[0].weeknumber);
        this.EMasterForm.controls['EWEEKLYHOLIDAY'].setValue(
          data[0].weeklyholiday,
        );
        this.EMasterForm.controls['EDISABLEWEEKLYHOLIDAY'].setValue(
          data[0].disableweeklyholiday,
        );
        this.EMasterForm.controls['EWAGEDISBURSED'].setValue(
          data[0].wagedisbursed,
        );
        this.PID = data[0].id;
      });
  }

  MasterForm: FormGroup = new FormGroup({
    ID: new FormControl('0'),
    FROMDATE: new FormControl(''),
    TODATE: new FormControl(''),
    WEEKNUMBER: new FormControl(''),
    WEEKLYHOLIDAY: new FormControl(''),
    DISABLEWEEKLYHOLIDAY: new FormControl(false),
    WAGEDISBURSED: new FormControl(false),

    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  EMasterForm: FormGroup = new FormGroup({
    EID: new FormControl('0'),
    EFROMDATE: new FormControl(''),
    ETODATE: new FormControl(''),
    EWEEKNUMBER: new FormControl(''),
    EWEEKLYHOLIDAY: new FormControl(''),
    EDISABLEWEEKLYHOLIDAY: new FormControl(false),
    EWAGEDISBURSED: new FormControl(false),
    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  Reset() {
    //this.MasterForm.reset();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.MasterForm.controls;
  }

  ngOnInit(): void {
    this.GetUnit();
    this.Loadlist();
  }

  Create(): void {
    this.submitted = true;
    if (this.MasterForm.value.FROMDATE == '') {
      this.toastr.error('Enter From Date.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.TODATE == '') {
      this.toastr.error('Enter To Date Code.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.WEEKNUMBER == '') {
      this.toastr.error('Select Week Number.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.WEEKLYHOLIDAY == '') {
      this.toastr.error('Select Plucking Section.', 'close');
      this.submitted = false;
    }
    if (this.submitted) {
      //console.log(this.MasterForm.value);
      this.service.Save(this.MasterForm.value).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item2, res.item1);
          this.Reset();
          this.service.GetData();
        }
      });
    }
  }

  Update(): void {
    this.submitted = true;
    if (this.EMasterForm.value.EFROMDATE == '') {
      this.toastr.error('Enter Section Name.', 'close');
      this.submitted = false;
    } else if (this.EMasterForm.value.ETODATE == '') {
      this.toastr.error('Enter Section Code.', 'close');
      this.submitted = false;
    } else if (this.EMasterForm.value.EWEEKNUMBER == '0') {
      this.toastr.error('Select Division.', 'close');
      this.submitted = false;
    } else if (this.EMasterForm.value.EWEEKLYHOLIDAY == '0') {
      this.toastr.error('Select Plucking Section.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      var obj = {
        ID: this.EMasterForm.value.EID,
        FROMDATE: this.EMasterForm.value.EFROMDATE,
        TODATE: this.EMasterForm.value.ETODATE,

        WEEKNUMBER: this.EMasterForm.value.EWEEKNUMBER,
        WEEKLYHOLIDAY: this.EMasterForm.value.EWEEKLYHOLIDAY,

        DISABLEWEEKLYHOLIDAY: this.EMasterForm.value.EDISABLEWEEKLYHOLIDAY,
        WAGEDISBURSED: this.EMasterForm.value.EWAGEDISBURSED,

        UNITID: localStorage.getItem('LoginUnit'),
        ENROLL: localStorage.getItem('Enroll'),
      };
      this.service.Update(obj).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item1, res.item2);
        }
      });
    }
  }

  Delete(id: any): void {
    this.service
      .Delete(id, localStorage.getItem('LoginUnit'))
      .subscribe((res: any) => {
        if (res.item2.includes('error')) {
          this.toastr.error(res.item1, 'Delete');
          this.Loadlist();
        }
      });
  }

  GetUnit() {
    this.cs.GetUnit().subscribe((data: any[]) => {
      this.gardemlist = data;
    });
  }
  SalaryCancel(id: any) {
    this.service
      .SalaryCancel(this.MasterForm.value.UNITID, id)
      .subscribe((res: any) => {
        if (res.item2.includes('error')) {
          this.toastr.error(res.item1, 'Delete');
        }
      });
  }
}
