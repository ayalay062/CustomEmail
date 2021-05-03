import { Injectable } from '@angular/core';
import { HttpServerService } from '../http-server.service';
import { Filter } from '../models/Filter';
import { FieldFilter } from '../models/FieldFilter';

@Injectable({
  providedIn: 'root'
})
export class FilterService {

  constructor(private http:HttpServerService) { }
  projectName(companycode:string){
    debugger;
return this.http.get('Filter/projectName/' + companycode);
  };
  getTablesName(companycode:string,projectName:string){
    return this.http.get('Filter/tablesName/'+companycode+'/'+projectName );
  };
  getColumnName(projectCode:string,tableName:string){
    debugger;
   return this.http.get('Filter/getColumnName/' +projectCode + '/' + tableName); 
 };
 saveFilter(filter:Filter){
  debugger;
  return this.http.post('Filter/saveFilter' ,filter);
};
saveField(car:{field:FieldFilter,id:number,TableName:string})
{
  return this.http.post('Filter/saveField' ,car);

}
getFilterTypes(projectCode:string,tableName:string,columnName:string){
  debugger;
  return this.http.get('Filter/getFilterTypes/' +projectCode + '/' + tableName+ '/' +columnName);
};
getNames(filterToSend:Filter){
  return this.http.post('Filter/filterToSend' ,filterToSend);
}
getFilter(projectCode:number){
  return this.http.get('Filter/getFilter/' +projectCode);

}
deleteFilter(filterId:number)
{
  return this.http.delete('Filter/deleteFilter/'+filterId);
}
}
