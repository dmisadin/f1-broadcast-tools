import { Component, OnDestroy, OnInit } from "@angular/core";
import { Minimap } from "../../shared/models/Minimap";
import { WebSocketService } from "../../core/services/websocket.service";

@Component({
    selector: 'minimap',
    templateUrl: 'minimap.component.html',
    styleUrl: 'minimap.component.css',
    providers: [WebSocketService]
})
export class MinimapComponent implements OnInit, OnDestroy {
    minimap?: Minimap;

    constructor(private webSocketService: WebSocketService<Minimap>) { }

    ngOnInit(): void {
        this.webSocketService.connect('ws://localhost:5000/ws/minimap');

        this.webSocketService.onMessage().subscribe((data: Minimap) => {
            this.minimap = data;
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }
}