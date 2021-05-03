import { Directive, Input, ElementRef, Renderer2, HostListener } from '@angular/core';

@Directive({
  selector: '[appPassword]'
})
export class PasswordDirective {
  ngOnInit(): void {
    this.lenght="";
   } 
   @Input()lenght:string;
  constructor(private elem:ElementRef,private rend:Renderer2) { }
  // @HostListener('keypress')
  // private change(){
  //   debugger;
  //  if(this.lenght.length>=10)
  //  {
  //  alert("you can enter just 10 digit");
  //  this.rend.setProperty(this.elem.nativeElement,"value"," ");
  //  }
  // }
  @HostListener('focus')
  private aa(){
    if(!this.lenght.includes("1"))
    alert("you must...")
   this.rend.setStyle(this.elem.nativeElement,"border-color","red");
  }
  @HostListener('blur')
  private mouseup(){
    if(!this.lenght.includes("1"))
    alert("you must...")
   this.rend.setStyle(this.elem.nativeElement,"border-color","red");
  }
}
