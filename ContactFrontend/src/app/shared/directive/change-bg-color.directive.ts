import { Directive, ElementRef, HostListener, Input, Renderer2 } from "@angular/core";

@Directive({
    selector: '[appChbgcolor]'
})
export class ChangeBgColorDirective {

    @Input() appChbgcolor = ''
    constructor(private el: ElementRef, private renderer: Renderer2) {

    }

    @HostListener('mouseenter') onMouseEnter() {
        this.ChangeBgColor(this.appChbgcolor);
    }

    @HostListener('mouseleave') onMouseLeave() {
        this.ChangeBgColor('');
    }

    ChangeBgColor(color: string) {
        this.renderer.setStyle(this.el.nativeElement, 'color', color);
    }
}