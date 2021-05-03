import { Component, OnInit } from '@angular/core';
import { SendService } from '../Services/send.service';
import { FormBuilder } from '@angular/forms';
import{Mail} from 'src/app/models/Mail';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.css']
})
export class SendEmailComponent implements OnInit {
  mailForm= this.fb.group({
    message:[''],
    mailAddress:[''],
    password:[''],
    subject:[''],
    name:[''],
    });
    emailDialog:boolean;
  constructor(private PS: SendService, private fb:FormBuilder) { }

  ngOnInit(): void {
    this.emailDialog=false;
  }
  openDialog(){
    this.emailDialog=true;
  }
send()
{
  let mail=new Mail();
  let str = this.mailForm.controls;
  mail.name=str.name.value;
  mail.mailAddress=str.mailAddress.value;
  mail.message=str.message.value;
  mail.password=str.password.value;
  mail.subject=str.subject.value;
  debugger;
  return this.PS.sendEmail(mail)
  
  .subscribe((result)=> {
    debugger;
    if(result==true)
    {

    }
  });
  
}
}
