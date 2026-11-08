import { Component } from '@angular/core';
import { CommonService } from '../service/common/common.service';
import { SalaryconfigurationService } from '../service/salaryconfiguration/salaryconfiguration.service';
import { PaginationService } from '../service/pagination/pagination.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { paginationsearchsort } from '../service/common/paginationsearchsort.service';
export interface GridJsModel {
  id: number;
  unitid: number;
  enroll: number;
  wages: number;
  ot: number;
  workertype: string;
  allowanceapplicable: boolean;
  otallowanceapplicable: boolean;
}
@Component({
  selector: 'app-salaryconfiguration',
  templateUrl: './salaryconfiguration.component.html',
  styleUrl: './salaryconfiguration.component.css',
})
export class SalaryconfigurationComponent {
  griddata: any;
  submitted = false;
  workertypelist: any;
  tempitems: any;
  items: any[] = [];
  pageOfItems?: Array<any>;
  sortProperty: string = 'id';
  sortOrder = 1;
  loading = false;

  constructor(
    public pagination: paginationsearchsort,
    private _common: CommonService,
    public service: SalaryconfigurationService,
    private sortService: PaginationService,
    private modalService: NgbModal,
    private cs: CommonService,
    private toastr: ToastrService,
  ) {
    this.Loadlist();
  }

  // Create(): void {
  //   this.submitted = true;
  //   if (this.MasterForm.value.Wages == '') {
  //     this.toastr.error('Enter wage amount', 'close');
  //     this.submitted = false;
  //   } else if (this.MasterForm.value.OT == '') {
  //     this.toastr.error('Enter OT amount', 'close');
  //     this.submitted = false;
  //   } else if (
  //     this.MasterForm.value.WorkerTypeID == '' ||
  //     this.MasterForm.value.WorkerTypeID == '--Select--'
  //   ) {
  //     this.toastr.error('Select worker type', 'close');
  //     this.submitted = false;
  //   }
  //   if (this.submitted) {
  //     this.service.Save(this.MasterForm.value).subscribe((res: any) => {
  //       if (res.item2.includes('success')) {
  //         this.toastr.success(res.item2, res.item1);
  //         this.Loadlist();
  //       }
  //     });
  //   }
  // }

  Create(): void {
    this.submitted = true;
    Object.keys(this.MasterForm.controls).forEach((key) => {
      this.MasterForm.get(key)?.markAsTouched();
    });

    if (
      this.MasterForm.value.Wages == '' ||
      this.MasterForm.value.Wages == null
    ) {
      this.toastr.error('Enter wage amount', 'Validation Error');
      this.submitted = false;
      return;
    }

    if (this.MasterForm.value.OT == '' || this.MasterForm.value.OT == null) {
      this.toastr.error('Enter OT amount', 'Validation Error');
      this.submitted = false;
      return;
    }

    if (
      this.MasterForm.value.WorkerTypeID == '' ||
      this.MasterForm.value.WorkerTypeID == '--Select--'
    ) {
      this.toastr.error('Select worker type', 'Validation Error');
      this.submitted = false;
      return;
    }

    this.service.Save(this.MasterForm.value).subscribe({
      next: (res: any) => {
        if (res.item2.includes('success')) {
          this.toastr.success(res.item2, res.item1);
          this.Loadlist();
          this.modalService.dismissAll();
          this.MasterForm.reset({
            ID: '0',
            AllowanceApplicable: false,
            OTAllowanceApplicable: false,
            UNITID: localStorage.getItem('LoginUnit'),
            ENROLL: localStorage.getItem('Enroll'),
          });
        } else {
          this.toastr.error(res.item2 || 'Save failed', 'Error');
        }
        this.submitted = false;
      },
      error: (err) => {
        console.error('Error saving:', err);
        this.toastr.error('Failed to save salary configuration', 'Error');
        this.submitted = false;
      },
    });
  }

  // Update(): void {
  //   this.submitted = true;
  //   if (this.EMasterForm.value.EWages == '') {
  //     this.toastr.error('Enter wage amount', 'close');
  //     this.submitted = false;
  //   } else if (this.EMasterForm.value.EOT == '') {
  //     this.toastr.error('Enter OT amount', 'close');
  //     this.submitted = false;
  //   } else if (
  //     this.EMasterForm.value.EWorkerTypeID == '' ||
  //     this.EMasterForm.value.EWorkerTypeID == '--Select--'
  //   ) {
  //     this.toastr.error('Select worker type', 'close');
  //     this.submitted = false;
  //   }
  //   if (this.submitted) {
  //     var obj = {
  //       ID: this.EMasterForm.value.EID,
  //       Wages: this.EMasterForm.value.EWages,
  //       OT: this.EMasterForm.value.EOT,
  //       WorkerTypeID: this.EMasterForm.value.EWorkerTypeID,
  //       AllowanceApplicable: this.EMasterForm.value.EAllowanceApplicable,
  //       OTAllowanceApplicable: this.EMasterForm.value.EOTAllowanceApplicable,

  //       UNITID: localStorage.getItem('LoginUnit'),
  //       ENROLL: localStorage.getItem('Enroll'),
  //     };
  //     this.service.Update(this.EMasterForm.value).subscribe((res: any) => {
  //       if (res.item2.includes('success')) {
  //         this.toastr.success(res.item2, res.item1);
  //       }
  //     });
  //   }
  // }

  Update(): void {
    this.submitted = true;
    if (this.EMasterForm.value.EWages == '') {
      this.toastr.error('Enter wage amount', 'close');
      this.submitted = false;
    } else if (this.EMasterForm.value.EOT == '') {
      this.toastr.error('Enter OT amount', 'close');
      this.submitted = false;
    } else if (
      this.EMasterForm.value.EWorkerTypeID == '' ||
      this.EMasterForm.value.EWorkerTypeID == '--Select--'
    ) {
      this.toastr.error('Select worker type', 'close');
      this.submitted = false;
    }

    if (this.submitted) {
      const obj = {
        ID: this.EMasterForm.value.EID,
        Wages: this.EMasterForm.value.EWages,
        OT: this.EMasterForm.value.EOT,
        WorkerTypeID: this.EMasterForm.value.EWorkerTypeID,
        AllowanceApplicable: this.EMasterForm.value.EAllowanceApplicable,
        OTAllowanceApplicable: this.EMasterForm.value.EOTAllowanceApplicable,
        UNITID: localStorage.getItem('LoginUnit'),
        ENROLL: localStorage.getItem('Enroll'),
      };

      this.service.Update(obj).subscribe({
        next: (res: any) => {
          if (res.item2.includes('success')) {
            this.toastr.success(res.item2, res.item1);
            this.Loadlist();
            this.modalService.dismissAll();
          }
        },
        error: (err) => {
          this.toastr.error('Update failed', 'Error');
          this.submitted = false;
        },
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

  MasterForm: FormGroup = new FormGroup({
    ID: new FormControl('0'),
    Wages: new FormControl('', Validators.required),
    OT: new FormControl('', Validators.required),
    WorkerTypeID: new FormControl('', Validators.required),
    AllowanceApplicable: new FormControl(false),
    OTAllowanceApplicable: new FormControl(false),
    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  EMasterForm: FormGroup = new FormGroup({
    EID: new FormControl('0'),
    EWages: new FormControl(''),
    EOT: new FormControl(''),
    EWorkerTypeID: new FormControl(''),
    EAllowanceApplicable: new FormControl(false),
    EOTAllowanceApplicable: new FormControl(false),
    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });

  // GetDataByID(id: any) {
  //   this.service.GetDataByID(id).subscribe((data: any[]) => {
  //     this.EMasterForm.controls['EID'].setValue(data[0].id);
  //     this.EMasterForm.controls['EWages'].setValue(data[0].wages);
  //     this.EMasterForm.controls['EOT'].setValue(data[0].ot);
  //   });
  // }
  GetDataByID(id: any) {
    this.service.GetDataByID(id).subscribe({
      next: (data: any[]) => {
        console.log('GetDataByID Response:', data);
        if (data && data.length > 0) {
          this.EMasterForm.patchValue({
            EID: data[0].id,
            EWages: data[0].wages,
            EOT: data[0].ot,
            EWorkerTypeID: data[0].workertypeid,
            EAllowanceApplicable: data[0].allowanceapplicable,
            EOTAllowanceApplicable: data[0].otallowanceapplicable,
          });
        }
      },
      error: (err) => {
        console.error('Error loading data:', err);
        this.toastr.error('Failed to load salary configuration', 'Error');
      },
    });
  }

  Reset() {
    this.MasterForm.reset();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.MasterForm.controls;
  }

  ngOnInit(): void {
    this.Loadlist();
    this.GetWorkerType();
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

  GetWorkerType() {
    this.cs.GetWorkerType().subscribe((data: any[]) => {
      this.workertypelist = data;
    });
  }
}
