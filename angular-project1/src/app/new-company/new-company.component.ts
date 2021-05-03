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
    LinkToDb: [''],
    
  });
 
  a:string;
  selectedImageToShow : string ;
  url:string;
  Company:Companies=new Companies ();
  resFromDB:any;
  
  
  constructor(private CS:CompaniesService,private fb: FormBuilder) { }

  ngOnInit(): void {
  }
 uploadImage(image: File){
    debugger;
    // const formData = new FormData();
    this.a=(<HTMLInputElement>event.target).value;
    this.url=this.a;
    // formData.append('image', image);
    // image.eve
    // this.a=image.target.value;
    // String s=image.target.value;
return this.CS.uploadImage(this.a).subscribe(re=>{
  debugger;
  this.url=re.path;
  
})
    // return this.http.post('/api/v1/image-upload', formData);
  };
 
AddCompany(){
  Object.assign(this.Company, this. companyForm.value);
  debugger;
 this.CS.AddCompany(this.Company).subscribe(res=>{
this.resFromDB= res;
// this.companyForm.controls.Name.setValue(undefined);
Object.keys(this.companyForm.controls).forEach(key => {
  this.companyForm.controls[key].reset();
});
alert(this.resFromDB);
});
}
// savethedb(){
//   debugger;
//   return this.CS.savethedb(this.tryForm.controls.serverName.value,this.tryForm.controls.projectName.value)
//  .subscribe((result)=> {
//    debugger;
// this.check=result;
// alert(this.check);
//  });
//  };
}
