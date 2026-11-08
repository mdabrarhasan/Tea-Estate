import { Component } from '@angular/core';
import { DivisionmanagementService, GridJsModel } from '../../service/divisionmanagement/divisionmanagement.service';
import { PaginationService } from '../../service/pagination/pagination.service';
import { Observable } from 'rxjs';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../../service/common/common.service';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-divisionmanagement',
  templateUrl: './divisionmanagement.component.html',
  styleUrl: './divisionmanagement.component.css'
})

export class DivisionmanagementComponent {
  PID:number=0;
  list:any;
  submitted = false;
  gardemlist:any;
  gridjsList$!: Observable<GridJsModel[]>;
  total$: Observable<number>=new Observable<number>;
  griddata: any;
  
  constructor(public service: DivisionmanagementService,private sortService: PaginationService,private modalService: NgbModal,private cs:CommonService,private toastr: ToastrService) {
    this.Loadlist();
  }
  ngOnInit() {
    this.Loadlist();
    this.GetUnit();
  }

  Loadlist()
  {
    this.service.GetDivisionInfo().subscribe((data: any[])=>{
      this.griddata = data;
    })  
  }

  Reset()
  {
    this.MasterForm.reset();
  }

  GetDataByID(id:any)
  {
   this.service.GetDataByID(id).subscribe((data: any[])=>{
    this.EMasterForm.controls['EID'].setValue(data[0].unitID);
    this.EMasterForm.controls['ECode'].setValue(data[0].code);
    this.EMasterForm.controls['EName'].setValue(data[0].name);
    this.PID=data[0].id;
   });
  }

  MasterForm: FormGroup=new FormGroup({
    ID:new FormControl('0',Validators.required),
    Name:new FormControl('',Validators.required),
    Code:new FormControl('',Validators.required)
  });


  EMasterForm: FormGroup=new FormGroup({
    EID:new FormControl('0',Validators.required),
    EName:new FormControl('',Validators.required),
    ECode:new FormControl('',Validators.required)
  });

  get f(): { [key: string]: AbstractControl } {
    return this.MasterForm.controls;
  }
  
  // ngOnInit(): void {
  //  this.GetUnit();
  //  this.Loadlist();
  // }

  onSort(column: any) {
    this.griddata = this.sortService.onSort(column, this.griddata)
  }

  Create() : void
  {
    this.submitted=true;
      if(this.MasterForm.value.Name=="")
      {
        this.toastr.error("Enter Division Name.", 'close');
        this.submitted=false;
      }
      else if(this.MasterForm.value.Code=="")
      {
        this.toastr.error("Enter Division Code.", 'close');
        this.submitted=false;
      }
      else if(this.MasterForm.value.ID=="0" ||  this.MasterForm.value.ID=="--Select--")
        {
          this.toastr.error("Plase Select Division.", 'close');
          this.submitted=false;
        }

      if(this.submitted)
      {
        if(this.MasterForm.valid)
          {
            this.service.Save(this.MasterForm.value).subscribe((res:any)=>{
              if(res.item2.includes("success"))
              {
                this.toastr.success(res.item2, res.item1);
                this.Reset();
              }
            })
          }
      }
  }

  
  Update() : void
  {
      this.submitted=true;
      if(this.EMasterForm.value.EName=="")
      {
        this.toastr.error("Enter Division Name.", 'close');
        this.submitted=false;
      }
      else if(this.EMasterForm.value.ECode=="")
      {
        this.toastr.error("Enter Division Code.", 'close');
        this.submitted=false;
      }
      else if(this.EMasterForm.value.EID=="0" ||  this.EMasterForm.value.EID=="--Select--")
        {
          this.toastr.error("Plase Select Garden.", 'close');
          this.submitted=false;
        }
      
        if(this.submitted)
        {
          var obj={
            ID: this.PID,
            Name: this.EMasterForm.value.EName,
            Code: this.EMasterForm.value.ECode
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
    this.service.Delete(id).subscribe((res:any)=>{
      if(res.item2.includes("error"))
      {
        this.toastr.error(res.item1, "Delete");
        this.Loadlist();
      }
    })
  }
  // GetDivisionInfo()
  // {
  //   this.service.GetDivisionInfo().subscribe((data: any[])=>{
  //     console.log(data);
  //     this.list = data;
  //   })  
  // }


   GetUnit()
  {
    this.cs.GetUnit().subscribe((data: any[])=>{
      this.gardemlist = data;
    })  
  }
}

