import { Component, Input } from "@angular/core";
import { SafetyCarStatus } from "../../../shared/models/Enumerations";

@Component({
    standalone: false,
    selector: 'session-details',
    templateUrl: './session-details.component.html',
    styleUrl: './session-details.component.css'
})
export class SessionDetailsComponent {
    @Input() currentLap: number;
    @Input() totalLaps: number;
    @Input() safetyCarStatus: SafetyCarStatus;
    @Input() sectorYellowFlags: boolean[];

    SafetyCarStatus = SafetyCarStatus;

}