import { Component, OnInit } from "@angular/core";
import { TimingTower } from "../../shared/models/TimingTower";

import testData from "./test-data/3.json";
import { WebSocketService } from "../../core/services/websocket.service";
import { AdditionalInfo, ResultStatus, SafetyCarStatus } from "../../shared/models/Enumerations";

@Component({
    standalone: false,
    selector: 'timing-tower',
    templateUrl: 'timing-tower.component.html',
    styleUrl: 'timing-tower.component.css'
})
export class TimingTowerComponent implements OnInit {
    timingTower?: TimingTower;
    safetyCarStatus = SafetyCarStatus;
    resultStatus = ResultStatus;
    showAdditionalInfo: number = 0;
    constructor(private webSocketService: WebSocketService<TimingTower>) { }

    ngOnInit(): void {
        console.log(this.timingTower);

        this.webSocketService.connect('ws://localhost:5000/ws');

        this.webSocketService.onMessage().subscribe((data: TimingTower) => {
            //console.log("onMessage", data);
            //TODO: we could use the % of lap done by each car, or at least the leader.
            this.timingTower = data;
        })
    }

    shouldShowAdditionalInfo() {
        //penalties, numPitStops, positionsGained
        const currentLap = this.timingTower?.currentLap || 1;
        if (currentLap % 2)
            this.showAdditionalInfo = this.showAdditionalInfo | AdditionalInfo.Warnings;
        else 
            this.showAdditionalInfo = this.showAdditionalInfo & ~AdditionalInfo.Warnings;
        if (currentLap % 3)
            this.showAdditionalInfo = this.showAdditionalInfo | AdditionalInfo.Penalites;
        else
            this.showAdditionalInfo = this.showAdditionalInfo & ~AdditionalInfo.Penalites;
        if (currentLap % 5)
            this.showAdditionalInfo = this.showAdditionalInfo | AdditionalInfo.NumPitStops;
        else
            this.showAdditionalInfo = this.showAdditionalInfo & ~AdditionalInfo.NumPitStops;
        if (currentLap === 2 || currentLap % 10)
            this.showAdditionalInfo = this.showAdditionalInfo | AdditionalInfo.PositionsGained;
        else
            this.showAdditionalInfo = this.showAdditionalInfo & ~AdditionalInfo.PositionsGained;
    }
}