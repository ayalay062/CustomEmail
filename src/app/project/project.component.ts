// import { Component, OnInit } from '@angular/core';

// @Component({
//   selector: 'app-project',
//   templateUrl: './project.component.html',
//   styleUrls: ['./project.component.css']
// })
// export class ProjectComponent implements OnInit {

//   constructor() { }

//   ngOnInit(): void {
//   }

// }
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { CompaniesService } from '../Services/companies.service';
import { ProjectserviceService } from '../Services/project.service';
@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {
  tableList:string;
  projectsname:string;
  columList:string;
  projectForm= this.fb.group({
  projectName:[''],
  table:[''] ,
  columName:[''] 
  });
  disable:boolean;
  companycode:string;
  projectCode:string;
  ppp:string;
  constructor(private PS: ProjectserviceService ,private fb: FormBuilder) {}
  ngOnInit(): void {
    this.disable=false;
    this.companycode =localStorage.getItem('companycode');
   }
   projectName(){
    return this.PS.projectName(this.companycode)
   .subscribe((result)=> {
    this.projectsname=result;
    this.disable=true;
    });
   };
   getTablesName(){
     debugger;
    return this.PS.getTablesName(this.companycode,this.projectForm.controls.projectName.value.projectname )
    .subscribe((result)=> {
      debugger;
      this.tableList=result.tablelistjo;
      localStorage.setItem('projectCode',result.projectCode);
      this.disable=true;
      this.projectCode= localStorage.getItem('projectCode');
      alert(this.projectCode);
      });
   };
  
   getColum(){
     
     debugger;
    return this.PS.getColum(this.projectCode,this.projectForm.controls.table.value)
    .subscribe((result)=> {
      debugger;
      this.columList=result;
      
      });
   };
}
