import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TabStructure } from '../../../shared/models/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { SharedFormService } from '../../../shared/services/shared-form.service';

@Component({
    selector: 'widget-details-form',
    imports: [ReactiveFormsModule, RouterOutlet, RouterLink, RouterLinkActive],
    templateUrl: './widget-details-form.component.html',
    styleUrl: './widget-details-form.component.css'
})
export class WidgetDetailsFormComponent {
    tabs: TabStructure[] = [];

    constructor(public sharedFormService: SharedFormService, private fb: FormBuilder) {
        this.sharedFormService.form = this.fb.group({
            general: this.fb.group({
                positionX: [0, [Validators.required]],
                positionY: [0, [Validators.required]],
            }),
            specific: this.fb.group({
                test1: [true, [Validators.required]],
                test2: [false, [Validators.required]],
            }),
        });

        this.tabs = [
            { title: 'General', route: "/general" },
            { title: 'Streaming info', route: "/streaming-info" }
        ];
    }

    submit() {
        console.log(this.sharedFormService.form.value)
    }

    discard() {
        // reload data from backend
    }
}
