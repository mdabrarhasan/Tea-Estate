import { Component } from '@angular/core';
import { FileuploadserviceService } from '../../service/manualentry/fileuploadservice.service';
import { ToastrService } from 'ngx-toastr';
import { FormControl, FormGroup } from '@angular/forms';
@Component({
  selector: 'app-fileupload',
  templateUrl: './fileupload.component.html',
  styleUrl: './fileupload.component.css',
})
export class FileuploadComponent {
  file: File | null = null;
  isUploading = false;

  constructor(
    public service: FileuploadserviceService,
    private toastr: ToastrService,
  ) {}

  onFileChange(event: any) {
    this.file = event.target.files[0];
  }

  uploadExcel() {
    if (!this.MasterForm.value.TYPEID) {
      this.toastr.warning('Please select a type', 'Warning');
      return;
    }
    if (!this.file) {
      this.toastr.warning('Please choose an Excel file', 'Warning');
      return;
    }

    this.isUploading = true;

    this.service
      .uploadExcel(
        this.file,
        this.MasterForm.value.PLUCKINGDATE,
        this.MasterForm.value.TYPEID,
        this.MasterForm.value.UNITID,
      )
      .subscribe({
        next: (res: any) => {
          this.isUploading = false;
          if (res?.item1?.includes('success')) {
            this.toastr.success(res.item1, res.item2);
          } else {
            this.toastr.error(res?.item2 || 'Upload failed', 'Error', {
              disableTimeOut: true,
              closeButton: true,
            });
          }
        },
        error: (err: any) => {
          this.isUploading = false;
          const msg =
            typeof err?.error === 'string'
              ? err.error
              : err?.message || 'Upload failed';
          this.toastr.error(msg, 'Error', {
            disableTimeOut: true,
            closeButton: true,
          });
        },
      });
  }

  MasterForm: FormGroup = new FormGroup({
    PLUCKINGDATE: new FormControl(''),

    TYPEID: new FormControl(''),

    UNITID: new FormControl(localStorage.getItem('LoginUnit')),
    ENROLL: new FormControl(localStorage.getItem('Enroll')),
  });
}
