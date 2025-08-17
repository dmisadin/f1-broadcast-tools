import { Component, OnDestroy, OnInit, signal } from "@angular/core";
import { WebSocketService } from "../../../core/services/websocket.service";
import { Minimap, MinimapCar } from "../../../shared/models/Minimap";
import { WidgetBaseComponent } from "../widget-base.component";
import { CommonModule } from "@angular/common";
import { MinimapCarComponent } from "./minimap-car/minimap-car.component";

@Component({
    selector: 'minimap',
    templateUrl: 'minimap.component.html',
    styleUrl: 'minimap.component.css',
    imports: [CommonModule, MinimapCarComponent],
    providers: [WebSocketService]
})
export class MinimapComponent extends WidgetBaseComponent<Minimap> implements OnInit, OnDestroy {
    trackId = signal<number | null>(null);
    //spectatorCarIdx = signal<number | null>(null);
    rotation = signal<number | null>(null);
    cars = signal<MinimapCar[] | null>(null);

    constructor(private webSocketService: WebSocketService<Minimap>) { super(); }

    ngOnInit(): void {
        const placeholder = this.placeholderData();
        if (placeholder) {
            this.updateState(placeholder);
            return;
        }

        this.webSocketService.connect('ws://localhost:5000/ws/minimap');

        this.webSocketService.onMessage().subscribe((data: Minimap) => {
            this.updateState(data);
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }

    private updateState(data: Minimap) {
        this.trackId.set(data.trackId)
        //this.spectatorCarIdx.set(data.spectatorCarIdx)
        this.rotation.set(data.rotation ?? 0);
        this.cars.set(data.cars);
    }
}