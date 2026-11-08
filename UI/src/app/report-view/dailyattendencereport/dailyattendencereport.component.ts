import { Component } from '@angular/core';
import { CommonService } from '../../service/common/common.service';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroup } from '@angular/forms';
import { GardenmanagementService } from '../../service/gardenmanagement/gardenmanagement.service';

@Component({
  selector: 'app-dailyattendencereport',
  templateUrl: './dailyattendencereport.component.html',
  styleUrl: './dailyattendencereport.component.css',
})
export class DailyattendencereportComponent {
  booklist: any;
  submitted = false;
  sortProperty: string = 'id';
  sortOrder = 1;
  tempitems: any;
  items: any[] = [];
  pageOfItems?: Array<any>;
  constructor(
    private cs: CommonService,
    private toastr: ToastrService,
    public service: GardenmanagementService,
  ) {}
  MasterForm: FormGroup = new FormGroup({
    PLUCKINGDATE: new FormControl(''),

    BOOKID: new FormControl(''),
    REPORTTYPE: new FormControl(''),

    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  GetBook() {
    this.cs.GetBook().subscribe((data: any[]) => {
      this.booklist = data;
    });
  }

  Report() {
    if (this.MasterForm.value.PLUCKINGDATE == '') {
      this.toastr.error('Select Plucking Date.', 'close');
      this.submitted = false;
    } else {
      if (this.MasterForm.value.REPORTTYPE == '1') {
        const motherUrl = 'Dummy_String';
        const folderUrl = '/TeaLink/GetGardenSummaryReport';
        const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

        const unit = localStorage.getItem('LoginUnit');
        const date = this.MasterForm.value.PLUCKINGDATE;
        const bookid = this.MasterForm.value.BOOKID || 0;

        const uniturt = '&UNITID=' + unit;
        const dateurt = '&DATE=' + date;
        const bookidurt = '&BOOKID=' + bookid;

        const fullUrl = `${motherUrl}${encodeURIComponent(
          folderUrl,
        )}${dateurt}${uniturt}${bookidurt}${otherUrl}`;

        const newwindow = window.open(
          fullUrl,
          '_blank',
          'scrollbars=yes,toolbar=0,height=400,width=800,top=70,left=50',
        );
        if (newwindow && newwindow.focus) {
          newwindow.focus();
        }
      }
      if (this.MasterForm.value.REPORTTYPE == '2') {
        this.service
          .GetDailyAttendeceDelete(
            this.MasterForm.value.PLUCKINGDATE,
            this.MasterForm.value.UNITID,
            this.MasterForm.value.BOOKID,
          )
          .subscribe((res: any) => {
            if (res.item2.includes('error')) {
              this.toastr.error(res.item1, 'Delete');
            }
          });
      }
    }
  }
  Fetch() {
    this.service
      .GetDailyAttendece(
        this.MasterForm.value.PLUCKINGDATE,
        this.MasterForm.value.UNITID,
        this.MasterForm.value.BOOKID,
      )
      .subscribe((data: any[]) => {
        console.log(data);
        this.items = data || [];
        this.tempitems = this.items;
        this.onChangePage(this.items.slice(0, 10));
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
  onChangePage(pageOfItems: Array<any>) {
    this.pageOfItems = pageOfItems;
  }
  sortIcon(property: string) {
    if (property === this.sortProperty) {
      return this.sortOrder === 1 ? '☝️' : '👇';
    }
    return '';
  }

  ngOnInit() {
    this.GetBook();
  }
}
