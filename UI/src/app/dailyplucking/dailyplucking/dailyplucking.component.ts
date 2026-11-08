import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../service/common/common.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PaginationService } from '../../service/pagination/pagination.service';
import {
  DailypluckingService,
  GridJsModel,
} from '../../service/dailyplucking/dailyplucking.service';
import { debounceTime, delay, Observable, switchMap, tap } from 'rxjs';
import { FormControl, FormGroup } from '@angular/forms';
import { paginationsearchsort } from '../../service/common/paginationsearchsort.service';
import { runInThisContext } from 'node:vm';

@Component({
  selector: 'app-dailyplucking',
  templateUrl: './dailyplucking.component.html',
  styleUrl: './dailyplucking.component.css',
})
export class DailypluckingComponent {
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

  pulckinglist: any;
  pluckeddatetemp: any;

  weighmentlistdata: any;
  empattendentlistdata: any;
  kamjarilist: any;
  empid: any;
  searchTerm: any;
  filteredpulckinglistmain: any;

  wmnt1: number = 0;
  wmnt2: number = 0;
  wmnt3: number = 0;
  wmnt4: number = 0;
  tw: number = 0;
  tov: number = 0;

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
  TotaAttStatus: string = '';

  constructor(
    public pagination: paginationsearchsort,
    public service: DailypluckingService,
    private sortService: PaginationService,
    private modalService: NgbModal,
    private cs: CommonService,
    private toastr: ToastrService,
  ) {
    this.GetDivision();
    this.GetBook();
    this.GetBatch();

    this.GetOperator();
    this.GetWeighment();
    this.GetDevice();
    this.GetKamjari();
    this.MasterForm.get('KAMJARIID')?.setValue(55);
  }

  searchKey(data: string) {
    this.searchTerm = data;
    this.search();
  }

  search() {
    const text = this.searchText.toLowerCase().trim();

    if (text) {
      this.items = this.items.filter(
        (item) =>
          item.name.toLowerCase().includes(text) ||
          item.bookempno.toLowerCase().includes(text),
      );
    } else {
      this.items = [...this.tempitems]; // Reset to original
    }
  }

  toggleAll(event: any) {
    const isChecked = event.target.checked;
    this.items.forEach(
      (row: { isselected: boolean }) => (row.isselected = isChecked),
    );
  }

  onSelectionChange() {}

  filterData() {
    const text = this.searchText.toLowerCase().trim();

    if (text) {
      this.items = this.items.filter((item) =>
        item.id.toLowerCase().includes(text),
      );
    } else {
      this.items = [...this.tempitems]; // Reset to original
    }
  }

  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }

  applyWeighment(rowData: any) {
    const selectedWeighment = rowData.weighmentid;
    if (!selectedWeighment) {
      this.toastr.warning(
        'Please select a weighment from dropdown',
        'Selection Required',
      );
      return;
    }
    this.toastr.success(
      `Weighment ${selectedWeighment} applied successfully`,
      'Success',
    );
  }

  updateWeighment(rowData: any) {
    const id = rowData.id;
    const targetWeighment = rowData.weighmentid;

    if (!targetWeighment) {
      this.toastr.warning('Please select a weighment!', 'Warning');
      return;
    }

    this.service.UpdateWeighment(id, targetWeighment).subscribe({
      next: (res: any) => {
        const msg = res.item1 || 'Weighment updated';
        const status = res.item2?.toLowerCase() || '';

        if (status.includes('success')) {
          this.toastr.success(msg, 'Success');
        } else {
          this.toastr.error(msg, 'Error');
        }
      },
      error: () => {
        this.toastr.error('Something went wrong!', 'Error');
      },
    });
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

  GetDivision() {
    this.cs.GetDivision().subscribe((data: any[]) => {
      this.divisionlist = data;
    });
  }

  GetKamjari() {
    this.cs.GetKamjariforUnit().subscribe((data: any[]) => {
      this.kamjarilist = data;
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

  GetWeighment() {
    this.cs.GetWeighment().subscribe((data: any[]) => {
      this.weightmentlist = data;
    });
  }

  GetDevice() {
    this.cs.GetDevicelist().subscribe((data: any[]) => {
      this.devicelist = data;
    });
  }

  Save() {
    this.submitted = true;
    if (this.MasterForm.value.PLUCKINGDATE == '') {
      this.toastr.error('SELECT PLUCKING DATE.', 'ERROR');
      this.submitted = false;
    } else if (this.MasterForm.value.BOOKID == 0) {
      this.toastr.error('PLEASE SELECT BOOK.', 'ERROR');
      this.submitted = false;
    }

    const selectedRows = this.items.filter(
      (row: { isselected: boolean }) => row.isselected,
    );
    if (selectedRows.length <= 0) {
      this.toastr.error('PLEASE CHECK ITEM.', 'WARNING');
      this.submitted = false;
    } else {
      if (this.submitted) {
        this.service.Save(selectedRows).subscribe((res: any) => {
          if (res.item2.includes('success')) {
            this.toastr.success(res.item2, res.item1);
          } else if (res.item2.includes('error')) {
            this.toastr.error(res.item2, res.item1);
          } else if (res.item2.includes('WARNING')) {
            this.toastr.error(res.item2, res.item1);
          }
          this.items.forEach(
            (row: { isselected: boolean }) => (row.isselected = false),
          );
        });
      }
    }
  }

  Delete(id: any) {
    this.service.Delete(id).subscribe((res: any) => {
      if (res.item2.includes('error')) {
        this.Loadlist(
          this.MasterForm.value.PLUCKINGDATE,
          this.MasterForm.value.KAMJARIID || 0,
          this.MasterForm.value.BOOKID || 0,
          this.MasterForm.value.OPERATORID || 0,
          this.MasterForm.value.DEVICEID || 0,
          this.MasterForm.value.BATCHID || 0,
          this.MasterForm.value.WEIGHMENTID || 0,
          this.MasterForm.value.DIVISIONID || 0,
        );
        this.toastr.error(res.item1, 'Delete');
        this.View(this.empid, this.pluckeddatetemp);
      }
    });
  }

  View(id: any, pluckingdate: any) {
    this.empid = id;
    this.pluckeddatetemp = pluckingdate;
    this.service.GetDataByID(id, pluckingdate).subscribe((data: any[]) => {
      this.pulckinglist = data;
    });
  }

  GetWeighmentData(pluckingdate: any, bookid: any) {
    this.service
      .GetWeighmentData(pluckingdate, bookid)
      .subscribe((data: any[]) => {
        this.wmnt1 = data[0].w1;
        this.wmnt2 = data[0].w2;
        this.wmnt3 = data[0].w3;
        this.wmnt4 = data[0].w4;
        this.tov = data[0].w1 + data[0].w2 + data[0].w3 + data[0].w4;
        this.tw = data[0].id;
      });
  }

  GetDataEmpCountDateWise(pluckingdate: any, bookid: any) {
    this.service
      .GetDataEmpCountDateWise(pluckingdate, bookid)
      .subscribe((data: any[]) => {
        this.full = data[0].full;
        this.half = data[0].half;
        this.extra = data[0].extra;
        this.fullplus = data[0].fullplus;
        this.absent = data[0].absent;
      });
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

    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  Report() {
    if (this.MasterForm.value.PLUCKINGDATE == '') {
      this.toastr.error('Select Plucking Date.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.BOOKID == '') {
      this.toastr.error('Select Book.', 'close');
      this.submitted = false;
    } else {
      const motherUrl = 'Dummy_String';
      const folderUrl = '/TeaLink/DailyPluckingReport';
      const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

      const unit = localStorage.getItem('LoginUnit');
      const date = this.MasterForm.value.PLUCKINGDATE;
      const bookid = this.MasterForm.value.BOOKID;

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

  Loadlist(
    pluckingdate: string,
    kamjaritypeid: number,
    bookid: number,
    operatorid: number,
    deviceid: number,
    batchid: number,
    weighmentid: number,
    divisionid: number,
  ) {
    this.service
      .GetDailyPlucking(
        pluckingdate,
        kamjaritypeid,
        bookid,
        operatorid,
        deviceid,
        batchid,
        weighmentid,
        divisionid,
      )
      .subscribe((data: any[]) => {
        this.TotaAttStatus = data[0].totalStatus;

        this.items = data || [];
        this.tempitems = this.items;

        this.onChangePage(this.items.slice(0, 10));

        this.items = data || [];
        this.tempitems = data || [];

        this.onChangePage(this.items.slice(0, 0));
      });
  }

  Fetch() {
    if (
      this.MasterForm.value.PLUCKINGDATE == '' ||
      !this.MasterForm.value.PLUCKINGDATE
    ) {
      this.toastr.error('PLEASE SELECT PLUCKING DATE.', 'ERROR');
      return;
    }

    if (
      this.MasterForm.value.BOOKID == 0 ||
      this.MasterForm.value.BOOKID == '' ||
      !this.MasterForm.value.BOOKID
    ) {
      this.toastr.error('PLEASE SELECT A SPECIFIC BOOK', 'ERROR');
      return;
    }
    this.Loadlist(
      this.MasterForm.value.PLUCKINGDATE,
      this.MasterForm.value.KAMJARIID || 0,
      this.MasterForm.value.BOOKID || 0,
      this.MasterForm.value.OPERATORID || 0,
      this.MasterForm.value.DEVICEID || 0,
      this.MasterForm.value.BATCHID || 0,
      this.MasterForm.value.WEIGHMENTID || 0,
      this.MasterForm.value.DIVISIONID || 0,
    );
    this.GetDataEmpCountDateWise(
      this.MasterForm.value.PLUCKINGDATE,
      this.MasterForm.value.BOOKID,
    );
    this.GetWeighmentData(
      this.MasterForm.value.PLUCKINGDATE,
      this.MasterForm.value.BOOKID,
    );
  }

  fullLGModal(fullXlDataModal: any) {
    this.modalService.open(fullXlDataModal, { size: 'xl', centered: true });
  }
}
