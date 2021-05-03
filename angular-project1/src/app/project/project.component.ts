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
import{Table} from 'src/app/models/Table';
import{Colum} from 'src/app/models/Colum';
import { HttpParams } from '@angular/common/http';
@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {
  tableList:string[];
  projectsname:string;
  columList:string;
  columNameFilter:string;
  projectForm= this.fb.group({
  projectName:[''],
  tableOfFilter:[''] ,
  tableOfMailAddresss:[''],
  emailColumn:[''],
  columName:[''],
  columsName:['']
  });
  disable:boolean;
  columnListForSave:Array <string>=[];
  columNameList2:Array<Colum> = [];
  companycode:string;
  projectCode:string;
  columName:Array<Colum> = [];
  tableName: Array<Table> = [];
  columNameForFilter:Array<Colum>=[];
  userName:string;
  constructor(private PS: ProjectserviceService ,private fb: FormBuilder) {}
  ngOnInit(): void {
    this.disable=false;
    this.companycode =localStorage.getItem('companyCode');
    this.projectName();
    this.userName=localStorage.getItem('userName');
   }
   projectName(){
    return this.PS.projectName(this.companycode)
   .subscribe((result)=> {
    this.projectsname=result;
    this.disable=true;
    });
   };
   getTablesName(){
    return this.PS.getTablesName(this.companycode,this.projectForm.controls.projectName.value.projectname )
    .subscribe((result)=> {
      this.tableList=result.tableList;
      this.tableName=new Array<Table>( );
      for (let index = 0; index < this.tableList.length; index++) {
        table:Table;
        let table=new Table();
        table.Name=this.tableList[index];
        this.tableName.push(table);
        }
      localStorage.setItem('projectCode',result.projectCode);
      this.disable=true;
      this.projectCode= localStorage.getItem('projectCode');
     
      });
   };
   getColumForMail(){
    return this.PS.getColum(this.projectCode,this.projectForm.controls.tableOfMailAddresss.value.Name)
    .subscribe((result)=> {
     this.columList=result;
      this.columName=new Array<Colum>();
      for (let index = 0; index < this.columList.length; index++) {
       let colum=new Colum();
       colum.Name=this.columList[index];
       this.columName.push(colum);
      }
      });
   };

   getColum(){
    return this.PS.getColum(this.projectCode,this.projectForm.controls.tableOfFilter.value.Name)
   .subscribe((result)=> {
    this.columNameFilter=result;
     this.columNameForFilter=new Array<Colum>();
     for (let index = 0; index < this.columNameFilter.length; index++) {
      let colum=new Colum();
      colum.Name=this.columNameFilter[index];
      this.columNameForFilter.push(colum);
     }
     });
  };
  // saveColum(){
  //   return this.PS.saveColum(this.projectCode,this.projectForm.controls.columName.value.Name)
  //   .subscribe((result)=> {
  //     alert(result);
  //     });
  //  };
   binSelect(){
    debugger;
  //  this.columNameList2=new Array<Colum>();
  this.columnListForSave=new Array<string>();
   for (let index = 0; index < this.projectForm.controls.columsName.value.length; index++) {
    // let colum=new Colum();
    // colum.Name=this.projectForm.controls.columsName.value[index].Name;
    // this.columNameList2.push(colum);
    this.columnListForSave.push(this.projectForm.controls.columsName.value[index].Name);
   
    }
    
  };
  saveSelectedFilter(){
    let nameTableOfFilter:string;
    nameTableOfFilter=this.projectForm.controls.tableOfFilter.value.Name;
    const projectObj= {
      code: this.projectCode,
      tableNameFilter:nameTableOfFilter,
      columnsForSave: this.projectForm.controls.columsName.value
    };
    debugger;
    const myObjStr = JSON.stringify(projectObj);
    var projectJson=JSON.parse( myObjStr );
    return this.PS.saveSelectedFilter(projectJson)
    .subscribe((result)=> {
      if(result==true){
      this.projectCode,this.projectForm.controls.tableOfFilter.reset();
      this.projectForm.controls.columsName.reset();
      }
      
    });
  }
    end(){
      debugger;
      if(this.projectForm.controls.columName.value.Name &&this.projectForm.controls.tableOfMailAddresss.value.Name )
      {
return this.PS.end(this.projectCode,this.projectForm.controls.columName.value.Name,this.projectForm.controls.tableOfMailAddresss.value.Name,this.projectForm.controls.emailColumn.value.Name)
.subscribe((result)=> {
  alert(result);
  });
}
else{
  alert("you must fiil all the input");
}
}

}

