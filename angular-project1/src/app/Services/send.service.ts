import { Injectable } from '@angular/core';
import { HttpServerService } from '../http-server.service';
import { Mail } from '../models/Mail';

@Injectable({
  providedIn: 'root'
})
export class SendService {

  constructor(private http:HttpServerService) { }
 sendEmail(mail:Mail){
   return this.http.post('SendEmail/send' ,mail);
   
  }
}
