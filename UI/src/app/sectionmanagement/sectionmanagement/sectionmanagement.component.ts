import { Component } from '@angular/core';

import {  SectionmanagementService } from '../../service/sectionmanagement/sectionmanagement.service';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { CommonService } from '../../service/common/common.service';
import { PaginationService } from '../../service/pagination/pagination.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, debounceTime, delay, Observable, of, Subject, switchMap, tap } from 'rxjs';
import { paginationsearchsort } from '../../service/common/paginationsearchsort.service';
export interface GridJsModel {
  id: number;
  sectionname: string;
  code: string;
  area: number;
  divisionid: number;
  divisionname: string;
  costcentretype: string;
  slno: number;
  pluckingid: number;
  pluckingsec: string;

  irrigationsection: string;
  irrigationid: number;

  usualcommandarea: string;
  usualsettings: string;
  prunetypeid: number;
  prunetype: string;
}
@Component({
  selector: 'app-sectionmanagement',
  templateUrl: './sectionmanagement.component.html',
  styleUrl: './sectionmanagement.component.css'
})
export class SectionmanagementComponent {

    PID:number=0;
    list:any;
    submitted = false;
    gardemlist:any;
    prunetypelist:any;
    isactivelist:any;
    divisionlist:any;

    searchText:string='';
    tempitems:any;
    items: any[] = [];
    pageOfItems?: Array<any>;
    sortProperty: string = 'id';
    sortOrder = 1;
    loading = false;


     constructor(public pagination:paginationsearchsort,private common:CommonService,public service: SectionmanagementService,private sortService: PaginationService,private modalService: NgbModal,private cs:CommonService,private toastr: ToastrService) {
          this.Loadlist();
          this.GetIsActive();
          this.GetPruneType();
          this.GetDivision();
        }
      
        GetDivision()
        {
          this.common.GetDivision().subscribe((data: any[])=>{
            this.divisionlist = data;
          })  
        }

        GetPruneType()
        {
          this.common.GetPruneType().subscribe((data: any[])=>{
            this.prunetypelist = data;
          })  
        }
    
        GetIsActive()
        {
          this.common.GetIsActive().subscribe((data: any[])=>{
            this.isactivelist = data;
          })  
        }
    
    
        Loadlist()
        {
           this.service.GetData().subscribe((data: any[])=>{
            this.items=data;
            this.tempitems = data;              
             })  
        }
      
        GetDataByID(id:any)
        {
         this.service.GetDataByID(id).subscribe((data: any[])=>{
          this.EMasterForm.controls['EID'].setValue(data[0].id);
          this.EMasterForm.controls['ECode'].setValue(data[0].code);
          this.EMasterForm.controls['EName'].setValue(data[0].sectionname);
          this.EMasterForm.controls['EDivisionID'].setValue(data[0].divisionid);
    
          this.EMasterForm.controls['ECostCentreType'].setValue(data[0].costcentretype);
          this.EMasterForm.controls['EIsPluckingID'].setValue(data[0].pluckingid);
    
          this.EMasterForm.controls['EArea'].setValue(data[0].area);
          this.EMasterForm.controls['EPrunetypeID'].setValue(data[0].prunetypeid);
    

          this.PID=data[0].id;
         });
        }
      
        MasterForm: FormGroup=new FormGroup({
          ID:new FormControl('0'),
          Name:new FormControl(''),
          Code:new FormControl(''),
      
          DivisionID:new FormControl('0'),
          CostCentreType:new FormControl('0'),

          IsPluckingID:new FormControl('0'),
          IsIrrigationID:new FormControl('0'),

          Area:new FormControl('0'),
          PrunetypeID:new FormControl('0'),

          UNITID:new FormControl(localStorage.getItem('LoginUnit')),
          ENROLL:new FormControl(localStorage.getItem('Enroll'))
        });
      
    
        EMasterForm: FormGroup=new FormGroup({
          EID:new FormControl('0'),
          EName:new FormControl(''),
          ECode:new FormControl(''),
      
          EDivisionID:new FormControl('0'),
          ECostCentreType:new FormControl('0'),

          EIsPluckingID:new FormControl('0'),
          EIsIrrigationID:new FormControl('0'),

          EArea:new FormControl('0'),
          EPrunetypeID:new FormControl('0')
        });
    
        Reset()
        {
          //this.MasterForm.reset();
        }
    
        get f(): { [key: string]: AbstractControl } {
          return this.MasterForm.controls;
        }
        
        ngOnInit(): void {
         this.GetUnit();
         this.Loadlist();
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
  

        onChangePage(pageOfItems: Array<any>) {
          this.pageOfItems = pageOfItems;
        }
  
      sortBy(property: string) {
          this.sortOrder = property === this.sortProperty ? (this.sortOrder * -1) : 1;
          this.sortProperty = property;
          this.items = [...this.items.sort((a: any, b: any) => {
              // sort comparison function
              let result = 0;
              if (a[property] < b[property]) {
                  result = -1;
              }
              if (a[property] > b[property]) {
                  result = 1;
              }
              return result * this.sortOrder;
          })];
      }
  
      sortIcon(property: string) {
          if (property === this.sortProperty) {
              return this.sortOrder === 1 ? '☝️' : '👇';
          }
          return '';
      }     
      
        Create() : void
        {
          this.submitted=true;
          if(this.MasterForm.value.Name=="")
          {
            this.toastr.error("Enter Section Name.", 'close');
            this.submitted=false;
          }
          else if(this.MasterForm.value.Code=="")
          {
            this.toastr.error("Enter Section Code.", 'close');
            this.submitted=false;
          }
          else if(this.MasterForm.value.DivisionID=="0")
          {
            this.toastr.error("Select Division.", 'close');
            this.submitted=false;
          }
          else if(this.MasterForm.value.IsPluckingID=="0")
          {
              this.toastr.error("Select Plucking Section.", 'close');
              this.submitted=false;
          }
          else if(this.MasterForm.value.Area=="0")
            {
                this.toastr.error("Enter Area.", 'close');
                this.submitted=false;
            }

          if(this.submitted)
          {
            this.service.Save(this.MasterForm.value).subscribe((res:any)=>{
              if(res.item2.includes("success"))
              {
                this.toastr.success(res.item2, res.item1);
                this.service.GetData();
              }
            })
          }
        }
        
        Update() : void
        {
          console.log(this.EMasterForm.value);
          this.submitted=true;
          if(this.MasterForm.value.EName=="")
            {
              this.toastr.error("Enter Section Name.", 'close');
              this.submitted=false;
            }
            else if(this.MasterForm.value.ECode=="")
            {
              this.toastr.error("Enter Section Code.", 'close');
              this.submitted=false;
            }
            else if(this.MasterForm.value.EDivisionID=="0")
            {
              this.toastr.error("Select Division.", 'close');
              this.submitted=false;
            }
            else if(this.MasterForm.value.EIsPluckingID=="0")
            {
                this.toastr.error("Select Plucking Section.", 'close');
                this.submitted=false;
            }
            else if(this.MasterForm.value.EArea=="0")
              {
                  this.toastr.error("Enter Area.", 'close');
                  this.submitted=false;
              }
            if(this.submitted)
            {
              var obj={
                ID:this.EMasterForm.value.EID,
                NAME:this.EMasterForm.value.EName,
                CODE:this.EMasterForm.value.ECode,
            
                DivisionID:this.EMasterForm.value.EDivisionID,
                CostCentreType:this.EMasterForm.value.ECostCentreType,

                IsPluckingID:this.EMasterForm.value.EIsPluckingID,
                IsIrrigationID:this.EMasterForm.value.EIsIrrigationID,

                Area:this.EMasterForm.value.EArea,
                PrunetypeID:this.EMasterForm.value.EPrunetypeID
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
              this.service.GetData();
              //this.Loadlist();
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
