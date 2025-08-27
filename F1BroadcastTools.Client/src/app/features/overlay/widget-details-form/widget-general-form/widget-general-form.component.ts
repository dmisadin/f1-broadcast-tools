import { Component, OnInit } from '@angular/core';
import { SharedFormService } from '../../../../shared/services/shared-form.service';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'widget-general-form',
    imports: [ReactiveFormsModule],
    templateUrl: './widget-general-form.component.html',
    styleUrl: './widget-general-form.component.css'
})
export class WidgetGeneralFormComponent implements OnInit {

    constructor(public sharedFormService: SharedFormService) {
    }

    ngOnInit() {
    }
}
