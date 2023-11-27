import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appSoloLetras]',
  standalone: true
})
export class SoloLetrasDirective {

  constructor(private el: ElementRef) { }

  @HostListener('input', ['$event']) onInputChange(event) {
    const initalValue = this.el.nativeElement.value;
    this.el.nativeElement.value = initalValue.replace(/[^a-zA-Z]*/g, '');
    if ( initalValue !== this.el.nativeElement.value) {
      event.stopPropagation();
    }
  }

}
