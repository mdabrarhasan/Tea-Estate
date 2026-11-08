import { Component } from '@angular/core';
import { CommonService } from '../../service/common/common.service';
import { ShiftmanagementService } from '../../service/shiftmanagement/shiftmanagement.service';
import { PaginationService } from '../../service/pagination/pagination.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
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
import { FormControl, FormGroup } from '@angular/forms';
import { paginationsearchsort } from '../../service/common/paginationsearchsort.service';

export interface GridJsModel {
  id: number;
  name: string;
  code: string;
  isactive: number;
  kamjname: boolean;
  dblyshift: boolean;
  gardenshift: boolean;
  enroll: number;
  unitid: number;
}

@Component({
  selector: 'app-shiftmanagement',
  templateUrl: './shiftmanagement.component.html',
  styleUrl: './shiftmanagement.component.css',
})
export class ShiftmanagementComponent {
  /* worker type Batch Start */

  searchText: string = '';
  griddata: any;
  submitted = false;
  kamjaritypelist: any;
  shiftlistview: any;
  shiftlist: any;
  tempitems: any;
  items: any[] = [];
  pageOfItems?: Array<any>;
  sortProperty: string = 'id';
  sortOrder = 1;
  loading = false;

  constructor(
    public pagination: paginationsearchsort,
    private common: CommonService,
    public service: ShiftmanagementService,
    private sortService: PaginationService,
    private modalService: NgbModal,
    private cs: CommonService,
    private toastr: ToastrService,
  ) {
    this.ShiftList();
    this.GetKamjari();
  }

  ofilterData() {
    const text = this.searchText.toLowerCase().trim();

    if (text) {
      this.items = this.items.filter((item) =>
        item.name.toLowerCase().includes(text),
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

  GetKamjari() {
    this.cs.GetKamjari().subscribe((data: any[]) => {
      this.kamjaritypelist = data;
    });
  }

  ShiftList() {
    this.service.GetData().subscribe((data: any[]) => {
      this.items = data;
      this.tempitems = data;
    });
  }

  filterData() {
    const text = this.searchText.toLowerCase().trim();

    if (text) {
      this.items = this.items.filter((item: any) =>
        item.name.toLowerCase().includes(text),
      );
    } else {
      this.items = [...this.tempitems]; // Reset to original
    }
  }

  Create(): void {
    this.submitted = true;
    if (this.MasterForm.value.Name == '') {
      this.toastr.error('Enter Shift.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.Code == '') {
      this.toastr.error('Enter Code.', 'close');
      this.submitted = false;
    } else if (
      this.MasterForm.value.KAMJARIID == '' ||
      this.MasterForm.value.KAMJARIID == '--Select--'
    ) {
      this.toastr.error('Select Kamjari.', 'close');
      this.submitted = false;
    }
    if (this.submitted) {
      this.service.Save(this.MasterForm.value).subscribe((res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item2, res.item1);
          this.ShiftList();
        }
      });
    }
  }

  MasterForm: FormGroup = new FormGroup({
    ID: new FormControl('0'),
    Name: new FormControl(''),
    Code: new FormControl(''),
    KAMJID: new FormControl(''),
    DBLYShift: new FormControl(false),
    GardenShift: new FormControl(false),
    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  GetDataByID(id: any) {
    this.service.GetDataByID(id).subscribe((data: any[]) => {
      this.shiftlist = data;
    });
  }

  fullLGModal(fullXlDataModal: any) {
    this.modalService.open(fullXlDataModal, { size: 'xl', centered: true });
  }
}
