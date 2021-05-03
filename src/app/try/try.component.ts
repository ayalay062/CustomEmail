import { Component, OnInit } from '@angular/core';
import { HttpServerService } from '../http-server.service';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { CompaniesService } from '../Services/companies.service';
@Component({
  selector: 'app-try',
  templateUrl: './try.component.html',
  styleUrls: ['./try.component.scss']
})
export class TryComponent implements OnInit {
  resulte:any;
  username:string;
  // check:any;
  // tryForm= this.fb.group({
  // serverName: [''],
  // projectName:[''] 
  // });
  constructor(private CS:CompaniesService,private fb:FormBuilder) { }
  
  // savethedb(){
  //   debugger;
  //   return this.CS.savethedb(this.tryForm.controls.serverName.value,this.tryForm.controls.projectName.value)
  //  .subscribe((result)=> {
  //    debugger;
  // this.check=result;
  // alert(this.check);
  //  });
  //  };
  ngOnInit(): void {
    this.username=localStorage.getItem('userName');
  }

}

