import { Component } from '@angular/core';
import { CommonService } from '../../service/common/common.service';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import {
  DailypluckingService,
  GridJsModel,
} from '../../service/dailyplucking/dailyplucking.service';
import { FormControl, FormGroup } from '@angular/forms';
import { NonpluckingactivitiesService } from '../../service/nonpluckingactivities/nonpluckingactivities.service';
import { Console } from 'console';
import { PaginationService } from '../../service/pagination/pagination.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-nonpluckingactivities',
  templateUrl: './nonpluckingactivities.component.html',
  styleUrl: './nonpluckingactivities.component.css',
})
export class NonpluckingactivitiesComponent {
  isDisabled: boolean = false;
  PID: number = 0;
  list: any;
  submitted = false;
  gardemlist: any;
  griddata: any;
  divisionlist: any;
  devicelist: any;
  booklist: any;
  batchlist: any;
  operatorlist: any;
  weightmentlist: any;

  pulckinglistmain: any;
  unitid: any;
  enroll: any;

  pulckinglist: any;
  shiftilist: any;
  pluckeddatetemp: any;

  weighmentlistdata: any;
  empattendentlistdata: any;
  kamjarilist: any;
  empid: any;

  wmnt1: number = 0;
  wmnt2: number = 0;
  wmnt3: number = 0;
  wmnt4: number = 0;

  full: number = 0;
  half: number = 0;
  extra: number = 0;
  fullplus: number = 0;
  absent: number = 0;
  dailyplucking = [{}];

  searchText: string = '';
  tempitems: any;
  items: any[] = [];
  pageOfItems?: Array<any>;
  sortProperty: string = 'id';
  sortOrder = 1;
  loading = false;
  dblysearch: string = '';
  TotaAttStatus: string = '';

  constructor(
    public service: NonpluckingactivitiesService,
    private modalService: NgbModal,
    private sortService: PaginationService,
    private cs: CommonService,
    private toastr: ToastrService,
  ) {
    this.GetDivision();
    this.GetBook();
    this.GetBatch();

    this.GetShit();
    this.GetOperator();
    // this.GetWeighment();
    this.GetDevice();
    this.GetKamjari();
  }

  MasterForm: FormGroup = new FormGroup({
    PLUCKINGDATE: new FormControl(''),
    KAMJARI: new FormControl(''),
    BOOKID: new FormControl(''),
    OPERATORID: new FormControl(''),
    DEVICEID: new FormControl(''),
    BATCHID: new FormControl(''),
    KAMJARIID: new FormControl(''),

    WEIGHMENTID: new FormControl(''),
    DIVISIONID: new FormControl(''),
    TYPEID: new FormControl(''),
    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  GetDivision() {
    this.cs.GetDivision().subscribe((data: any[]) => {
      this.divisionlist = data;
    });
  }

  GetKamjari() {
    this.cs.GetKamjari().subscribe((data: any[]) => {
      this.kamjarilist = data;
    });
  }

  GetShit() {
    this.cs.GetShit().subscribe((data: any[]) => {
      this.shiftilist = data;
    });
  }
  GetBook() {
    this.cs.GetBook().subscribe((data: any[]) => {
      this.booklist = data;
    });
  }

  GetBatch() {
    this.cs.GetBatch().subscribe((data: any[]) => {
      this.batchlist = data;
    });
  }

  GetOperator() {
    this.cs.GetOperator().subscribe((data: any[]) => {
      this.operatorlist = data;
    });
  }

  GetDevice() {
    this.cs.GetDevicelist().subscribe((data: any[]) => {
      this.devicelist = data;
    });
  }

  Loadlist(
    pluckingdate: string,
    kamjaritypeid: number,
    bookid: number,
    operatorid: number,
    deviceid: number,
    batchid: number,
    recordid: number,
    divisionid: number,
  ) {
    //console.log(bookid);
    //this.service.GetDailyPlucking(pluckingdate,kamjaritypeid,bookid,operatorid,deviceid,batchid,weighmentid,divisionid);
    this.service
      .GetDailyPlucking(
        pluckingdate,
        kamjaritypeid,
        bookid,
        operatorid,
        deviceid,
        batchid,
        recordid,
        divisionid,
      )
      .subscribe((data: any[]) => {
        this.TotaAttStatus = data[0].totalStatus;
        this.items = data || [];
        this.tempitems = this.items;
        this.onChangePage(this.items.slice(0, 10));
      });
  }

  toggleAll(event: any) {
    const isChecked = event.target.checked;

    this.items.forEach(
      (row: { isselected: boolean }) => (row.isselected = isChecked),
    );
  }

  GetDataEmpCountDateWise(pluckingdate: any) {
    this.service
      .GetDataEmpCountDateWise(pluckingdate)
      .subscribe((data: any[]) => {
        this.full = data[0].full;
        this.half = data[0].half;
        this.extra = data[0].extra;
        this.fullplus = data[0].fullplus;
        this.absent = data[0].absent;
      });
  }

  filterData() {
    const text = this.searchText.toLowerCase().trim();

    if (text) {
      this.items = this.items.filter(
        (item) =>
          item.name.toLowerCase().includes(text) ||
          item.kamjari.toLowerCase().includes(text) ||
          item.bookempno.toLowerCase().includes(text),
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

  Fetch() {
    this.submitted = true;
    if (this.MasterForm.value.PLUCKINGDATE == '') {
      this.toastr.error('Enter Section PLUCKINGDATE.', 'CLOSE');
      this.submitted = false;
    }
    // if (this.MasterForm.value.BOOKID == "") {
    //   this.toastr.error("Enter Section BOOKID.", 'close');
    //   this.submitted = false;
    // }
    // } else if (this.MasterForm.value.DIVISIONID == "") {
    //   this.toastr.error("Enter Section DIVISIONID.", 'close');
    //   this.submitted = false;
    // }
    // else if (this.MasterForm.value.BOOKID == "") {
    //   this.toastr.error("Enter Section BOOKID.", 'close');
    //   this.submitted = false;
    // }
    // else if (this.MasterForm.value.OPERATORID == "") {
    //   this.toastr.error("Enter Section OPERATORID.", 'close');
    //   this.submitted = false;
    // }
    // else if (this.MasterForm.value.DEVICEID == "") {
    //   this.toastr.error("Enter Section DEVICEID.", 'close');
    //   this.submitted = false;
    // }
    // else if (this.MasterForm.value.BATCHID == "") {
    //   this.toastr.error("Enter Section BATCHID.", 'close');
    //   this.submitted = false;
    // }
    // else if (this.MasterForm.value.KAMJARIID == "") {
    //   this.toastr.error("Enter Section KAMJARIID.", 'close');
    //   this.submitted = false;
    // }
    // else if (this.MasterForm.value.WEIGHMENTID == "") {
    //   this.toastr.error("Enter Section Record.", 'close');
    //   this.submitted = false;
    // }
    if (this.submitted) {
      this.Loadlist(
        this.MasterForm.value.PLUCKINGDATE,
        this.MasterForm.value.KAMJARIID,
        this.MasterForm.value.BOOKID,
        this.MasterForm.value.OPERATORID,
        this.MasterForm.value.DEVICEID,
        this.MasterForm.value.BATCHID,
        this.MasterForm.value.WEIGHMENTID,
        this.MasterForm.value.DIVISIONID,
      );
      this.GetDataEmpCountDateWise(this.MasterForm.value.PLUCKINGDATE);
    }
    //this.GetWeighmentData(this.MasterForm.value.PLUCKINGDATE);
  }

  Save() {
    this.submitted = true;
    this.isDisabled = true;
    if (
      this.MasterForm.value.BOOKID == '' ||
      this.MasterForm.value.BOOKID == '0'
    ) {
      this.toastr.error('Select BOOKID.', 'close');
      this.submitted = false;
      this.isDisabled = false;
    }
    const selectedRows = this.items.filter(
      (row: { isselected: boolean }) => row.isselected,
    );

    if (selectedRows.length === 0) {
      this.toastr.error('Select at least one Employee.', 'close');
      this.submitted = false;
      this.isDisabled = false;
    }

    this.unitid = localStorage.getItem('LoginUnit');
    this.enroll = localStorage.getItem('Enroll');
    if (this.submitted) {
      this.service.Save(selectedRows).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.isDisabled = false;
          this.toastr.success(res.item2, res.item1);
          this.items.forEach(
            (row: { isselected: boolean }) => (row.isselected = false),
          );
        }
        if (res.item2.includes('error')) {
          this.isDisabled = false;
          this.toastr.error(res.item2, res.item1);
        }
      });
    }
  }

  Report() {
    if (this.MasterForm.value.PLUCKINGDATE == '') {
      this.toastr.error('Select Plucking Date.', 'close');
      this.submitted = false;
    }

    // else if(this.MasterForm.value.BOOKID=="")
    // {
    //   this.toastr.error("Select Book.", 'close');
    //   this.submitted=false;
    // }
    else {
      const motherUrl = 'Dummy_String';
      const folderUrl = '/TeaLink/NONDailyPluckingReport';
      const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

      const unit = localStorage.getItem('LoginUnit');
      const date = this.MasterForm.value.PLUCKINGDATE;
      const bookid = this.MasterForm.value.BOOKID || 0;

      const uniturt = '&UNITID=' + unit;
      const dateurt = '&DATE=' + date;
      const bookidurt = '&BOOK=' + bookid;

      const fullUrl = `${motherUrl}${encodeURIComponent(
        folderUrl,
      )}${bookidurt}${uniturt}${dateurt}${otherUrl}`;

      const newwindow = window.open(
        fullUrl,
        '_blank',
        'scrollbars=yes,toolbar=0,height=400,width=800,top=70,left=50',
      );
      if (newwindow && newwindow.focus) {
        newwindow.focus();
      }
    }
  }

  Delete(id: any) {
    this.service.Delete(id).subscribe((res: any) => {
      if (res.item2.includes('error')) {
        this.toastr.error(res.item1, 'Delete');
        this.View(this.empid, this.pluckeddatetemp);
      }
    });
  }

  View(id: any, pluckingdate: any) {
    // this.GetKamjari();
    this.empid = id;
    this.pluckeddatetemp = pluckingdate;
    this.service.GetDataByID(id, pluckingdate).subscribe((data: any[]) => {
      this.pulckinglist = data;
    });
  }

  fullLGModal(fullXlDataModal: any) {
    this.modalService.open(fullXlDataModal, { size: 'xl', centered: true });
  }

  filterdbly() {
    //alert(this.dblysearch)

    //this.items = this.items.filter(item => item.dbly && String(item.dbly).includes('0'));

    alert(this.dblysearch.trim());

    const text = this.dblysearch.trim();

    if (this.dblysearch.trim() == '--Select--') {
      this.items = [...this.tempitems];
    } else {
      this.items = this.items.filter((item) => item.dbly.includes(text));
      // Reset to original
    }

    // alert();
    // if (this.dblysearch != "") {
    //   this.items = this.items.filter(item =>item.dbly.toLowerCase().includes(this.dblysearch))
    // } else {
    //   this.items = [...this.tempitems]; // Reset to original
    // }

    // this.pageOfItems?.filter(item => item.dbly=this.dblysearch)
  }

  KamjariUpdate(row: any) {
    //console.log(`Row ID: ${row.id}, Selected Kamjari ID: ${row.kamjariid}`);
    this.submitted = true;
    if (row.teamid == '0') {
      this.toastr.error('Enter Shift .', 'close');
      this.submitted = false;
    }
    if (row.kamjariid == '0') {
      this.toastr.error('Enter Shift .', 'close');
      this.submitted = false;
    }
    if (this.submitted) {
      this.service
        .KamjariUpdate(row.id, row.kamjariid, row.teamid)
        .subscribe((res: any) => {
          if (res.item2.includes('success')) {
            this.toastr.success(res.item2, res.item1);
            this.View(this.empid, this.pluckeddatetemp);
          }
        });
    }
  }
}
