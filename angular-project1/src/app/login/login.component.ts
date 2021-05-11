import { Component, DefaultIterableDiffer, OnInit } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { HttpServerService } from '../http-server.service';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CompaniesService } from '../Services/companies.service';
import { Router } from '@angular/router';
import {Message,MessageService} from 'primeng/api';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  
  msg1:Message[];
   str1:string;
   g:number;
   loginForm= this.fb.group({
    Name: [''],
    Password:[''] 
    });  
  userName:string; 
  constructor(private CS:CompaniesService,private fb: FormBuilder,private router: Router) { }
  checkCompanyRegistered(){
   return this.CS.getCheckCompanyRegistered(this.loginForm.controls.Name.value,this.loginForm.controls.Password.value)
  .subscribe((result)=> {
    debugger;
    this.str1=result.text;
    switch (this.str1) {
        case ("the password is corentlly"):
         this.msg1=[{severity:'success', summary:" ", detail:result.text}];
         this.userName =localStorage.getItem('userName');
         localStorage.setItem('companyCode', result.code);
         this.router.navigate(['/home']);
         break;
         case("סיסמה שגויה"):
          this.msg1=[{severity:'error', summary:" ", detail:result.text}];
          break;
          case("worng try again"):
          this.msg1=[ {severity:'warn', summary:" ", detail:result.text},];
          break;
          default:
            this.msg1=[ {severity:'warn', summary:" ", detail:result.text},];
        break;
    }
    // this.userName=undefined;
    Object.keys(this.loginForm.controls).forEach(key => {
    this.loginForm.controls[key].reset();
  });
  });
  } 
  ngOnInit(): void {
    
  }
}



























