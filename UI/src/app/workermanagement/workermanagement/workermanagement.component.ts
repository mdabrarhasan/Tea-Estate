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

import { CommonService } from '../../service/common/common.service';
import { PaginationService } from '../../service/pagination/pagination.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import {
  GridWorkerBatchModel,
  WorkerbatchService,
} from '../../service/workermanagement/workerbatch.service';
import { Console } from 'console';
import {
  GridJsModel,
  WorkerbookService,
} from '../../service/workermanagement/workerbook.service';
import { WorkermanagementService } from '../../service/workermanagement/workermanagement.service';
import {
  GridJsWorkerTypeModel,
  WorkertypeService,
} from '../../service/workermanagement/workertype.service';
import {
  GridleaveJsModel,
  WorkerleaveService,
} from '../../service/workermanagement/workerleave.service';
import { paginationsearchsort } from '../../service/common/paginationsearchsort.service';

export interface GridJsWManagementModel {
  id: number;
  name: string;
  code: string;
  empno: string;
  pfnumber: string;
  dateofbirth: string;

  address: string;
  contactnumber: string;
  divisionid: number;
  divisionname: string;

  genderid: number;
  gendername: string;
  workertypeid: number;
  workertype: string;

  workersubtypeid: number;
  workersubtype: string;
  bookid: number;
  bookname: string;

  disableddisincentiveid: boolean;
  monthlyrated: boolean;
  essentialworker: boolean;
  retiredid: boolean;

  pluckingbatchid: number;
  pluckingbatchname: string;

  batchid: number;
  batchname: string;

  kamjariid: number;
  kamjari: string;

  enroll: number;
  unitid: number;
}

@Component({
  selector: 'app-workermanagement',
  templateUrl: './workermanagement.component.html',
  styleUrl: './workermanagement.component.css',
})
export class WorkermanagementComponent {
  PID: number = 0;
  BatchPID: number = 0;
  EmpID: number = 0;

  list: any;
  submitted = false;
  gardemlist: any;
  kamjaritypelist: any;
  kamjarilist: any;
  attendancelist: any;
  prunetypelist: any;
  isactivelist: any;
  divisionlist: any;
  shiftlist: any;
  batchlistdata: any;

  genderlist: any;
  booklist: any;
  booklistdata: any;
  workertypelist: any;
  jobstationlist: any;
  workergrouplist: any;
  batchlist: any;

  gridjsList$!: Observable<GridJsModel[]>;
  total$: Observable<number> = new Observable<number>();
  pagetotalworkertype$: Observable<number> = new Observable<number>();
  pagetotalworkermanagement$: Observable<number> = new Observable<number>();

  gridworkerbatchList$!: Observable<GridWorkerBatchModel[]>;
  totalwbatch$: Observable<number> = new Observable<number>();

  gridworkertypeList$!: Observable<GridJsWorkerTypeModel[]>;
  gridworkerleaveList$!: Observable<GridleaveJsModel[]>;
  totalworkertype$: Observable<number> = new Observable<number>();
  totalworkerleave$: Observable<number> = new Observable<number>();

  gridworkermanagementList$!: Observable<GridJsWManagementModel[]>;
  totalworkermanagement$: Observable<number> = new Observable<number>();

  griddata: any;
  items: any[] = [];
  pageOfItems?: Array<any>;
  workerItems?: Array<any>;
  sortProperty: string = 'id';
  sortOrder = 1;
  loading = false;
  searchText: any;
  tempitems: any;

  constructor(
    public pagination: paginationsearchsort,
    private common: CommonService,
    public service: WorkerbookService,
    public servicebatch: WorkerbatchService,
    public serviceworkermanagement: WorkermanagementService,
    public workerleveservice: WorkerleaveService,
    public serviceworkertype: WorkertypeService,
    private sortService: PaginationService,
    private modalService: NgbModal,
    private cs: CommonService,
    private toastr: ToastrService,
  ) {
    this.Loadlist();
    this.GetShit();
    this.GetKamjari();
    this.GetKamjariData();
    this.GetDivision();
    this.WBatchLoadlist();
    this.WorkerManagementLoadlist();
    this.GetAttendance();

    this.GetBatch();
    this.GetBatchData();

    this.GetBook();
    this.GetBookData();
    this.GetGender();
    this.GetWorkerType();
    this.WorkerTypeLoadlist();
    this.WorkerLeaveLoadlist();
    this.GetJobStation();
    this.GetWorkerGrouplist();
  }

  Loadlist() {
    this.gridjsList$ = this.service.countries$;
    this.total$ = this.service.total$;
  }

  GetDataByID(id: any) {
    this.service.GetDataByID(id).subscribe((data: any[]) => {
      this.EMasterForm.controls['EID'].setValue(data[0].id);
      this.EMasterForm.controls['ECode'].setValue(data[0].code);
      this.EMasterForm.controls['EName'].setValue(data[0].name);
      this.EMasterForm.controls['ERetirement'].setValue(data[0].isretirement);
      this.PID = data[0].id;
    });
  }

  MasterForm: FormGroup = new FormGroup({
    ID: new FormControl('0'),
    Name: new FormControl(''),
    Code: new FormControl(''),
    Retirement: new FormControl(''),
    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  EMasterForm: FormGroup = new FormGroup({
    EID: new FormControl('0'),
    EName: new FormControl(''),
    ECode: new FormControl(''),
    ERetirement: new FormControl(''),
  });

  WLMASTER: FormGroup = new FormGroup({
    LEAVECODE: new FormControl(''),
    ATTENDENCE: new FormControl('0'),
    MAXALLOWED: new FormControl(''),
    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });
  EWLMASTER: FormGroup = new FormGroup({
    EID: new FormControl('0'),
    ELEAVECODE: new FormControl(''),
    EATTENDENCE: new FormControl('0'),
    EMAXALLOWED: new FormControl(''),
  });

  Reset() {
    this.MasterForm.reset();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.MasterForm.controls;
  }

  ngOnInit(): void {
    this.GetUnit();
    this.Loadlist();
  }

  WLCreate(): void {
    this.submitted = true;
    if (this.WLMASTER.value.LEAVECODE == '') {
      this.toastr.error('Enter LeaveCode.', 'close');
      this.submitted = false;
    } else if (this.WLMASTER.value.ATTENDENCE == '') {
      this.toastr.error('Enter Attendence.', 'close');
      this.submitted = false;
    } else if (this.WLMASTER.value.MAXALLOWED == '') {
      this.toastr.error('Enter Maxallowed.', 'close');
      this.submitted = false;
    }
    if (this.submitted) {
      this.workerleveservice
        .WLCreate(this.WLMASTER.value)
        .subscribe((res: any) => {
          if (res.item2.includes('success')) {
            this.toastr.success(res.item2, res.item1);
            this.Reset();
          }
        });
    }
  }

  Create(): void {
    this.submitted = true;
    if (this.MasterForm.value.Name == '') {
      this.toastr.error('Enter Plucking Name.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.Code == '') {
      this.toastr.error('Enter Plucking Code.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      this.service.Save(this.MasterForm.value).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item2, res.item1);
          this.Reset();
        }
      });
    }
  }
  Update(): void {
    this.submitted = true;
    if (this.EMasterForm.value.EName == '') {
      this.toastr.error('Enter Plucking Name.', 'close');
      this.submitted = false;
    } else if (this.EMasterForm.value.ECode == '') {
      this.toastr.error('Enter Plucking Code.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      var obj = {
        ID: this.EMasterForm.value.EID,
        NAME: this.EMasterForm.value.EName,
        CODE: this.EMasterForm.value.ECode,
        ISRETIREMENT: this.EMasterForm.value.ERetirement,
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

  GetKamjari() {
    this.cs.GetKamjari().subscribe((data: any[]) => {
      this.kamjaritypelist = data;
    });
  }

  GetKamjariData() {
    this.cs.GetKamjariData().subscribe((data: any[]) => {
      this.kamjarilist = data;
    });
  }

  GetWorkerGrouplist() {
    this.cs.GetWorkerGrouplist().subscribe((data: any[]) => {
      this.workergrouplist = data;
    });
  }
  GetJobStation() {
    this.cs.GetJobStation().subscribe((data: any[]) => {
      this.jobstationlist = data;
    });
  }
  GetAttendance() {
    this.cs.GetAttendance().subscribe((data: any[]) => {
      this.attendancelist = data;
    });
  }
  GetDivision() {
    this.cs.GetDivision().subscribe((data: any[]) => {
      this.divisionlist = data;
    });
  }

  GetShit() {
    this.cs.GetShit().subscribe((data: any[]) => {
      this.shiftlist = data;
    });
  }

  GetGender() {
    this.cs.GetGender().subscribe((data: any[]) => {
      this.genderlist = data;
    });
  }

  GetBook() {
    this.cs.GetBook().subscribe((data: any[]) => {
      this.booklist = data;
    });
  }

  GetBookData() {
    this.service.GetData().subscribe((data: any[]) => {
      this.booklistdata = data;
    });
  }

  GetWorkerType() {
    this.cs.GetWorkerType().subscribe((data: any[]) => {
      this.workertypelist = data;
    });
  }

  GetBatch() {
    this.cs.GetBatch().subscribe((data: any[]) => {
      this.batchlist = data;
    });
  }

  GetBatchData() {
    this.servicebatch.GetData().subscribe((data: any[]) => {
      this.batchlistdata = data;
    });
  }

  /* Worker Batch Start */
  WBatchLoadlist() {
    this.gridworkerbatchList$ = this.servicebatch.countries$;
    this.totalwbatch$ = this.servicebatch.total$;
  }

  WBatchGetDataByID(id: any) {
    this.servicebatch.GetDataByID(id).subscribe((data: any[]) => {
      this.EWBatchMasterForm.controls['EBATCHID'].setValue(data[0].id);
      this.EWBatchMasterForm.controls['ESHIFTID'].setValue(data[0].shiftid);
      this.EWBatchMasterForm.controls['EDIVISIONID'].setValue(
        data[0].divisionid,
      );
      this.EWBatchMasterForm.controls['EBATCHNAME'].setValue(data[0].name);
      this.EWBatchMasterForm.controls['EKAMJARITYPEID'].setValue(
        data[0].kamjaritypeid,
      );
      this.EWBatchMasterForm.controls['EISOUTSIDEWORKER'].setValue(
        data[0].isoutsideworker,
      );
    });
  }

  WBatchMasterForm: FormGroup = new FormGroup({
    ID: new FormControl('0'),
    BATCHNAME: new FormControl(''),
    KAMJARITYPEID: new FormControl(''),
    SHIFTID: new FormControl(''),
    DIVISIONID: new FormControl(''),
    ISOUTSIDEWORKER: new FormControl(false),
    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  EWBatchMasterForm: FormGroup = new FormGroup({
    EBATCHID: new FormControl('0'),
    EBATCHNAME: new FormControl(''),
    EKAMJARITYPEID: new FormControl('0'),
    ESHIFTID: new FormControl('0'),
    EDIVISIONID: new FormControl('0'),
    EISOUTSIDEWORKER: new FormControl(false),
    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  WBatchReset() {
    this.MasterForm.reset();
  }

  get WBatchf(): { [key: string]: AbstractControl } {
    return this.MasterForm.controls;
  }

  WBatchonSort(column: any) {
    this.griddata = this.sortService.onSort(column, this.griddata);
  }

  WBatchCreate(): void {
    this.submitted = true;
    if (this.WBatchMasterForm.value.BATCHNAME == '') {
      this.toastr.error('Enter Batch Name.', 'close');
      this.submitted = false;
    } else if (
      this.WBatchMasterForm.value.KAMJARITYPEID == '0' ||
      this.WBatchMasterForm.value.KAMJARITYPEID == '--Select--'
    ) {
      this.toastr.error('Please Select Kamjari Type.', 'close');
      this.submitted = false;
    } else if (
      this.WBatchMasterForm.value.SHIFTID == '0' ||
      this.WBatchMasterForm.value.SHIFTID == '--Select--'
    ) {
      this.toastr.error('Please Select Shift.', 'close');
      this.submitted = false;
    } else if (
      this.WBatchMasterForm.value.DIVISIONID == '0' ||
      this.WBatchMasterForm.value.DIVISIONID == '--Select--'
    ) {
      this.toastr.error('Please Select Division.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      this.servicebatch
        .Save(this.WBatchMasterForm.value)
        .subscribe((res: any) => {
          if (res.item2.includes('success')) {
            this.toastr.success(res.item2, res.item1);
            this.Reset();
          }
        });
    }
  }

  WBatchUpdate(): void {
    this.submitted = true;
    if (this.EWBatchMasterForm.value.EBATCHNAME == '') {
      this.toastr.error('Enter Batch Name.', 'close');
      this.submitted = false;
    } else if (
      this.EWBatchMasterForm.value.EKAMJARITYPEID == '0' ||
      this.EWBatchMasterForm.value.EKAMJARITYPEID == '--Select--'
    ) {
      this.toastr.error('Please Select Kamjari Type.', 'close');
      this.submitted = false;
    } else if (
      this.EWBatchMasterForm.value.ESHIFTID == '0' ||
      this.EWBatchMasterForm.value.ESHIFTID == '--Select--'
    ) {
      this.toastr.error('Please Select Shift.', 'close');
      this.submitted = false;
    } else if (
      this.EWBatchMasterForm.value.EDIVISIONID == '0' ||
      this.EWBatchMasterForm.value.EDIVISIONID == '--Select--'
    ) {
      this.toastr.error('Please Select Division.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      var obj = {
        ID: this.EWBatchMasterForm.value.EBATCHID,
        BATCHNAME: this.EWBatchMasterForm.value.EBATCHNAME,
        KAMJARITYPEID: this.EWBatchMasterForm.value.EKAMJARITYPEID,
        SHIFTID: this.EWBatchMasterForm.value.ESHIFTID,
        DIVISIONID: this.EWBatchMasterForm.value.EDIVISIONID,
        ISOUTSIDEWORKER: this.EWBatchMasterForm.value.EISOUTSIDEWORKER,
        UNITID: localStorage.getItem('LoginUnit'),
        ENROLL: localStorage.getItem('Enroll'),
      };

      this.servicebatch.Update(obj).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item1, res.item2);
        }
      });
    }
  }

  WBatchDelete(id: any): void {
    this.servicebatch
      .Delete(id, localStorage.getItem('LoginUnit'))
      .subscribe((res: any) => {
        if (res.item2.includes('error')) {
          this.toastr.error(res.item1, 'Delete');
          this.Loadlist();
        }
      });
  }

  /* Worker Batch Start */

  onChangePage(workerItems: Array<any>) {
    // update current page of items
    this.workerItems = workerItems;
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
      this.items = this.items.filter(
        (item) =>
          item.name.toLowerCase().includes(text.toLowerCase()) ||
          item.code.toLowerCase().includes(text.toLowerCase()),
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

  WorkerLeaveLoadlist() {
    this.gridworkerleaveList$ = this.workerleveservice.countries$;
    this.totalworkerleave$ = this.workerleveservice.total$;
  }

  WorkerManagementLoadlist() {
    this.serviceworkermanagement.GetData().subscribe((data: any[]) => {
      this.items = data;
      this.tempitems = data;
    });
  }

  WorkerManagementGetDataByID(id: any) {
    this.serviceworkermanagement.GetDataByID(id).subscribe((data: any[]) => {
      this.EWorkerManagementMasterForm.controls['EWMWORKERID'].setValue(
        data[0].id,
      );
      this.EWorkerManagementMasterForm.controls['EWMNAME'].setValue(
        data[0].name,
      );
      this.EWorkerManagementMasterForm.controls['EWMCODE'].setValue(
        data[0].code,
      );
      this.EWorkerManagementMasterForm.controls['EWMPFNumber'].setValue(
        data[0].pfnumber,
      );
      this.EWorkerManagementMasterForm.controls['EWMBATCHID'].setValue(
        data[0].batchid,
      );
      this.EWorkerManagementMasterForm.controls['EWMDIVISIONID'].setValue(
        data[0].divisionid,
      );
      this.EWorkerManagementMasterForm.controls['EWMEMPNO'].setValue(
        data[0].empno,
      );

      this.EWorkerManagementMasterForm.controls['EWMGENDERID'].setValue(
        data[0].genderid,
      );
      this.EWorkerManagementMasterForm.controls['EWMWORKERTYPEID'].setValue(
        data[0].workertypeid,
      );
      this.EWorkerManagementMasterForm.controls['EWMBOOKID'].setValue(
        data[0].bookid,
      );
      this.EWorkerManagementMasterForm.controls['EWMPLUCKINGBATCHID'].setValue(
        data[0].pluckingbatchid,
      );
      this.EWorkerManagementMasterForm.controls['EWMBATCHID'].setValue(
        data[0].batchid,
      );
      this.EWorkerManagementMasterForm.controls['EWMKAMJARIID'].setValue(
        data[0].kamjariid,
      );

      this.EWorkerManagementMasterForm.controls['EWMDATEOFBIRTH'].setValue(
        data[0].dateofbirth,
      );
      this.EWorkerManagementMasterForm.controls['EWMCONTACTNUMBER'].setValue(
        data[0].contactnumber,
      );
      this.EWorkerManagementMasterForm.controls['EWMADDRESS'].setValue(
        data[0].address,
      );
      this.EWorkerManagementMasterForm.controls[
        'EWMDISABLEDDISINCENTIVEID'
      ].setValue(data[0].disableddisincentiveid);
      this.EWorkerManagementMasterForm.controls['EWMMONTHLYRATED'].setValue(
        data[0].monthlyrated,
      );
      this.EWorkerManagementMasterForm.controls['EWMESSENTIALWORKER'].setValue(
        data[0].essentialworker,
      );
      this.EWorkerManagementMasterForm.controls['EWMRETIREDID'].setValue(
        data[0].retiredid,
      );
    });
  }

  WorkerManagementMasterForm: FormGroup = new FormGroup({
    ID: new FormControl('0'),
    NAME: new FormControl(''),
    CODE: new FormControl(''),
    PFNumber: new FormControl(''),
    DIVISIONID: new FormControl(''),
    EMPNO: new FormControl(''),

    GENDERID: new FormControl('0'),
    WORKERTYPEID: new FormControl(''),
    BOOKID: new FormControl(''),
    PLUCKINGBATCHID: new FormControl(''),
    BATCHID: new FormControl(''),
    KAMJARIID: new FormControl(false),

    DATEOFBIRTH: new FormControl('0'),
    WORKGROUP: new FormControl('0'),
    JOBSTATION: new FormControl('0'),
    CONTACTNUMBER: new FormControl(''),
    ADDRESS: new FormControl(''),
    DISABLEDDISINCENTIVEID: new FormControl(false),
    MONTHLYRATED: new FormControl(false),
    ESSENTIALWORKER: new FormControl(false),
    RETIREDID: new FormControl(false),

    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  EWorkerManagementMasterForm: FormGroup = new FormGroup({
    EWMWORKERID: new FormControl('0'),
    EWMNAME: new FormControl(''),
    EWMCODE: new FormControl(''),
    EWMPFNumber: new FormControl(''),
    EWMDIVISIONID: new FormControl(''),
    EWMEMPNO: new FormControl(''),

    EWMGENDERID: new FormControl('0'),
    EWMWORKERTYPEID: new FormControl(''),
    EWMBOOKID: new FormControl(''),
    EWMPLUCKINGBATCHID: new FormControl(''),
    EWMBATCHID: new FormControl(''),
    EWMKAMJARIID: new FormControl(false),
    EWORKGROUP: new FormControl('0'),
    EJOBSTATION: new FormControl('0'),
    EWMDATEOFBIRTH: new FormControl('0'),
    EWMCONTACTNUMBER: new FormControl(''),
    EWMADDRESS: new FormControl(''),
    EWMDISABLEDDISINCENTIVEID: new FormControl(false),
    EWMMONTHLYRATED: new FormControl(false),
    EWMESSENTIALWORKER: new FormControl(false),
    EWMRETIREDID: new FormControl(false),

    EUNITID: new FormControl(localStorage.getItem('LoginUnit')),
    EENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  WorkerManagementReset() {
    this.WorkerManagementMasterForm.reset();
  }

  get WorkerManagementf(): { [key: string]: AbstractControl } {
    return this.MasterForm.controls;
  }

  WorkerManagementonSort(column: any) {
    this.griddata = this.sortService.onSort(column, this.griddata);
  }

  WorkerManagementCreate(): void {
    this.submitted = true;
    if (this.WorkerManagementMasterForm.value.NAME == '') {
      this.toastr.error('Enter Name.', 'close');
      this.submitted = false;
    } else if (this.WorkerManagementMasterForm.value.CODE == '') {
      this.toastr.error('Enter Code.', 'close');
      this.submitted = false;
    } else if (
      this.WorkerManagementMasterForm.value.DIVISIONID == '0' ||
      this.WBatchMasterForm.value.DIVISIONID == '--Select--'
    ) {
      this.toastr.error('Please Select Division.', 'close');
      this.submitted = false;
    } else if (
      this.WorkerManagementMasterForm.value.GENDERID == '0' ||
      this.WBatchMasterForm.value.GENDERID == '--Select--'
    ) {
      this.toastr.error('Please Select Gender.', 'close');
      this.submitted = false;
    } else if (
      this.WorkerManagementMasterForm.value.WORKERTYPEID == '0' ||
      this.WBatchMasterForm.value.WORKERTYPEID == '--Select--'
    ) {
      this.toastr.error('Please Select Worker Type.', 'close');
      this.submitted = false;
    } else if (
      this.WorkerManagementMasterForm.value.BOOKID == '0' ||
      this.WBatchMasterForm.value.BOOKID == '--Select--'
    ) {
      this.toastr.error('Please Select Book.', 'close');
      this.submitted = false;
    } else if (
      this.WorkerManagementMasterForm.value.PLUCKINGBATCHID == '0' ||
      this.WBatchMasterForm.value.PLUCKINGBATCHID == '--Select--'
    ) {
      this.toastr.error('Please Select Plucking Batch.', 'close');
      this.submitted = false;
    } else if (
      this.WorkerManagementMasterForm.value.BATCHID == '0' ||
      this.WBatchMasterForm.value.BATCHID == '--Select--'
    ) {
      this.toastr.error('Please Select Batch.', 'close');
      this.submitted = false;
    } else if (
      this.WorkerManagementMasterForm.value.KAMJARIID == '0' ||
      this.WBatchMasterForm.value.KAMJARIID == '--Select--'
    ) {
      this.toastr.error('Please Select Kamjari.', 'close');
      this.submitted = false;
    } else if (this.WorkerManagementMasterForm.value.DATEOFBIRTH == '') {
      this.toastr.error('Please Select Date of Birth.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      this.serviceworkermanagement
        .Save(this.WorkerManagementMasterForm.value)
        .subscribe((res: any) => {
          if (res.item2.includes('success')) {
            this.toastr.success(res.item2, res.item1);
            this.Reset();
          } else {
            this.toastr.error(res.item2, res.item1);
          }
        });
    }
  }

  WorkerManagementUpdate(): void {
    this.submitted = true;
    this.submitted = true;
    if (this.EWorkerManagementMasterForm.value.EWMNAME == '') {
      this.toastr.error('Enter Name.', 'close');
      this.submitted = false;
    } else if (this.EWorkerManagementMasterForm.value.EWMCODE == '') {
      this.toastr.error('Enter Code.', 'close');
      this.submitted = false;
    } else if (
      this.EWorkerManagementMasterForm.value.EWMDIVISIONID == '0' ||
      this.EWorkerManagementMasterForm.value.EWMDIVISIONID == '--Select--'
    ) {
      this.toastr.error('Please Select Division.', 'close');
      this.submitted = false;
    } else if (
      this.EWorkerManagementMasterForm.value.EWMGENDERID == '0' ||
      this.EWorkerManagementMasterForm.value.EWMGENDERID == '--Select--'
    ) {
      this.toastr.error('Please Select Gender.', 'close');
      this.submitted = false;
    } else if (
      this.EWorkerManagementMasterForm.value.EWMWORKERTYPEID == '0' ||
      this.EWorkerManagementMasterForm.value.EWMWORKERTYPEID == '--Select--'
    ) {
      this.toastr.error('Please Select Worker Type.', 'close');
      this.submitted = false;
    } else if (
      this.EWorkerManagementMasterForm.value.EWMBOOKID == '0' ||
      this.EWorkerManagementMasterForm.value.EWMBOOKID == '--Select--'
    ) {
      this.toastr.error('Please Select Book.', 'close');
      this.submitted = false;
    } else if (
      this.EWorkerManagementMasterForm.value.EWMPLUCKINGBATCHID == '0' ||
      this.EWorkerManagementMasterForm.value.EWMPLUCKINGBATCHID == '--Select--'
    ) {
      this.toastr.error('Please Select Plucking Batch.', 'close');
      this.submitted = false;
    } else if (this.EWorkerManagementMasterForm.value.EWMDATEOFBIRTH == '') {
      this.toastr.error('Please Select Date of Birth.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      var obj = {
        ID: this.EWorkerManagementMasterForm.value.EWMWORKERID,
        NAME: this.EWorkerManagementMasterForm.value.EWMNAME,
        CODE: this.EWorkerManagementMasterForm.value.EWMCODE,
        PFNumber: this.EWorkerManagementMasterForm.value.EWMPFNumber,
        DIVISIONID: this.EWorkerManagementMasterForm.value.EWMDIVISIONID,
        EMPNO: this.EWorkerManagementMasterForm.value.EWMEMPNO,

        GENDERID: this.EWorkerManagementMasterForm.value.EWMGENDERID,
        WORKERTYPEID: this.EWorkerManagementMasterForm.value.EWMWORKERTYPEID,
        BOOKID: this.EWorkerManagementMasterForm.value.EWMBOOKID,
        SHIFTID: this.EWorkerManagementMasterForm.value.EWMPLUCKINGBATCHID,
        PLUCKINGBATCHID:
          this.EWorkerManagementMasterForm.value.EWMPLUCKINGBATCHID,
        BATCHID: this.EWorkerManagementMasterForm.value.EWMKAMJARIID,

        KAMJARIID: this.EWorkerManagementMasterForm.value.EWMKAMJARIID,
        DATEOFBIRTH: this.EWorkerManagementMasterForm.value.EWMDATEOFBIRTH,
        CONTACTNUMBER: this.EWorkerManagementMasterForm.value.EWMCONTACTNUMBER,
        ADDRESS: this.EWorkerManagementMasterForm.value.EWMADDRESS,
        DISABLEDDISINCENTIVEID:
          this.EWorkerManagementMasterForm.value.EWMDISABLEDDISINCENTIVEID,
        MONTHLYRATED: this.EWorkerManagementMasterForm.value.EWMMONTHLYRATED,
        ESSENTIALWORKER:
          this.EWorkerManagementMasterForm.value.EWMESSENTIALWORKER,
        RETIREDID: this.EWorkerManagementMasterForm.value.EWMRETIREDID,
        UNITID: localStorage.getItem('LoginUnit'),
        ENROLL: localStorage.getItem('Enroll'),
      };

      this.serviceworkermanagement.Update(obj).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item1, res.item2);
        }
      });
    }
  }

  WorkerManagementDelete(id: any): void {
    this.serviceworkermanagement
      .Delete(id, localStorage.getItem('LoginUnit'))
      .subscribe((res: any) => {
        if (res.item2.includes('error')) {
          this.toastr.error(res.item1, 'Delete');
          this.Loadlist();
        }
      });
  }

  fullLGModal(fullXlDataModal: any) {
    this.modalService.open(fullXlDataModal, { size: 'xl', centered: true });
  }

  EfullLGModal(efullXlDataModal: any) {
    this.modalService.open(efullXlDataModal, { size: 'xl', centered: true });
  }

  /* worker type Batch Start */
  WorkerTypeLoadlist() {
    this.gridworkertypeList$ = this.serviceworkertype.countries$;
    this.totalworkertype$ = this.servicebatch.total$;
  }

  WLeaveGetDataByID(id: any) {
    this.workerleveservice.GetDataByID(id).subscribe((data: any[]) => {
      console.log(data);
      this.EWLMASTER.controls['ELEAVECODE'].setValue(data[0].leavecode);
      this.EWLMASTER.controls['EID'].setValue(data[0].id);
      this.EWLMASTER.controls['EATTENDENCE'].setValue(data[0].attendence);
      this.EWLMASTER.controls['EMAXALLOWED'].setValue(data[0].maxallowed);
    });
  }

  UpdateWLCreate(): void {
    this.submitted = true;
    if (this.EWLMASTER.value.ELEAVECODE == '') {
      this.toastr.error('Enter LeaveCode.', 'close');
      this.submitted = false;
    } else if (this.EWLMASTER.value.EATTENDENCE == '0') {
      this.toastr.error('Enter Attendence.', 'close');
      this.submitted = false;
    } else if (this.EWLMASTER.value.EMAXALLOWED == '') {
      this.toastr.error('Enter Maxallowed.', 'close');
      this.submitted = false;
    }
    if (this.submitted) {
      var obj = {
        ID: this.EWLMASTER.value.EID,
        LEAVECODE: this.EWLMASTER.value.ELEAVECODE,
        ATTENDENCE: this.EWLMASTER.value.EATTENDENCE,
        MAXALLOWED: this.EWLMASTER.value.EMAXALLOWED,

        UNITID: localStorage.getItem('LoginUnit'),
        ENROLL: localStorage.getItem('Enroll'),
      };
      console.log(obj);
      this.workerleveservice.Update(obj).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item2, res.item1);
          this.Reset();
        }
      });
    }
  }
  WLeaveDelete(id: any): void {
    this.workerleveservice
      .Delete(id, localStorage.getItem('LoginUnit'))
      .subscribe((res: any) => {
        if (res.item2.includes('error')) {
          this.toastr.error(res.item1, 'Delete');
          this.Loadlist();
        }
      });
  }

  Report() {
    const motherUrl = 'Dummy_String';
    const folderUrl = '/TeaLink/WorkerList';
    const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

    const unit = localStorage.getItem('LoginUnit');
    const fromdate = this.MasterForm.value.FROMDATE;
    const todate = this.MasterForm.value.TODATE;
    const bookid = this.MasterForm.value.BOOKID || 0;

    const uniturt = '&Unit=' + unit;
    const dateurt = '&STARTDATE=' + fromdate;
    const todateurt = '&ENDDATE=' + todate;
    const bookidurt = '&BOOKID=' + bookid;

    const fullUrl = `${motherUrl}${encodeURIComponent(
      folderUrl,
    )}${uniturt}${otherUrl}`;

    const newwindow = window.open(
      fullUrl,
      '_blank',
      'scrollbars=yes,toolbar=0,height=400,width=800,top=70,left=50',
    );
    if (newwindow && newwindow.focus) {
      newwindow.focus();
    }
  }

  file: File | null = null;

  onFileChange(event: any) {
    this.file = event.target.files[0];
  }

  uploadWorkerExcel() {
    if (!this.file) return;

    const unitid = localStorage.getItem('LoginUnit');

    this.serviceworkermanagement
      .uploadWorkerExcel(this.file, unitid)
      .subscribe((res: any) => {
        if (res.item1.includes('success')) {
          this.toastr.success(res.item2, res.item1);
        } else if (res.item1.includes('warning')) {
          this.toastr.warning(res.item2, 'Saved with warnings');
        } else if (res.item1.includes('error')) {
          this.toastr.error(res.item2, res.item1);
        }
      });
  }
}
