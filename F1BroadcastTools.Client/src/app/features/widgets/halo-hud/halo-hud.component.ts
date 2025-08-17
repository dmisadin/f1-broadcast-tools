import { CommonModule } from "@angular/common";
import { ChangeDetectionStrategy, Component, input, signal } from "@angular/core";
import { WebSocketService } from "../../../core/services/websocket.service";
import { HaloTelemetryDashboard, ERSDeployMode } from "../../../shared/models/telemetry.model";
import { WidgetBaseComponent } from "../widget-base.component";
import { DriverBasicDetails } from "../../../shared/models/driver.model";

@Component({
    selector: 'halo-hud',
    templateUrl: 'halo-hud.component.html',
    styleUrl: 'halo-hud.component.css',
    imports: [CommonModule],
    providers: [WebSocketService],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class HaloHudComponent extends WidgetBaseComponent<HaloTelemetryDashboard> {
    vehicleIdx          = signal<number>(255);
    speed               = signal<number>(0);
    throttle            = signal<number>(0);
    brake               = signal<number>(0);
    gear                = signal<number>(0);
    engineRPM           = signal<number>(4000);
    engineRPMPercentage = signal<number>(0);
    drs                 = signal<boolean>(false);
    position            = signal<number>(1);

    ersDeployMode       = signal<ERSDeployMode>(ERSDeployMode.None);

    turn                = signal<number>(1);
    driver              = signal<DriverBasicDetails | null>(null);
    nextDriver          = signal<DriverBasicDetails | null>(null);

    ErsDeployMode = ERSDeployMode;

    constructor(private webSocketService: WebSocketService<HaloTelemetryDashboard>) { super(); }

    ngOnInit(): void {
        const placeholder = this.placeholderData();
        if (placeholder) {
            this.setState(placeholder);
            return;
        }
        this.webSocketService.connect('ws://localhost:5000/ws/halo-telemetry');
        this.webSocketService.onMessage().subscribe((data: HaloTelemetryDashboard) => {
            this.setState(data);
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }

    protected override setState(data: HaloTelemetryDashboard): void {
        this.vehicleIdx.set(data.vehicleIdx);
        this.speed.set(data.speed);
        this.throttle.set(data.throttle);
        this.brake.set(data.brake);
        this.gear.set(data.gear);
        this.engineRPM.set(data.engineRPM);
        this.engineRPMPercentage.set(data.engineRPMPercentage);
        this.drs.set(data.drs);
        this.position.set(data.position);
        this.ersDeployMode.set(data.ersDeployMode);
        this.turn.set(data.turn);
        this.driver.set(data.driver ?? null);
        this.nextDriver.set(data.nextDriver ?? null);
    }
    // [style.transform]="'translateY(' + ((1-carTelemetry.throttle)*156) +'px)'"
    // style="background-image: url(images/hud/halo/HaloBackground.png);"
}