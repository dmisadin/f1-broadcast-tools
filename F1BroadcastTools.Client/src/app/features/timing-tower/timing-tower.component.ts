import { Component, OnDestroy, OnInit, signal } from "@angular/core";
import { TimingTower } from "../../shared/models/TimingTower"
import { WebSocketService } from "../../core/services/websocket.service";
import { AdditionalInfo, ResultStatus, SafetyCarStatus } from "../../shared/models/Enumerations";

@Component({
    standalone: false,
    selector: 'timing-tower',
    templateUrl: 'timing-tower.component.html',
    styleUrl: 'timing-tower.component.css'
})
export class TimingTowerComponent implements OnInit, OnDestroy {
    timingTower?: TimingTower;
    safetyCarStatus = SafetyCarStatus;
    resultStatus = ResultStatus;
    showAdditionalInfo = signal<number>(AdditionalInfo.None);
    constructor(private webSocketService: WebSocketService<TimingTower>) { }

    ngOnInit(): void {
        this.webSocketService.connect('ws://localhost:5000/ws');

        this.webSocketService.onMessage().subscribe((data: TimingTower) => {
            //console.log("onMessage", data);
            //TODO: we could use the % of lap done by each car, or at least the leader.
            this.timingTower = data;
            this.showAdditionalInfo.set(this.setAdditionalInfo(this.timingTower.showAdditionalInfo));
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
