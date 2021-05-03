import { Directive,ElementRef, HostListener,Renderer2 } from '@angular/core';

@Directive({
  selector: '[appInput]'
})
export class InputDirective {

  constructor(private elem:ElementRef,private rend:Renderer2) { }
//  @HostListener('mouseenter')
//   private mouseenter(){
//    this.rend.setStyle(this.elem.nativeElement,"background-color","red");
//   }
  
//   @HostListener('mouseup')
//   private mouseup(){
//    this.rend.setStyle(this.elem.nativeElement,"border-color","blue");
//   }
//   @HostListener('mouseleave')
//   private mouseleave(){
//    this.rend.setStyle(this.elem.nativeElement,"background-color","white");
//   }
}
