import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})

export class HttpServerService {
  constructor(private httpClientService: HttpClient) { }
 userControllerUrl ='http://localhost:63400/api/';

 httpOptions: { headers: HttpHeaders}; 

get(route: string, parameter: any = null): Observable<any> {
  this.httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }; 
  if (parameter instanceof (Object)) {
    debugger;
    return this.httpClientService.get(this.userControllerUrl + route, parameter);
  }
  return this.httpClientService.get(this.userControllerUrl + route,this.httpOptions);
}

post(route: string, parameter: any = null): Observable<any> {
  debugger;
  this.httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }; 
  if (parameter instanceof (Object)) {
    debugger;
    return this.httpClientService.post(this.userControllerUrl + route, parameter);
  }
  else {
    debugger;
    return this.httpClientService.post(this.userControllerUrl + route, parameter, this.httpOptions);

  }
 
}
  
}
