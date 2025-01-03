import { Component, OnInit } from "@angular/core";
import { TimingTower } from "../../shared/models/TimingTower";

import testData from "./test-data/3.json";
import { WebSocketService } from "../../core/services/websocket.service";
import { SafetyCarStatus } from "../../shared/models/Enumerations";

@Component({
    standalone: false,
    selector: 'timing-tower',
    templateUrl: 'timing-tower.component.html',
    styleUrl: 'timing-tower.component.css'
})
export class TimingTowerComponent implements OnInit {
    timingTower?: TimingTower;
    safetyCarStatus = SafetyCarStatus;
    constructor(private webSocketService: WebSocketService<TimingTower>) { }

    ngOnInit(): void {
        console.log(this.timingTower);

        this.webSocketService.connect('ws://localhost:5000/ws');

        this.webSocketService.onMessage().subscribe((data: TimingTower) => {
            console.log("onMessage", data);
            this.timingTower = data;
        })
    }
}