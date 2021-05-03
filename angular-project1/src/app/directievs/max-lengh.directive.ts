import { Directive, HostListener, Input, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appMaxLengh]'
})
export class MaxLenghDirective {
  ngOnInit(): void {
    this.lenght=" ";
   } 
   @Input()lenght:string;
  constructor(private elem:ElementRef,private rend:Renderer2) { }
//   @HostListener('keypress')
//   private change(){
//    if(this.lenght.length>=10)
//    {
//    alert("you can enter just 10 digit");
//    this.rend.setProperty(this.elem.nativeElement,"value"," ");
//    }
// }
// @HostListener('mouseleave')
//   private mouseup(){
//   //   if(!this.lenght.includes("1"))
//   //   alert("you must...")
//   //  this.rend.setStyle(this.elem.nativeElement,"border-color","blue");
//   }

}