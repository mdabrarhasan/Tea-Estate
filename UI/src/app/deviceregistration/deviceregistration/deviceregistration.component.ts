import { Component } from '@angular/core';
import { DeviceregistrationService } from '../../service/deviceregistration/deviceregistration.service';
import { PaginationService } from '../../service/pagination/pagination.service';
import { Observable } from 'rxjs';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-deviceregistration',
  templateUrl: './deviceregistration.component.html',
  styleUrl: './deviceregistration.component.css'
})
export class DeviceregistrationComponent {

    items: any[] = [];
  pageOfItems?: Array<any>;
      tempitems:any;
  sortProperty: string = 'id';
  sortOrder = 1;
  loading = false;
  searchText:any;

  submitted = false;
  griddata: any;


  constructor(
    public service: DeviceregistrationService,
    private sortService: PaginationService,
    private toastr: ToastrService
  ) {
    this.Loadlist();
  }

  ngOnInit() {
    
    this.Loadlist();
  }

      Loadlist()
        {
          this.service.GetDeviceInfo().subscribe((data: any[])=>{
             this.items = data;
             this.tempitems = data;
          })  
                              
        }


              onChangePage(pageOfItems: Array<any>) {
          // update current page of items
          this.pageOfItems = pageOfItems;
      }

  Reset() {
    this.MasterForm.reset();
  }


      filterData() {
        const text = this.searchText.toLowerCase().trim();

        if (text) {
          this.items = this.items.filter(item =>item.name.toLowerCase().includes(text)
          );
        } else {
          this.items = [...this.tempitems]; // Reset to original
        }
      }


  MasterForm: FormGroup = new FormGroup({
    Code: new FormControl('', Validators.required),
    Name: new FormControl('', Validators.required),
    
  });

  
  get f(): { [key: string]: AbstractControl } {
    return this.MasterForm.controls;
  }


  onSort(column: any) {
    this.griddata = this.sortService.onSort(column, this.griddata);
  }


Create() : void
{
  this.submitted = true;
  
  if(this.MasterForm.value.Name == "") {
    this.toastr.error("Enter Device Name.", 'error');
    this.submitted = false;
    return;
  }
  else if(this.MasterForm.value.Code == "") {
    this.toastr.error("Enter Device Code.", 'error');
    this.submitted = false;
    return;
  }

  if(this.submitted) {
    if(this.MasterForm.valid) {
      const unitId = parseInt(localStorage.getItem('LoginUnit') || '0');
      const enroll = parseInt(localStorage.getItem('LoginEnroll') || '0');
      
      const deviceData = {
        Code: this.MasterForm.value.Code,
        Name: this.MasterForm.value.Name,
        UnitID: unitId,
        Enroll: enroll
      };
      
      this.service.Save(deviceData).subscribe((res:any) => {
        if(res.item2.includes("success")) {
          this.toastr.success(res.item1, 'Success'); // "Device saved successfully"
          this.Reset();
          this.Loadlist();
        } else if(res.item1.includes("REJ") || res.item2.includes("error")) {
          // Backend rejected due to duplicate
          this.toastr.error("This device already exists!", 'Error');
        } else {
          this.toastr.error(res.item1, 'Error');
        }
      }, (error) => {
        this.toastr.error('Error saving device', 'Error');
      })
    }
  }
}


  ////////////////////////////////////////////////////////////////////////
// Create() : void
// {
//   this.submitted = true;
  
//   if(this.MasterForm.value.Name == "") {
//     this.toastr.error("Enter Device Name.", 'close');
//     this.submitted = false;
//   }
//   else if(this.MasterForm.value.Code == "") {
//     this.toastr.error("Enter Device Code.", 'close');
//     this.submitted = false;
//   }
//   // REMOVED UnitID validation since it's not in form anymore

//   if(this.submitted) {
//     if(this.MasterForm.valid) {
//       // Get UnitID from logged-in user instead of form
//       const unitId = parseInt(localStorage.getItem('LoginUnit') || '0');
//       const enroll = parseInt(localStorage.getItem('LoginEnroll') || '0');
      
//       // Create payload with auto-filled UnitID and Enroll
//       const deviceData = {
//         Code: this.MasterForm.value.Code,
//         Name: this.MasterForm.value.Name,
//         UnitID: unitId,        // Auto-filled from login
//         Enroll: enroll         // Auto-filled from login
//       };
      
//       this.service.Save(deviceData).subscribe((res:any) => {
//         if(res.item2.includes("success")) {
//           this.toastr.success(res.item2, res.item1);
//           this.Reset();
//           this.Loadlist(); // Refresh the list
//         }
//       })
//     }
//   }
// }
/////////////////////////////////////////////////////////////////////////////


  Delete(code: string): void {
    if (confirm('Are you sure you want to delete this device?')) {
      var unitid = parseInt(localStorage.getItem('LoginUnit') || '0');
      
      this.service.Delete(code, unitid).subscribe((res: any) => {
        if (res.item2.includes("success")) { 
          this.toastr.success(res.item1, res.item2); 
          this.Loadlist();
        }
        else {
          this.toastr.error(res.item1, 'Error');  
        }
      }, (error) => {
        this.toastr.error('An error occurred while deleting device.', 'Error');
      });
    }
  }
}