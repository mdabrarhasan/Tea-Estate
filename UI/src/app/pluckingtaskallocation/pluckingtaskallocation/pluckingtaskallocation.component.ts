import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { PluckingtaskallocationService } from '../../service/pluckingtaskallocation/pluckingtaskallocation.service';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from '../../service/common/common.service';
@Component({
  selector: 'app-pluckingtaskallocation',
  templateUrl: './pluckingtaskallocation.component.html',
  styleUrl: './pluckingtaskallocation.component.css',
})
export class PluckingtaskallocationComponent {
  submitted = false;
  kamjaritypelist: any;
  @ViewChild('detailsItems') tableRef!: ElementRef;
  tableData: any[] = [];
  constructor(
    public service: PluckingtaskallocationService,
    private cs: CommonService,
    private fb: FormBuilder,
    private toastr: ToastrService,
  ) {
    this.GetKamjari();

    this.MasterForm.get('PLUCKINGTYPEID')?.setValue(55);
  }

  Reset() {
    this.MasterForm.reset();
    this.tableData = [];
  }
  MasterForm: FormGroup = new FormGroup({
    PLUCKINGTYPEID: new FormControl(0),
    PLUCKINGDATE: new FormControl(''),

    ELPDLPRATE: new FormControl(0),
    TARGETKG: new FormControl(0),
    MINTASKKG: new FormControl(0),
    MAXTASKKG: new FormControl(0),
    EXTRARATE: new FormControl(0),
    AVGPLUCKED: new FormControl(this.tableData?.[0]?.avg || 0),
    MINPLUCKED: new FormControl(this.tableData?.[0]?.min || 0),
    MAXPLUCKED: new FormControl(this.tableData?.[0]?.max || 0),
    DISABLEDISINCENTIVE: new FormControl(false),
    ENABLEPLUCKINGEXTRA: new FormControl(false),
    ENROLL: new FormControl(0),
    UNIT: new FormControl(0),
  });

  fetchData() {
    this.submitted = true;
    if (this.MasterForm.value.PLUCKINGDATE == '') {
      this.toastr.error('Enter Section Date.', 'close');
      this.submitted = false;
    } else if (this.MasterForm.value.PLUCKINGTYPEID == '0') {
      this.toastr.error('Enter Section Type.', 'close');
      this.submitted = false;
    }
    if (this.submitted) {
      this.service
        .GetDataByID(
          this.MasterForm.value.PLUCKINGTYPEID,
          this.MasterForm.value.PLUCKINGDATE,
        )
        .subscribe((data: any[]) => {
          this.tableData = data;
          this.MasterForm.controls['ELPDLPRATE'].setValue(data[0].elp);
          this.MasterForm.controls['TARGETKG'].setValue(data[0].target);
          this.MasterForm.controls['MINTASKKG'].setValue(data[0].mintask);
          this.MasterForm.controls['MAXTASKKG'].setValue(data[0].maxtask);
          this.MasterForm.controls['EXTRARATE'].setValue(data[0].extrarate);
          this.MasterForm.controls['AVGPLUCKED'].setValue(data[0].avg);
          this.MasterForm.controls['MAXPLUCKED'].setValue(data[0].max);
          this.MasterForm.controls['MINPLUCKED'].setValue(data[0].min);
          this.MasterForm.controls['DISABLEDISINCENTIVE'].setValue(false);
          this.MasterForm.controls['ENABLEPLUCKINGEXTRA'].setValue(false);
          this.MasterForm.controls['ENROLL'].setValue(
            parseInt(localStorage.getItem('Enroll') || '0', 10),
          );
          this.MasterForm.controls['UNIT'].setValue(
            parseInt(localStorage.getItem('LoginUnit') || '0', 10),
          );
        });
    }
  }

  GetKamjari() {
    this.cs.GetKamjariforUnit().subscribe((data: any[]) => {
      this.kamjaritypelist = data;
    });
  }

  getTableData(): void {
    const rows = this.tableRef.nativeElement.querySelectorAll('tbody tr');
    const details: any[] = [];

    rows.forEach((row: any) => {
      const checkbox = row.querySelector('input[type="checkbox"]');

      if (checkbox && checkbox.checked) {
        const BATCH = row.querySelector('.batch')?.value || '';
        const AVGPLUCKQTY =
          parseFloat(row.querySelector('.avgpluckqty')?.value) || 0;
        const MINPLUCKQTY =
          parseFloat(row.querySelector('.minpluckqty')?.value) || 0;
        const MAXPLUCKQTY =
          parseFloat(row.querySelector('.maxpluckqty')?.value) || 0;
        const TARGETQTY =
          parseFloat(row.querySelector('.targetqty')?.value) || 0;
        const MINQTY = parseFloat(row.querySelector('.mintaskqty')?.value) || 0;
        const MAXQTY = parseFloat(row.querySelector('.maxtaskqty')?.value) || 0;
        const BATCHID = parseFloat(row.querySelector('.batchid')?.value) || 0;
        const BOOKID = parseFloat(row.querySelector('.bookid')?.value) || 0;
        const OPERATORID =
          parseFloat(row.querySelector('.operatorid')?.value) || 0;
        const DEVICEID = parseFloat(row.querySelector('.deviceid')?.value) || 0;
        const KAMJARITYPEID =
          parseFloat(row.querySelector('.kamjaritypeid')?.value) || 0;
        const WEIGHMENTID =
          parseFloat(row.querySelector('.weighmentid')?.value) || 0;

        details.push({
          BATCH,
          AVGPLUCKQTY,
          MINPLUCKQTY,
          MAXPLUCKQTY,
          TARGETQTY,
          MINQTY,
          MAXQTY,
          BATCHID,
          BOOKID,
          OPERATORID,
          DEVICEID,
          KAMJARITYPEID,
          WEIGHMENTID,
        });
      }
    });

    console.log(details);

    this.service.Save(this.MasterForm.value, details).subscribe((res: any) => {
      if (res.item1.includes('success')) {
        this.toastr.success(res.item1, res.item2);
        this.fetchData();
      }
    });
  }
  toggleAll(event: any) {
    const isChecked = event.target.checked;
    this.tableData.forEach(
      (row: { isselected: boolean }) => (row.isselected = isChecked),
    );
  }

  onSelectionChange() {}
}
