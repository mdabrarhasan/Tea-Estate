import { Component } from '@angular/core';
import { BehaviorSubject, debounceTime, delay, first, Observable, of, Subject, switchMap, take, tap } from 'rxjs';
import {  PluckingtypemanagementService } from '../../service/pluckingtypemanagement/pluckingtypemanagement.service';
import { CommonService } from '../../service/common/common.service';
import { PaginationService } from '../../service/pagination/pagination.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { paginationsearchsort } from '../../service/common/paginationsearchsort.service';
export interface GridJsModel {
  id: number;
  name: string;
  code: string;
  isactive: number;
  slno: number;
  enroll: number;
  unitid: number;
}
@Component({
  selector: 'app-pluckingtypemanagement',
  templateUrl: './pluckingtypemanagement.component.html',
  styleUrl: './pluckingtypemanagement.component.css'
})

export class PluckingtypemanagementComponent {
    PID:number=0;
    list:any;
    submitted = false;
    gardemlist:any;
    prunetypelist:any;
    isactivelist:any;
    divisionlist:any;
    griddata: any;
    
    searchText:string='';
    tempitems:any;
    items: any[] = [];
    pageOfItems?: Array<any>;
    sortProperty: string = 'id';
    sortOrder = 1;
    loading = false;

     constructor(public pagination:paginationsearchsort,private common:CommonService,public service: PluckingtypemanagementService,private sortService: PaginationService,private modalService: NgbModal,private cs:CommonService,private toastr: ToastrService) {
          this.Loadlist();
      }
    
       Loadlist()
        {
            this.service.GetData().subscribe((data: any[])=>{
                this.items = data;
                this.tempitems = data;               
            })  
        }
      
        ngAfterViewInit(){
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
      
        GetDataByID(id:any)
        {
          
         this.service.GetDataByID(id).subscribe((data: any[])=>{
          this.EMasterForm.controls['EID'].setValue(data[0].id);
          this.EMasterForm.controls['ECode'].setValue(data[0].code);
          this.EMasterForm.controls['EName'].setValue(data[0].name);
          this.PID=data[0].id;
         });
        }
      
        MasterForm: FormGroup=new FormGroup({
          ID:new FormControl('0'),
          Name:new FormControl(''),
          Code:new FormControl(''),
          UNITID:new FormControl(localStorage.getItem('LoginUnit')),
          ENROLL:new FormControl(localStorage.getItem('Enroll'))
        });
      
    
        EMasterForm: FormGroup=new FormGroup({
          EID:new FormControl('0'),
          EName:new FormControl(''),
          ECode:new FormControl('')
        });
    
        Reset()
        {
          this.MasterForm.reset();
        }
    
        get f(): { [key: string]: AbstractControl } {
          return this.MasterForm.controls;
        }
        
        ngOnInit(): void {
         this.GetUnit();
         this.Loadlist();
        }
      
        onSort(column: any) {
          this.griddata = this.sortService.onSort(column, this.griddata)
        }
      
        Create() : void
        {
          this.submitted=true;
          if(this.MasterForm.value.Name=="")
          {
            this.toastr.error("Enter Plucking Name.", 'close');
            this.submitted=false;
          }
          else if(this.MasterForm.value.Code=="")
          {
            this.toastr.error("Enter Plucking Code.", 'close');
            this.submitted=false;
          }
          
          if(this.submitted)
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
        
        Update() : void
        {
          
          this.submitted=true;
          if(this.EMasterForm.value.EName=="")
            {
              this.toastr.error("Enter Plucking Name.", 'close');
              this.submitted=false;
            }
            else if(this.EMasterForm.value.ECode=="")
            {
              this.toastr.error("Enter Plucking Code.", 'close');
              this.submitted=false;
            }
            
            if(this.submitted)
            {
              var obj={
                ID:this.EMasterForm.value.EID,
                NAME:this.EMasterForm.value.EName,
                CODE:this.EMasterForm.value.ECode,
                UNITID:localStorage.getItem('LoginUnit'),
                ENROLL:localStorage.getItem('Enroll')
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
