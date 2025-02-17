import { TemplateRef, Directive, Input } from '@angular/core';

@Directive({
    standalone: false,
    selector: '[slot]',
})
export class SlotDirective {
    @Input('slot') fieldName: string = "";
    @Input('slotValue') value: any;

    constructor(public templateRef: TemplateRef<any>) {}
}
