import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Injectable } from '@angular/core';
import{Companies} from 'src/app/models/Companies';
import { HttpServerService } from '../http-server.service';

@Injectable({
  providedIn: 'root'
})
export class CompaniesService {
 

  constructor(private http:HttpServerService) { }
  AddCompany(company:Companies){
   return  this.http.post('Company/Add',company);
   }
   getCheckCompanyRegistered(name:string,password:string){
     try{
      localStorage.setItem('userName',name );
     }catch(e){
       console.error('Eror in saving to local storage',e);
     }
    return this.http.get('Company/checkDetails/' + name + '/' + password);
   }
  
   
   
}
