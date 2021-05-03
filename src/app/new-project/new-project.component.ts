import { Component, OnInit } from '@angular/core';
import { CompaniesService } from '../Services/companies.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-new-project',
  templateUrl: './new-project.component.html',
  styleUrls: ['./new-project.component.css']
})
export class NewProjectComponent implements OnInit {
  tableList:any;
  tryForm= this.fb.group({
  serverName: [''],
  projectName:[''] 
  });
  visible:boolean;
  constructor(private CS:CompaniesService,private fb:FormBuilder) { }

  ngOnInit(): void {
    this.visible=false;
  }
  savethedb(){
    debugger;
    return this.CS.savethedb(this.tryForm.controls.serverName.value,this.tryForm.controls.projectName.value)
   .subscribe((result)=> {
     debugger;
  this.tableList=result;
  this.visible=true;
  // alert(this.check);
   });
   };
}
