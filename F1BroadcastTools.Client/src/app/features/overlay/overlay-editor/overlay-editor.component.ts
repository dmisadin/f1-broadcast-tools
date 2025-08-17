import { Component, OnInit, signal } from '@angular/core';
import { CdkDrag, CdkDragEnd } from '@angular/cdk/drag-drop';
import { NudgeDragDirective } from '../../../shared/directives/nudge-drag.directive';
import { DragItemCoordinates } from '../../../shared/models/common';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NavigationService } from '../../../core/services/navigation.service';
import { RestService } from '../../../core/services/rest.service';
import { OverlayEndpoints, WidgetEndpoints } from '../../../shared/constants/apiUrls';
import { OverlayDto } from '../../../shared/dtos/overlay.dto';
import { WidgetDto, WidgetType } from '../../../shared/dtos/widget.dto';
import { TimingTowerComponent } from "../../widgets/timing-tower/timing-tower.component";
import { StopwatchListComponent } from "../../widgets/stopwatch-list/stopwatch-list.component";
import { MinimapComponent } from "../../widgets/minimap/minimap.component";
import { HaloHudComponent } from "../../widgets/halo-hud/halo-hud.component";
import * as timingTowerPlaceholder from "./mock-data/mock-timing-tower.json"
import * as stopwatchPlaceholder from "./mock-data/mock-stopwatch.json"
import * as minimapPlaceholder from "./mock-data/mock-minimap.json"
import * as haloPlaceholder from "./mock-data/mock-halo.json"

// TODO: 
// 1. Use mock data for preview in editor.
// 2. Use wrapper component with ng-content for dragging (switch case WidgetType as well)

@Component({
    selector: 'overlay-editor',
    imports: [CdkDrag, NudgeDragDirective, FormsModule, RouterLink, RouterOutlet, TimingTowerComponent, StopwatchListComponent, MinimapComponent, HaloHudComponent],
    templateUrl: './overlay-editor.component.html',
    styleUrl: './overlay-editor.component.css',
    providers: [NavigationService]
})
export class OverlayEditorComponent implements OnInit {
    overlay = signal<OverlayDto | null>(null);
    WidgetType = WidgetType;
    timingTowerPlaceholder = timingTowerPlaceholder;
    stopwatchPlaceholder = stopwatchPlaceholder;
    minimapPlaceholder = minimapPlaceholder;
    haloPlaceholder = haloPlaceholder;
    
    constructor(private restService: RestService,
        private navigationService: NavigationService) { }

    ngOnInit(): void {
        const overlayId = parseInt(this.navigationService.getId());

        this.restService.get<OverlayDto>(OverlayEndpoints.get(overlayId))
            .subscribe(result => this.overlay.set(result));
    }

    onSubmit() {
        if (!this.overlay())
            return;

        this.restService.post(WidgetEndpoints.updateMany, this.overlay()?.widgets).subscribe({
            next: (ids) => console.log("Updated widgets IDs: ", ids)
        });
    }

    onDragEnded(event: CdkDragEnd) {
        const itemCoordinates: DragItemCoordinates = {
            id: event.source.data,
            position: event.source.getFreeDragPosition()
        };
        console.log("onDragEnded", itemCoordinates.position)
        this.onPositionChange(itemCoordinates);
    }

    onPositionChange(item: DragItemCoordinates) {
        console.log(item.position)
        this.overlay.update(current => {
            if (!current) return current;

            return {
                ...current,
                widgets: current.widgets.map(widget =>
                    widget.id === item.id
                        ? {
                            ...widget,
                            positionX: item.position.x,
                            positionY: item.position.y
                        }
                        : widget
                )
            };
        });
    }

    onDeactivateRoute(event: any) {
        const newWidget = event.newWidget() as WidgetDto;
        if (!newWidget || !this.overlay())
            return;

        this.overlay.update(current => {
            if (!current) return current;

            return {
                ...current,
                widgets: [...current.widgets, newWidget]
            };
        });
    }
}
