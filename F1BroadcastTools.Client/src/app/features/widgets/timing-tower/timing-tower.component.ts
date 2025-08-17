import { Component, OnDestroy, OnInit, signal } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SessionDetailsComponent } from "./session-details/session-details.component";
import { DriverTimingDetailsComponent } from "./driver-timing-details/driver-timing-details.component";
import { WebSocketService } from "../../../core/services/websocket.service";
import { SafetyCarStatus, ResultStatus, AdditionalInfo } from "../../../shared/models/Enumerations";
import { TimingTower } from "../../../shared/models/TimingTower";
import { WidgetBaseComponent } from "../widget-base.component";

@Component({
    selector: 'timing-tower',
    templateUrl: 'timing-tower.component.html',
    styleUrl: 'timing-tower.component.css',
    imports: [CommonModule, SessionDetailsComponent, DriverTimingDetailsComponent],
    providers: [WebSocketService]
})
export class TimingTowerComponent extends WidgetBaseComponent<TimingTower> implements OnInit, OnDestroy {
    timingTower = signal<TimingTower | null>(null);
    safetyCarStatus = SafetyCarStatus;
    resultStatus = ResultStatus;
    showAdditionalInfo = signal<number>(AdditionalInfo.None);

    constructor(private webSocketService: WebSocketService<TimingTower>) { super(); }

    ngOnInit(): void {
        if (this.placeholderData()) {
            this.timingTower.set(this.placeholderData() ?? null);
            return;
        }

        this.webSocketService.connect('ws://localhost:5000/ws/timing-tower');

        this.webSocketService.onMessage().subscribe((data: TimingTower) => {
            //console.log("onMessage", data);
            //TODO: we could use the % of lap done by each car, or at least the leader.
            this.timingTower.set(data);
            if (data)
                this.showAdditionalInfo.set(this.setAdditionalInfo(data.showAdditionalInfo));
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
