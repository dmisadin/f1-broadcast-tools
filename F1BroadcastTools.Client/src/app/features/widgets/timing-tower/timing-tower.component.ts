import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, signal } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SessionDetailsComponent } from "./session-details/session-details.component";
import { DriverTimingDetailsComponent } from "./driver-timing-details/driver-timing-details.component";
import { WebSocketService } from "../../../core/services/websocket.service";
import { SafetyCarStatus, ResultStatus, AdditionalInfo, GameYear } from "../../../shared/models/Enumerations";
import { DriverTimingDetails, TimingTower } from "../../../shared/models/TimingTower";
import { WidgetBaseComponent } from "../widget-base.component";

@Component({
    selector: 'timing-tower',
    templateUrl: 'timing-tower.component.html',
    styleUrl: 'timing-tower.component.css',
    imports: [CommonModule, SessionDetailsComponent, DriverTimingDetailsComponent],
    providers: [WebSocketService],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class TimingTowerComponent extends WidgetBaseComponent<TimingTower> implements OnInit, OnDestroy {
    gameYear            = signal<GameYear>(GameYear.F123);
    currentLap          = signal<number>(1);
    totalLaps           = signal<number>(3);

    safetyCarStatus     = signal<SafetyCarStatus>(SafetyCarStatus.None);
    sectorYellowFlags   = signal<boolean[]>([false, false, false]);
    showAdditionalInfo  = signal<number>(0);

    driverTimingDetails = signal<DriverTimingDetails[]>([]);
    spectatorCarIdx     = signal<number>(255);

    SafetyCarStatus = SafetyCarStatus;
    ResultStatus = ResultStatus;


    setState(data: TimingTower) {
        this.gameYear.set(data.gameYear);
        this.currentLap.set(data.currentLap);
        this.totalLaps.set(data.totalLaps);

        this.safetyCarStatus.set(data.safetyCarStatus);
        this.sectorYellowFlags.set(data.sectorYellowFlags);
        this.showAdditionalInfo.set(this.setAdditionalInfo(data.showAdditionalInfo));

        this.driverTimingDetails.set(data.driverTimingDetails);
        this.spectatorCarIdx.set(data.spectatorCarIdx);
    }
    constructor(private webSocketService: WebSocketService<TimingTower>) { super(); }

    ngOnInit(): void {
        const placeholder = this.placeholderData();
        if (placeholder) {
            this.setState(placeholder);
            return;
        }

        this.webSocketService.connect('ws://localhost:5000/ws/timing-tower');

        this.webSocketService.onMessage().subscribe((data: TimingTower) => {
            //console.log("onMessage", data);
            //TODO: we could use the % of lap done by each car, or at least the leader.
            this.setState(data);
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }

    setAdditionalInfo(additionalInfoFlags: number): AdditionalInfo {
        if (additionalInfoFlags & AdditionalInfo.PositionsGained) return AdditionalInfo.PositionsGained;
        else if (additionalInfoFlags & AdditionalInfo.NumPitStops) return AdditionalInfo.NumPitStops;
        else if (additionalInfoFlags & AdditionalInfo.Penalties) return AdditionalInfo.Penalties;
        else if (additionalInfoFlags & AdditionalInfo.Warnings) return AdditionalInfo.Warnings;

        return AdditionalInfo.None;
    }
}
