import { ChangeDetectionStrategy, Component, computed, OnDestroy, OnInit, signal } from "@angular/core";
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
    providers: [WebSocketService],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class MinimapComponent extends WidgetBaseComponent<Minimap> implements OnInit, OnDestroy {
    trackId = signal<number | null>(null);
    //spectatorCarIdx = signal<number | null>(null);
    rotation = signal<number | null>(null);
    cars = signal<MinimapCar[] | null>(null);

    trackUrl = computed(() => {
        const trackId = this.trackId();
        if (trackId == null) 
            return '';
        return `images/tracks/${trackId}.svg`
    });

    constructor(private webSocketService: WebSocketService<Minimap>) { super(); }

    ngOnInit(): void {
        const placeholder = this.placeholderData();
        if (placeholder) {
            this.setState(placeholder);
            return;
        }

        this.webSocketService.connect('ws://localhost:5000/ws/minimap');

        this.webSocketService.onMessage().subscribe((data: Minimap) => {
            this.setState(data);
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }

    protected override setState(data: Minimap): void {
        this.trackId.set(data.trackId)
        //this.spectatorCarIdx.set(data.spectatorCarIdx)
        this.rotation.set(data.rotation ?? 0);
        this.cars.set(data.cars);
    }
}