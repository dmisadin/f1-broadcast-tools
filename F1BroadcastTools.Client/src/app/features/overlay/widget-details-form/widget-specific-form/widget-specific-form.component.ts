import { Component } from '@angular/core';
import { SharedFormService } from '../../../../shared/services/shared-form.service';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'widget-specific-form',
    imports: [ReactiveFormsModule],
    templateUrl: './widget-specific-form.component.html',
    styleUrl: './widget-specific-form.component.css'
})
export class WidgetSpecificFormComponent {

    constructor(public sharedFormService: SharedFormService) {
    }

}
