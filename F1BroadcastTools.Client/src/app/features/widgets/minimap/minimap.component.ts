import { Component, OnDestroy, OnInit } from "@angular/core";
import { WebSocketService } from "../../../core/services/websocket.service";
import { Minimap } from "../../../shared/models/Minimap";
import { WidgetBaseComponent } from "../widget-base.component";

@Component({
    selector: 'minimap',
    templateUrl: 'minimap.component.html',
    styleUrl: 'minimap.component.css',
    providers: [WebSocketService]
})
export class MinimapComponent extends WidgetBaseComponent<Minimap> implements OnInit, OnDestroy {
    minimap?: Minimap;

    constructor(private webSocketService: WebSocketService<Minimap>) { super(); }

    ngOnInit(): void {
        if (this.placeholderData()) {
            this.minimap = this.placeholderData();
            return;
        }

        this.webSocketService.connect('ws://localhost:5000/ws/minimap');

        this.webSocketService.onMessage().subscribe((data: Minimap) => {
            this.minimap = data;
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }
}