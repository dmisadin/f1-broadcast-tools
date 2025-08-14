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
import { WidgetDto } from '../../../shared/dtos/widget.dto';

@Component({
    selector: 'overlay-editor',
    imports: [CdkDrag, NudgeDragDirective, FormsModule, RouterLink, RouterOutlet],
    templateUrl: './overlay-editor.component.html',
    styleUrl: './overlay-editor.component.css',
    providers: [NavigationService]
})
export class OverlayEditorComponent implements OnInit {
    overlay = signal<OverlayDto | null>(null);

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
        }
        this.onPositionChange(itemCoordinates);
    }

    onPositionChange(item: DragItemCoordinates) {
        this.overlay.update(current => {
            if (!current) return current;

            return {
                ...current,
                widgets: current.widgets.map(widget =>
                    widget.id === item.id
                        ? { ...widget,
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
