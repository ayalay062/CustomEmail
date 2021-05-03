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
}
