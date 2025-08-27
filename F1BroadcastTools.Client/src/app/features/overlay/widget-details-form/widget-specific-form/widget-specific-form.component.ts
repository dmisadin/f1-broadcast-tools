import { Component } from '@angular/core';
import { SharedFormService } from '../../../../shared/services/shared-form.service';
import { ReactiveFormsModule } from '@angular/forms';
import { SwitchComponent } from "../../../../shared/components/forms/switch/switch.component";

@Component({
    selector: 'widget-specific-form',
    imports: [ReactiveFormsModule, SwitchComponent],
    templateUrl: './widget-specific-form.component.html',
    styleUrl: './widget-specific-form.component.css'
})
export class WidgetSpecificFormComponent {

    constructor(public sharedFormService: SharedFormService) {
    }

}
