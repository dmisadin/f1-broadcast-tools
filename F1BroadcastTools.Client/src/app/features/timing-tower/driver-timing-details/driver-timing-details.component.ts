import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";
import { DriverTimingDetails } from "../../../shared/models/TimingTower";
import { ResultStatus, SafetyCarStatus } from "../../../shared/models/Enumerations";

@Component({
    standalone: false,
    selector: 'driver-timing-details',
    templateUrl: 'driver-timing-details.component.html',
    styleUrl: 'driver-timing-details.component.css'
})
export class DriverTimingDetailsComponent implements OnChanges {
    @Input() driver!: DriverTimingDetails;
    @Input() isSpectated: boolean = false;

    resultStatus = ResultStatus;
    
    ngOnChanges(changes: SimpleChanges): void {
        /* Implement change tracking and trigger 1sec timer to show red/green
            if position is lost or gained
            changes['position'].previousValue;
            changes['position'].currentValue;

            Note: sorting on backend might be an issue in tracking
        */
    }
}