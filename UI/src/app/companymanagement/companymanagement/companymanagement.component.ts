import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { CompanymanagementService } from '../../service/companymanagement/companymanagement.service';
import { PaginationService } from '../../service/pagination/pagination.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-companymanagement',
  templateUrl: './companymanagement.component.html',
  styleUrl: './companymanagement.component.css'
})
export class CompanymanagementComponent {

  unitname:string="";
  address:string="";
  email:string="";
  contactnumber:string="";
  replytoemail:string="";

  contactperson:string="";
  roundoffruleid:number=0;

  facerecognizationruleid:number=0;
  unitareapluckedid:number=0;
  unitforleafareaid:number=0;
  unitforpluckerperareaid:number=0;

  public list:any=[];
  public pluckedlist:any;
  public facrecoglist:any;
  public roundoffrulelist:any;

  breadCrumbItems!: Array<{}>;

  gridjsList$!: Observable<GridJsModel[]>;
  total$: Observable<number>;
  griddata: any;
  
  constructor(private modalService: NgbModal,public service: CompanymanagementService,private sortService: PaginationService) {
    this.gridjsList$ = service.countries$;
    this.total$ = service.total$;
  }


  ngOnInit(): void {
    /**
    * BreadCrumb
    */
    this.GetCompanyInfo();
    this.GetUnitAreaPlucked();
    this.GetFaceRecognization();
    this.GetRoundOffRule();

    this.breadCrumbItems = [
      { label: 'Tables' },
      { label: 'Grid Js', active: true }
    ];

    this.gridjsList$.subscribe((data: any) => {
      this.griddata = Object.assign([], data)
    })
  }

  onSort(column: any) {
    this.griddata = this.sortService.onSort(column, this.griddata)
  }

  GetCompanyInfo()
  {
    this.service.GetCompanyInfo().subscribe((data: any[])=>{
      this.MasterForm.controls['unitname'].setValue(data[0].unitname);
      this.MasterForm.controls['address'].setValue(data[0].address);
      this.MasterForm.controls['email'].setValue(data[0].email);
      this.MasterForm.controls['contactnumber'].setValue(data[0].contactnumber);
      this.MasterForm.controls['replytoemail'].setValue(data[0].replytoemail);
      this.MasterForm.controls['contactperson'].setValue(data[0].contactperson);
      this.MasterForm.controls['unitname'].setValue(data[0].unitname);
      this.MasterForm.controls['unitname'].setValue(data[0].unitname);

      this.MasterForm.controls['facerecognizationruleid'].setValue(data[0].facerecognizationruleid);
      this.MasterForm.controls['roundoffruleid'].setValue(data[0].roundoffruleid);
      this.MasterForm.controls['unitareapluckedid'].setValue(data[0].unitareapluckedid);
      this.MasterForm.controls['unitforleafareaid'].setValue(data[0].unitforleafareaid);
      this.MasterForm.controls['unitforpluckerperareaid'].setValue(data[0].unitforpluckerperareaid);
    })  
  }

  MasterForm: FormGroup=new FormGroup({
    unitname:new FormControl(),
    address:new FormControl(''),
    email:new FormControl(''),
    contactnumber:new FormControl(''),
    replytoemail:new FormControl(''),
    contactperson:new FormControl(),
    roundoffruleid:new FormControl('0'),

    unitareapluckedid:new FormControl('0'),
    unitforleafareaid:new FormControl('0'),
    unitforpluckerperareaid:new FormControl('0'),
    facerecognizationruleid:new FormControl('0')
  });

  GetUnitAreaPlucked()
  {
    this.service.GetUnitAreaPlucked().subscribe((data: any[])=>{
      console.log(data);
      this.pluckedlist = data;
    })  
  }


  GetFaceRecognization()
  {
    this.service.GetFaceRecognization().subscribe((data: any[])=>{
      console.log(data);
      this.facrecoglist = data;
    })  
  }

  GetRoundOffRule()
  {
    this.service.GetRoundOffRule().subscribe((data: any[])=>{
      console.log(data);
      this.roundoffrulelist = data;
    })  
  }

}


export interface GridJsModel {
  id: string;
  name: string;
  email: string;
  position: string;
  company: string;
  country: string;
}
