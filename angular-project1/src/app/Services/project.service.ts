// import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root'
// })
// export class ProjectService {

//   constructor() { }
// }
import { Injectable } from '@angular/core';
import { HttpServerService } from '../http-server.service';

@Injectable({
  providedIn: 'root'
})
export class ProjectserviceService {

  constructor(private http:HttpServerService) { }
  // savethedb(serverName:string,projectName:string){
  //   return this.http.get('Project/readDB/' + serverName + '/' + projectName);
  // }
  projectName(code:string){
    debugger;
   return this.http.get('Project/projectName/' + code);
 }
 getTablesName(companycode:string,projectName:string){
   debugger;
  return this.http.get('Project/tablesName/' + companycode+ '/' + projectName );
  }
  getColum(projectCode:string,tableName:string){
    debugger;
    return this.http.get('Project/getColum/' +projectCode + '/' + tableName); 
  }
  // saveColum(projectCode:string,columToSave:string){
  //   debugger;
  //   return this.http.get('Project/saveColum/' +projectCode + '/' +columToSave);
  // }
//   saveSelectedFilter(projectCode:string,nameTableOfFilter:string,columNameListOfFilter:Array<string>){
//  debugger;
//   return this.http.post('Project/saveSelectedFilter/' +projectCode + '/' +nameTableOfFilter ,columNameListOfFilter);
// }
saveSelectedFilter(project:JSON){
  debugger;
 return this.http.post('Project/saveSelectedFilter' ,project);
 
}
 end(projectCode:string,columName:string,tableOfMailAddresss:string,emailColumn:string)
    {
return this.http.get('Project/end/'+projectCode+'/'+columName+'/'+tableOfMailAddresss+'/'+emailColumn);
    }
}

