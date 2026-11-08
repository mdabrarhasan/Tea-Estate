import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CommonService } from '../../service/common/common.service';
import { ToastrService } from 'ngx-toastr';
import { GardenmanagementService } from '../../service/gardenmanagement/gardenmanagement.service';

@Component({
  selector: 'app-pfandabsentreport',
  templateUrl: './pfandabsentreport.component.html',
  styleUrl: './pfandabsentreport.component.css',
})
export class PfandabsentreportComponent {
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
    FROMDATE: new FormControl(''),
    TODATE: new FormControl(''),

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
        const folderUrl = '/TeaLink/PF%20Details';
        const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

        const unit = localStorage.getItem('LoginUnit');
        const fromdate = this.MasterForm.value.FROMDATE;
        const todate = this.MasterForm.value.TODATE;
        const bookid = this.MasterForm.value.BOOKID || 0;

        const dateurt = '&SDATE=' + fromdate;
        const todateurt = '&EDATE=' + todate;
        const uniturt = '&UNITID=' + unit;
        const bookidurt = '&BOOKID=' + bookid;

        const fullUrl = `${motherUrl}${encodeURIComponent(
          folderUrl,
        )}${dateurt}${todateurt}${uniturt}${bookidurt}${otherUrl}`;

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
        const motherUrl = 'Dummy_String';
        const folderUrl = '/TeaLink/PF Summary';
        const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

        const unit = localStorage.getItem('LoginUnit');
        const fromdate = this.MasterForm.value.FROMDATE;
        const todate = this.MasterForm.value.TODATE;
        const bookid = this.MasterForm.value.BOOKID || 0;

        const uniturt = '&UNITID=' + unit;
        const bookidurt = '&BOOKID=' + bookid;
        const dateurt = '&FROMDATE=' + fromdate;
        const todateurt = '&TODATE=' + todate;

        const fullUrl = `${motherUrl}${encodeURIComponent(
          folderUrl,
        )}${uniturt}${bookidurt}${dateurt}${todateurt}${otherUrl}`;

        const newwindow = window.open(
          fullUrl,
          '_blank',
          'scrollbars=yes,toolbar=0,height=400,width=800,top=70,left=50',
        );
        if (newwindow && newwindow.focus) {
          newwindow.focus();
        }
      }
      if (this.MasterForm.value.REPORTTYPE == '3') {
        const motherUrl = 'Dummy_String';
        const folderUrl = '/TeaLink/AbsentList';
        const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

        const unit = localStorage.getItem('LoginUnit');
        const fromdate = this.MasterForm.value.FROMDATE;
        const todate = this.MasterForm.value.TODATE;
        const bookid = this.MasterForm.value.BOOKID || 0;

        const uniturt = '&UNITID=' + unit;
        const bookidurt = '&BOOKID=' + bookid;
        const dateurt = '&STARTDATE=' + fromdate;
        const todateurt = '&ENDDATE=' + todate;

        const fullUrl = `${motherUrl}${encodeURIComponent(
          folderUrl,
        )}${uniturt}${bookidurt}${dateurt}${todateurt}${otherUrl}`;

        const newwindow = window.open(
          fullUrl,
          '_blank',
          'scrollbars=yes,toolbar=0,height=400,width=800,top=70,left=50',
        );
        if (newwindow && newwindow.focus) {
          newwindow.focus();
        }
      }
      if (this.MasterForm.value.REPORTTYPE == '4') {
        const motherUrl = 'Dummy_String';
        const folderUrl = '/TeaLink/SectionList';
        const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

        const unit = localStorage.getItem('LoginUnit');
        const fromdate = this.MasterForm.value.FROMDATE;
        const todate = this.MasterForm.value.TODATE;
        const bookid = this.MasterForm.value.BOOKID || 0;

        const uniturt = '&UNITID=' + unit;
        const bookidurt = '&BOOKID=' + bookid;
        const dateurt = '&STARTDATE=' + fromdate;
        const todateurt = '&ENDDATE=' + todate;

        const fullUrl = `${motherUrl}${encodeURIComponent(
          folderUrl,
        )}${uniturt}${bookidurt}${dateurt}${todateurt}${otherUrl}`;

        const newwindow = window.open(
          fullUrl,
          '_blank',
          'scrollbars=yes,toolbar=0,height=400,width=800,top=70,left=50',
        );
        if (newwindow && newwindow.focus) {
          newwindow.focus();
        }
      }
      if (this.MasterForm.value.REPORTTYPE == '5') {
        const motherUrl = 'Dummy_String';
        const folderUrl = '/TeaLink/LeaveList';
        const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

        const unit = localStorage.getItem('LoginUnit');
        const fromdate = this.MasterForm.value.FROMDATE;
        const todate = this.MasterForm.value.TODATE;
        const bookid = this.MasterForm.value.BOOKID || 0;

        const uniturt = '&UNITID=' + unit;
        const dateurt = '&STARTDATE=' + fromdate;
        const todateurt = '&ENDDATE=' + todate;
        const bookidurt = '&BOOKID=' + bookid;

        const fullUrl = `${motherUrl}${encodeURIComponent(
          folderUrl,
        )}${uniturt}${dateurt}${todateurt}${bookidurt}${otherUrl}`;

        const newwindow = window.open(
          fullUrl,
          '_blank',
          'scrollbars=yes,toolbar=0,height=400,width=800,top=70,left=50',
        );
        if (newwindow && newwindow.focus) {
          newwindow.focus();
        }
      }
      if (this.MasterForm.value.REPORTTYPE == '6') {
        const motherUrl = 'Dummy_String';
        const folderUrl = '/TeaLink/GetGardenSummaryRepoRange';
        const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

        const unit = localStorage.getItem('LoginUnit');
        const fromdate = this.MasterForm.value.FROMDATE;
        const todate = this.MasterForm.value.TODATE;
        const bookid = this.MasterForm.value.BOOKID || 0;

        const fromdateurt = '&SDATE=' + fromdate;
        const todateurt = '&EDATE=' + todate;
        const uniturt = '&UNITID=' + unit;
        const bookidurt = '&BOOKID=' + bookid;

        const fullUrl = `${motherUrl}${encodeURIComponent(
          folderUrl,
        )}${fromdateurt}${todateurt}${uniturt}${bookidurt}${otherUrl}`;

        const newwindow = window.open(
          fullUrl,
          '_blank',
          'scrollbars=yes,toolbar=0,height=400,width=800,top=70,left=50',
        );
        if (newwindow && newwindow.focus) {
          newwindow.focus();
        }
      }
      if (this.MasterForm.value.REPORTTYPE == '7') {
        const motherUrl = 'Dummy_String';
        const folderUrl = '/TeaLink/DualPresent';
        const otherUrl = '&rs:Embed=true&rc:LinkTarget=_self';

        const unit = localStorage.getItem('LoginUnit');
        const fromdate = this.MasterForm.value.FROMDATE;
        const todate = this.MasterForm.value.TODATE;
        const bookid = this.MasterForm.value.BOOKID || 0;

        const fromdateurt = '&FromDate=' + fromdate;
        const todateurt = '&ToDate=' + todate;
        const uniturt = '&UnitID=' + unit;
        const bookidurt = '&BookID=' + bookid;

        const fullUrl = `${motherUrl}${encodeURIComponent(
          folderUrl,
        )}${fromdateurt}${todateurt}${uniturt}${bookidurt}${otherUrl}`;

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
