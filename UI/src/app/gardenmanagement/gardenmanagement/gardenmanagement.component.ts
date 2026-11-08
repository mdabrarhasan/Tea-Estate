import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { PaginationService } from '../../service/pagination/pagination.service';
import { CommonService } from '../../service/common/common.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { GardenmanagementService, GridJsModel } from '../../service/gardenmanagement/gardenmanagement.service';
import { Console } from 'node:console';

@Component({
  selector: 'app-gardenmanagement',
  templateUrl: './gardenmanagement.component.html',
  styleUrl: './gardenmanagement.component.css'
})

export class GardenmanagementComponent {

  PID:number=0;
  list:any;
  submitted = false;
  gardemlist:any;
  weeklyholidaylist:any;
  intervallist:any;
  gridjsList$!: Observable<GridJsModel[]>;
  total$: Observable<number>=new Observable<number>;
  griddata: any;

   constructor(private common:CommonService,public service: GardenmanagementService,private sortService: PaginationService,private modalService: NgbModal,private cs:CommonService,private toastr: ToastrService) {
      this.Loadlist();
      this.LoadHoliday();
      this.LoadInterval();
      this.GetData();
    }
  
    GetData()
    {
      this.service.GetData().subscribe((data: any[])=>{
        this.griddata = data;
      })  
    }

    LoadHoliday()
    {
      this.common.GetWeeklyHoliday().subscribe((data: any[])=>{
        this.weeklyholidaylist = data;
      })  
    }

    LoadInterval()
    {
      this.common.GetPLKInterval().subscribe((data: any[])=>{
        this.intervallist = data;
      })  
    }


    Loadlist()
    {
      this.gridjsList$ = this.service.countries$;
      this.total$ = this.service.total$; 
    }
  
    GetDataByID(id:any)
    {
     this.service.GetDataByID(id).subscribe((data: any[])=>{
      this.EMasterForm.controls['EID'].setValue(data[0].id);
      this.EMasterForm.controls['ECODE'].setValue(data[0].code);
      this.EMasterForm.controls['ENAME'].setValue(data[0].name);

      this.EMasterForm.controls['EHOLIDAYID'].setValue(data[0].holidayid);
      this.EMasterForm.controls['EINTERVALID'].setValue(data[0].intervalid);

      this.PID=data[0].id;
     });
    }
  
    MasterForm: FormGroup=new FormGroup({
      ID:new FormControl('0'),
      NAME:new FormControl(''),
      CODE:new FormControl(''),
  
      HOLIDAYID:new FormControl('0'),
      INTERVALID:new FormControl('0'),
      UNITID:new FormControl(0),
      ENROLL:new FormControl(0)
    });
  

    EMasterForm: FormGroup=new FormGroup({
      EID:new FormControl('0'),
      ENAME:new FormControl(''),
      ECODE:new FormControl(''),
  
      EHOLIDAYID:new FormControl('0'),
      EINTERVALID:new FormControl('0'),
      UNITID:new FormControl(0),
      ENROLL:new FormControl(0)
    });

    Reset()
    {
      this.MasterForm.reset();
    }
    Clear()
    {
      this.MasterForm.controls['NAME'].setValue('');
      this.MasterForm.controls['CODE'].setValue('');
      this.MasterForm.controls['HOLIDAYID'].setValue(0);
      this.MasterForm.controls['INTERVALID'].setValue(0);
      
    }
    get f(): { [key: string]: AbstractControl } {
      return this.MasterForm.controls;
    }
    
    ngOnInit(): void {
     this.GetUnit();
     this.Loadlist();
     this.MasterForm.controls['ENROLL'].setValue(parseInt(localStorage.getItem('Enroll')|| '0', 10));
      this.MasterForm.controls['UNITID'].setValue(parseInt(localStorage.getItem('LoginUnit') || '0', 10));
    }
  
    onSort(column: any) {
      this.griddata = this.sortService.onSort(column, this.griddata)
    }
  
    Create() : void
    {
      this.submitted=true;
      if(this.MasterForm.value.NAME=="")
      {
        this.toastr.error("Enter Garden Name.", 'close');
        this.submitted=false;
      }
      else if(this.MasterForm.value.CODE=="")
      {
        this.toastr.error("Enter Garden Code.", 'close');
        this.submitted=false;
      }
     else if(this.MasterForm.value.HOLIDAYID=="0")
      {
        this.toastr.error("Select Holiday.", 'close');
        this.submitted=false;
      }
      else if(this.MasterForm.value.INTERVALID=="0")
      {
          this.toastr.error("Select Interval.", 'close');
          this.submitted=false;
      }
     
      if(this.submitted)
      {
        this.service.Save(this.MasterForm.value).subscribe((res:any)=>{
          if(res.item2.includes("success"))
          {
            this.toastr.success(res.item2, res.item1);
            this.Clear();
            this.service.GetData();
          }
        })
      }
    }
    
    Update() : void
    {
      this.submitted=true;
      if(this.EMasterForm.value.ENAME=="")
        {
          this.toastr.error("Enter Garden Name.", 'close');
          this.submitted=false;
        }
        else if(this.EMasterForm.value.ECODE=="")
        {
          this.toastr.error("Enter Garden Code.", 'close');
          this.submitted=false;
        }
        else if(this.EMasterForm.value.EHOLIDAYID=="--Select--")
        {
          this.toastr.error("Select Holiday.", 'close');
          this.submitted=false;
        }
        else if(this.EMasterForm.value.EINTERVALID=="--Select--")
        {
            this.toastr.error("Select Interval.", 'close');
            this.submitted=false;
        }

        if(this.submitted)
        {
          var obj={
            ID:this.EMasterForm.value.EID,
            NAME:this.EMasterForm.value.ENAME,
            CODE:this.EMasterForm.value.ECODE,
        
            HOLIDAYID:this.EMasterForm.value.EHOLIDAYID,
            INTERVALID:this.EMasterForm.value.EINTERVALID,
            UNITID:this.EMasterForm.value.UNITID,
            ENROLL:this.EMasterForm.value.ENROLL
          }
            this.service.Update(obj).subscribe((res:any)=>{
              if(res.item2.includes("success"))
              {
                this.toastr.success(res.item1,res.item2);
              }
            })
        }
      }
  
    Delete(id:any) : void
    {
      this.service.Delete(id,localStorage.getItem('LoginUnit')).subscribe((res:any)=>{
        if(res.item2.includes("error"))
        {
          this.toastr.error(res.item1, "Delete");
          this.Loadlist();
        }
      })
    }

     GetUnit()
    {
      this.cs.GetUnit().subscribe((data: any[])=>{
        this.gardemlist = data;
      })  
    }
}
