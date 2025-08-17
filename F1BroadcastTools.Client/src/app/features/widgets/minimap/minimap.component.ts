import { Component, OnDestroy, OnInit, signal } from "@angular/core";
import { WebSocketService } from "../../../core/services/websocket.service";
import { Minimap } from "../../../shared/models/Minimap";
import { WidgetBaseComponent } from "../widget-base.component";
import { CommonModule } from "@angular/common";

@Component({
    selector: 'minimap',
    templateUrl: 'minimap.component.html',
    styleUrl: 'minimap.component.css',
    imports: [CommonModule],
    providers: [WebSocketService]
})
export class MinimapComponent extends WidgetBaseComponent<Minimap> implements OnInit, OnDestroy {
    minimap = signal<Minimap | null>(null);

    constructor(private webSocketService: WebSocketService<Minimap>) { super(); }

    ngOnInit(): void {
        const placeholder = this.placeholderData();
        if (placeholder) {
            this.minimap.set(placeholder);
            return;
        }

        this.webSocketService.connect('ws://localhost:5000/ws/minimap');

        this.webSocketService.onMessage().subscribe((data: Minimap) => {
            this.minimap.set(data);
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }
}