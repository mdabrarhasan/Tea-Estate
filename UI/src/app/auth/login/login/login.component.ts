import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { LoginService } from '../../../service/login/login.service';
import { Router } from '@angular/router';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  year: number = new Date().getFullYear();
  submitted = false;
  loading=false;
  fieldTextType!: boolean;
  
  constructor(public loginservice:LoginService,private router:Router) { }
  
  loginform:FormGroup=new FormGroup({
    UserID:new FormControl('',[Validators.required]),
    Emppass:new FormControl('',Validators.required)
  });

  get f(): { [key: string]: AbstractControl } {
    return this.loginform.controls;
  }
  
  ngOnInit():void
  {
      this.loginservice.loading=false;
      localStorage.clear();
      this.loginservice.submitted=false;
  }


  onSubmit()
  {
    this.submitted = true;
    if(this.loginform.valid)
      this.loginservice.Login(this.loginform.value)
      this.loading=this.loginservice.loading;
  }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
}
