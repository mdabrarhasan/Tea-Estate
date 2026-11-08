import { Component, PipeTransform } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import {
  BehaviorSubject,
  concatMap,
  debounceTime,
  delay,
  distinctUntilChanged,
  first,
  Observable,
  of,
  Subject,
  switchMap,
  take,
  tap,
} from 'rxjs';
import { CommonService } from '../../service/common/common.service';
import { PaginationService } from '../../service/pagination/pagination.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { KamjarimanagementService } from '../../service/kamjarimanagement/kamjarimanagement.service';
import { DecimalPipe } from '@angular/common';
import { paginationsearchsort } from '../../service/common/paginationsearchsort.service';

export interface GridJsModel {
  id: number;
  name: string;
  code: string;
}

@Component({
  selector: 'app-kamjarimanagement',
  templateUrl: './kamjarimanagement.component.html',
  styleUrl: './kamjarimanagement.component.css',
})
export class KamjarimanagementComponent {
  items: any[] = [];
  pageOfItems?: Array<any>;
  sortProperty: string = 'id';
  sortOrder = 1;
  loading = false;
  searchText: any;

  PID: number = 0;
  list: any;
  submitted = false;
  kamjarilist: any;
  prunetypelist: any;
  isactivelist: any;
  divisionlist: any;
  tempitems: any;
  griddata: any;
  mastertypelist: any;

  constructor(
    public pagination: paginationsearchsort,
    private _common: CommonService,
    public service: KamjarimanagementService,
    private sortService: PaginationService,
    private modalService: NgbModal,
    private cs: CommonService,
    private toastr: ToastrService,
  ) {
    this.Loadlist();
  }

  onChangePage(pageOfItems: Array<any>) {
    // update current page of items
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

  filterData() {
    const text = this.searchText.toLowerCase().trim();

    if (text) {
      this.items = this.items.filter((item) =>
        item.name.toLowerCase().includes(text),
      );
    } else {
      this.items = [...this.tempitems]; // Reset to original
    }
  }

  sortIcon(property: string) {
    if (property === this.sortProperty) {
      return this.sortOrder === 1 ? '☝️' : '👇';
    }
    return '';
  }

  onSort(column: any) {
    this.griddata = this.sortService.onSort(column, this.griddata);
    this.pagination._countries$.next(this.griddata);
  }

  Loadlist() {
    this.service.GetData().subscribe((data: any[]) => {
      this.items = data;
      this.tempitems = data;
    });
  }

  GetDataByID(id: any) {
    this.service.GetDataByID(id).subscribe((data: any[]) => {
      this.EMasterForm.controls['EID'].setValue(data[0].id);
      this.EMasterForm.controls['ECode'].setValue(data[0].code);
      this.EMasterForm.controls['EName'].setValue(data[0].name);

      this.EMasterForm.controls['EType'].setValue(data[0].kamjaritypeid);
      this.EMasterForm.controls['EDoublyKamjari'].setValue(
        data[0].markasdoubly,
      );
      this.EMasterForm.controls['EExcludeInWages'].setValue(
        data[0].excludeinwages,
      );

      this.EMasterForm.controls['EAccount'].setValue(data[0].accountid);
      this.EMasterForm.controls['ETaskUnit'].setValue(data[0].taskunit);
      this.EMasterForm.controls['ETaskMen'].setValue(data[0].taskmen);
      this.EMasterForm.controls['ETaskWomen'].setValue(data[0].taskwomen);

      this.EMasterForm.controls['EPayofPost'].setValue(data[0].payofpost);
      this.EMasterForm.controls['EIsMaterialAccount'].setValue(
        data[0].ismaterialaccount,
      );
      this.EMasterForm.controls['EDoublyRate'].setValue(data[0].doublyrate);
      this.EMasterForm.controls['EFixedDoublyRate'].setValue(
        data[0].fixeddoublyrate,
      );
      this.PID = data[0].id;
    });
  }

  MasterForm: FormGroup = new FormGroup({
    ID: new FormControl('0'),
    Name: new FormControl(''),
    Code: new FormControl(''),
    Type: new FormControl(''),

    DoublyKamjari: new FormControl(''),
    ExcludeInWages: new FormControl(''),
    Account: new FormControl(''),
    TaskUnit: new FormControl(''),
    MasterTypeID: new FormControl(''),
    TaskMen: new FormControl(''),
    TaskWomen: new FormControl(''),
    PayofPost: new FormControl(''),
    IsMaterialAccount: new FormControl(''),

    DoublyRate: new FormControl(''),
    FixedDoublyRate: new FormControl(''),

    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  EMasterForm: FormGroup = new FormGroup({
    EID: new FormControl('0'),
    EName: new FormControl(''),
    ECode: new FormControl(''),
    EType: new FormControl(''),

    EDoublyKamjari: new FormControl(''),
    EExcludeInWages: new FormControl(''),
    EAccount: new FormControl(''),
    ETaskUnit: new FormControl(''),

    ETaskMen: new FormControl(''),
    ETaskWomen: new FormControl(''),
    EPayofPost: new FormControl(''),
    EIsMaterialAccount: new FormControl(''),
    EMasterTypeID: new FormControl(''),
    EDoublyRate: new FormControl(''),
    EFixedDoublyRate: new FormControl(''),

    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  Reset() {
    this.MasterForm.reset();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.MasterForm.controls;
  }

  ngOnInit(): void {
    this.GetKamjari();
    this.Loadlist();
    this.GetIsActive();
    this.GetMasterTypes();
  }

  Create(): void {
    this.submitted = true;
    if (this.MasterForm.value.Name == '') {
      this.toastr.error('Enter Kamjari Name.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.Code == '') {
      this.toastr.error('Enter Kamjari Code.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.Type == '') {
      this.toastr.error('Enter Kamjari Type.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.DoublyKamjari == '') {
      this.toastr.error('Enter Kamjari Doubly Kamjari.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.Account == '') {
      this.toastr.error('Enter Kamjari Account.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.ExcludeInWages == '') {
      this.toastr.error('Enter Kamjari Exclude In Wages.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.TaskUnit == '') {
      this.toastr.error('Enter Kamjari Task Unit.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.TaskMen == '') {
      this.toastr.error('Enter Kamjari Task Men.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.TaskWomen == '') {
      this.toastr.error('Enter Kamjari Task Women.', 'close');
      this.submitted = false;
    } else if (!/^\d+$/.test(this.MasterForm.value.PayofPost)) {
      this.toastr.error('Pay of Post must contain numbers only.', 'close');
      this.submitted = false;
      return;
    } else if (this.MasterForm.value.MasterTypeID == '') {
      this.toastr.error('Enter Master Type.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.IsMaterialAccount == '') {
      this.toastr.error('Enter Kamjari IsMaterial Account.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.DoublyRate == '') {
      this.toastr.error('Enter Kamjari Doubly Rate.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.FixedDoublyRate == '') {
      this.toastr.error('Enter Kamjari Fixed Doubly Rate.', 'close');
      this.submitted = false;
    }
    if (this.submitted) {
      this.service.Save(this.MasterForm.value).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item2, res.item1);
          //this.Reset();

          this.Loadlist();
        }
      });
    }
  }

  GetMasterTypes() {
    this.cs.GetMasterTypes().subscribe((data: any[]) => {
      this.mastertypelist = data;
    });
  }

  Update(): void {
    this.submitted = true;
    if (this.EMasterForm.value.EName == '') {
      this.toastr.error('Enter Plucking Name.', 'close');
      this.submitted = false;
    } else if (this.EMasterForm.value.ECode == '') {
      this.toastr.error('Enter Plucking Code.', 'close');
      this.submitted = false;
    } else if (!/^\d+$/.test(this.EMasterForm.value.EPayofPost)) {
      this.toastr.error('Pay of Post must contain numbers only.', 'Error');
      this.submitted = false;
      return;
    }

    if (this.submitted) {
      var obj = {
        ID: this.EMasterForm.value.EID,
        NAME: this.EMasterForm.value.EName,
        CODE: this.EMasterForm.value.ECode,

        Type: this.EMasterForm.value.EType,

        DoublyKamjari: this.EMasterForm.value.EDoublyKamjari,
        ExcludeInWages: this.EMasterForm.value.EExcludeInWages,
        Account: this.EMasterForm.value.EAccount,
        TaskUnit: this.EMasterForm.value.ETaskUnit,

        TaskMen: this.EMasterForm.value.ETaskMen,
        TaskWomen: this.EMasterForm.value.ETaskWomen,
        PayofPost: this.EMasterForm.value.EPayofPost,
        IsMaterialAccount: this.EMasterForm.value.EIsMaterialAccount,
        MASTERTYPEID: this.EMasterForm.value.EMasterTypeID,
        DoublyRate: this.EMasterForm.value.EDoublyRate,
        FixedDoublyRate: this.EMasterForm.value.EFixedDoublyRate,

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
    if (confirm('Are you sure you want to delete this Kamjari?')) {
      const unitid = localStorage.getItem('LoginUnit');
      const enroll = localStorage.getItem('Enroll');

      this.service.Delete(id, unitid, enroll).subscribe(
        (res: any) => {
          console.log('Delete Response:', res);

          if (res.item2 && res.item2.includes('success')) {
            this.toastr.success(
              res.item1 || 'Kamjari deleted successfully',
              'Success',
            );
            this.Loadlist();
          } else {
            this.toastr.error(res.item1 || 'Delete failed', 'Error');
          }
        },
        (error) => {
          console.error('Delete Error:', error);
          this.toastr.error(
            'An error occurred while deleting Kamjari',
            'Error',
          );
        },
      );
    }
  }

  GetKamjari() {
    this.cs.GetKamjari().subscribe((data: any[]) => {
      this.kamjarilist = data;
    });
  }

  GetIsActive() {
    this.cs.GetIsActive().subscribe((data: any[]) => {
      this.isactivelist = data;
    });
  }
}
