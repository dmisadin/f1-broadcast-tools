import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { WebSocketService } from "../../../core/services/websocket.service";
import { HaloTelemetryDashboard, ERSDeployMode } from "../../../shared/models/telemetry.model";

@Component({
    selector: 'halo-hud',
    templateUrl: 'halo-hud.component.html',
    styleUrl: 'halo-hud.component.css',
    imports: [CommonModule],
    providers: [WebSocketService],
})
export class HaloHudComponent {
    carTelemetry?: HaloTelemetryDashboard;
    ersDeployMode = ERSDeployMode;
    constructor(private webSocketService: WebSocketService<HaloTelemetryDashboard>) { }

    ngOnInit(): void {
        this.webSocketService.connect('ws://localhost:5000/ws/halo-telemetry');

        this.webSocketService.onMessage().subscribe((data: HaloTelemetryDashboard) => {
            this.carTelemetry = data;
            console.log(data);
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }
    // [style.transform]="'translateY(' + ((1-carTelemetry.throttle)*156) +'px)'"
    // style="background-image: url(images/hud/halo/HaloBackground.png);"
}