import { Component, OnInit } from "@angular/core";
import { TimingTower } from "../../shared/models/TimingTower";

import testData from "./test-data/3.json";

@Component({
    standalone: false,
    selector: 'timing-tower',
    templateUrl: 'timing-tower.component.html',
    styleUrl: 'timing-tower.component.css'
})
export class TimingTowerComponent implements OnInit{
    
    timingTower?: TimingTower = testData;

    ngOnInit(): void {
        console.log(testData);
        console.log(this.timingTower);
    }
}