import { Component, OnInit } from '@angular/core';
import {CompaniesService} from 'src/app/Services/companies.service';
import{Companies} from 'src/app/models/Companies';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-new-company',
  templateUrl: './new-company.component.html',
  styleUrls: ['./new-company.component.scss']
})
export class NewCompanyComponent implements OnInit {
  //  companyForm:FormGroup;
 companyForm= this.fb.group({
    Name: [''],
    Password:[''], 
    ContractStartDate: [''],
    Logo: [''],
    ServerName: ['']
  });
  
  Company:Companies=new Companies ();
  code:string;
  constructor(private CS:CompaniesService,private fb: FormBuilder) { }
  ngOnInit(): void {
  }

AddCompany(){
  Object.assign(this.Company, this. companyForm.value);
  debugger;
 this.CS.AddCompany(this.Company).subscribe(res=>{
this.code= res;
localStorage.setItem('companycode',this.code);
});
// this.companyForm.controls.Name.setValue(undefined);
Object.keys(this.companyForm.controls).forEach(key => {
  this.companyForm.controls[key].reset();
});


}

}
