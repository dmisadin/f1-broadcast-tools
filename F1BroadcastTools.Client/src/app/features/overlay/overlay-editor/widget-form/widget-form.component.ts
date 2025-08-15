import { Component, OnInit, signal } from '@angular/core';
import { ModalDialogComponent } from "../../../../shared/components/modal-dialog/modal-dialog.component";
import { NgSelectModule } from "@ng-select/ng-select";
import { WidgetDto, WidgetType } from '../../../../shared/dtos/widget.dto';
import { FormsModule } from '@angular/forms';
import { BaseFormComponent } from '../../../../shared/components/forms/base-form.component';
import { NavigationService } from '../../../../core/services/navigation.service';
import { WidgetEndpoints } from '../../../../shared/constants/apiUrls';

@Component({
    selector: 'widget-form',
    imports: [FormsModule, ModalDialogComponent, NgSelectModule],
    templateUrl: './widget-form.component.html',
    styleUrl: './widget-form.component.css',
    providers: [NavigationService]
})
export class WidgetFormComponent extends BaseFormComponent implements OnInit {
    selectedWidget = signal<{ id: WidgetType, label: string } | null>(null);
    currentWidgets = signal<WidgetType[]>([]);
    newWidget = signal<WidgetDto | null>(null);

    widgetOptions = [
        { id: WidgetType.TimingTower, label: 'Timing Tower' },
        { id: WidgetType.Stopwatch, label: 'Stopwatch' },
        { id: WidgetType.Minimap, label: 'Minimap' },
        { id: WidgetType.HaloHUD, label: 'Halo HUD' },
    ]

    override ngOnInit(): void {
        super.ngOnInit();
    }

    onFormDiscard() {
        this.navigationService.navigateToParent(!!this.entityId);
    }

    onFormSubmit() {
        if (!this.selectedWidget()) {
            this.onFormDiscard();
            return;
        }
        const overlayId = this.navigationService.getId(true);

        const widgetDto: WidgetDto = {
            id: 0,
            overlayId: parseInt(overlayId),
            widgetType: this.selectedWidget()?.id ?? WidgetType.Minimap,
            positionX: 0,
            positionY: 0
        }

        this.restService.save(WidgetEndpoints.add, widgetDto).subscribe({
            next: (newId) => {
                this.newWidget.set({ ...widgetDto, id: parseInt(newId) });
                this.navigationService.navigateToParent(!!this.entityId);
            }
        });
    }
}
