import { Component } from '@angular/core';
import { HolidayService } from '../../service/holiday/holiday.service';
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
import { paginationsearchsort } from '../../service/common/paginationsearchsort.service';
import { FormControl, FormGroup } from '@angular/forms';
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
  selector: 'app-holiday',
  templateUrl: './holiday.component.html',
  styleUrl: './holiday.component.css',
})
export class HolidayComponent {
  submitted = false;
  items: any[] = [];
  pageOfItems?: Array<any>;
  sortProperty: string = 'id';
  sortOrder = 1;
  loading = false;
  searchText: any;

  constructor(
    public pagination: paginationsearchsort,
    private service: HolidayService,
    private toastr: ToastrService,
  ) {
    this.GetData();
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

  sortIcon(property: string) {
    if (property === this.sortProperty) {
      return this.sortOrder === 1 ? '☝️' : '👇';
    }
    return '';
  }

  GetData() {
    this.service.GetData().subscribe((data: any[]) => {
      this.items = data;
    });
  }
  MasterForm: FormGroup = new FormGroup({
    FromDate: new FormControl(''),
    ToDate: new FormControl(''),
    Name: new FormControl(''),
  });

  EMasterForm: FormGroup = new FormGroup({
    EFromDate: new FormControl(''),
    EToDate: new FormControl(''),
    EName: new FormControl(''),
    EID: new FormControl(0),
  });
  Create(): void {
    this.submitted = true;
    if (this.MasterForm.value.Name == '') {
      this.toastr.error('Enter Name .', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.ToDate == '') {
      this.toastr.error('Enter ToDate .', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.FromDate == '0') {
      this.toastr.error('Select FromDate.', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      var obj = {
        Name: this.MasterForm.value.Name,
        FromDate: this.MasterForm.value.FromDate,
        ToDate: this.MasterForm.value.ToDate,
      };

      this.service.Save(obj).subscribe((res: any) => {
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
    this.MasterForm.controls['FROMDATE'].setValue('');
    this.MasterForm.controls['TODATE'].setValue('');
    this.MasterForm.controls['LEAVETYPEID'].setValue(0);
  }
  Delete(id: any) {
    this.service.Delete(id).subscribe((res: any) => {
      if (res.item2.includes('error')) {
        this.toastr.error(res.item1, 'Delete');
        this.GetData();
      }
    });
  }
}
