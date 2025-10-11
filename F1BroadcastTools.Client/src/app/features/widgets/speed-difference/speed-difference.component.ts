import { Component, computed, OnInit, signal } from '@angular/core';
import { WidgetBaseComponent } from '../widget-base.component';
import { SpeedDifference } from '../../../shared/models/speed-difference.model';
import { WebSocketService } from '../../../core/services/websocket.service';
import { DriverStateService } from '../../../shared/services/states/driver-state.service';
import { DriverBasicDetails } from '../../../shared/models/driver.model';

@Component({
    selector: 'speed-difference',
    imports: [],
    templateUrl: './speed-difference.component.html',
    styleUrl: './speed-difference.component.css',
    providers: [WebSocketService]
})
export class SpeedDifferenceComponent extends WidgetBaseComponent<SpeedDifference> implements OnInit {
    state = signal<SpeedDifference | null>(null);

    readonly spectatedDriver = computed<DriverBasicDetails | null>(() => {
        const drivers = this.state();
        if (drivers)
            return this.driverStateService.driversSignal()[drivers.spectatedVehicleIdx]
        return null;
    });

    readonly followingDriver = computed<DriverBasicDetails | null>(() => {
        const drivers = this.state();
        if (drivers)
            return this.driverStateService.driversSignal()[drivers.followingVehicleIdx]
        return null;
    });

    readonly speedColor = computed<string>(() => {
        const drivers = this.state();
        if (!drivers || drivers.followingSpeedDifference == 0)
            return ""
        return drivers.followingSpeedDifference < 0 ? "slower" : "faster";
    });

    readonly speedDifference = computed<string | number>(() => {
        const driver = this.state();
        if (!driver)
            return "-";
        const speedDiff = driver.followingSpeedDifference;
        return speedDiff > 0 ? `+${speedDiff}` : speedDiff;
    })

    readonly spectatedTeamGradient = computed<string>(() => 
        `linear-gradient(90deg, ${this.spectatedDriver()?.teamDetails?.primaryColor ?? '#000000'}77 0%, #00000000 100%)`
    );

    readonly followingTeamGradient = computed<string>(() => 
        `linear-gradient(90deg, ${this.followingDriver()?.teamDetails?.primaryColor ?? '#000000'}77 0%, #00000000 100%)`
    );

    constructor(private webSocketService: WebSocketService<SpeedDifference>,
        private driverStateService: DriverStateService) { super(); }

    ngOnInit(): void {
        this.webSocketService.connect('ws://localhost:5000/ws/speed-difference');

        this.webSocketService.onMessage().subscribe((data: SpeedDifference) => {
            this.setState(data);
        });
    }

    protected override setState(data: SpeedDifference): void {
        this.state.set(data);
    }
}
