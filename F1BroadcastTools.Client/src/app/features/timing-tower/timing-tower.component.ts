import { Component, OnInit } from "@angular/core";
import { TimingTower } from "../../shared/models/TimingTower";

import testData from "./test-data/3.json";
import { WebSocketService } from "../../core/services/websocket.service";
import { ResultStatus, SafetyCarStatus } from "../../shared/models/Enumerations";

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
    showAdditionalInfo: number = 0;
    constructor(private webSocketService: WebSocketService<TimingTower>) { }

    ngOnInit(): void {
        this.webSocketService.connect('ws://localhost:5000/ws');

        this.webSocketService.onMessage().subscribe((data: TimingTower) => {
            //console.log("onMessage", data);
            //TODO: we could use the % of lap done by each car, or at least the leader.
            this.timingTower = data;
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }
}