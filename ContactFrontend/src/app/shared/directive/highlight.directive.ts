import { Directive, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appHighlight]'
})
export class HighlightDirective {

  constructor(private el: ElementRef, private renderer: Renderer2) {
    this.ChangeBgColor('red');
}


ChangeBgColor(color: string) {
    //this.el.nativeElement.style.backgroundColor = 'red';
    this.renderer.setStyle(this.el.nativeElement, 'color', color);
}

}
