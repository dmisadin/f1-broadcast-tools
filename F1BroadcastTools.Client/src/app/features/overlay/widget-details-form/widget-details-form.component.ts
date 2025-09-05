import { ChangeDetectionStrategy, Component, effect, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TabStructure } from '../../../shared/models/common';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { SharedFormService } from '../../../shared/services/shared-form.service';
import { RestService } from '../../../core/services/rest.service';
import { WidgetEndpoints } from '../../../shared/constants/apiUrls';
import { WidgetDto } from '../../../shared/dtos/widget.dto';
import { toSignal } from '@angular/core/rxjs-interop';
import { map } from 'rxjs';


@Component({
    selector: 'widget-details-form',
    imports: [ReactiveFormsModule, RouterOutlet, RouterLink, RouterLinkActive],
    templateUrl: './widget-details-form.component.html',
    styleUrl: './widget-details-form.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class WidgetDetailsFormComponent {
    private activatedRoute = inject(ActivatedRoute);
    readonly widgetId = toSignal<number | null>(this.activatedRoute.params.pipe(
        map(params => +params['id'])),
        { initialValue: null }
    );

    tabs: TabStructure[] = [];
    currentWidgetDto: WidgetDto;
    
    constructor(public sharedFormService: SharedFormService,
        private fb: FormBuilder,
        private restService: RestService) 
    {
        this.sharedFormService.form = this.fb.group({
            positionX: [0, [Validators.required]],
            positionY: [0, [Validators.required]]
        });

        this.tabs = [
            { title: 'General', route: "/general" },
            { title: 'Streaming info', route: "/streaming-info" }
        ];

        effect(() => {
            const id = this.widgetId();
            if (id) 
                this.reload(id);
        });

    }

    submit() {
        const widgetDto = {
            ...this.currentWidgetDto,
            ...this.sharedFormService.form.value
        }
        this.restService.post(WidgetEndpoints.update, widgetDto).subscribe();
    }

    discard() {
        // reload data from backend
    }

    reload(widgetId: number) {
        this.restService.get<WidgetDto>(WidgetEndpoints.get(widgetId)).subscribe(result => {
            this.currentWidgetDto = result;
            this.sharedFormService.form.patchValue(result);
        });
    }
}
