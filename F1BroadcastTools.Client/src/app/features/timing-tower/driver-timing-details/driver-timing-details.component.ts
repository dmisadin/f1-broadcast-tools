import { Component, Input, OnChanges, SimpleChanges } from "@angular/core";
import { DriverTimingDetails } from "../../../shared/models/TimingTower";
import { AdditionalInfo, ResultStatus, SafetyCarStatus } from "../../../shared/models/Enumerations";

@Component({
    standalone: false,
    selector: 'driver-timing-details',
    templateUrl: 'driver-timing-details.component.html',
    styleUrl: 'driver-timing-details.component.css'
})
export class DriverTimingDetailsComponent implements OnChanges {
    @Input() driver!: DriverTimingDetails;

    @Input() isSpectated: boolean = false;
    @Input() showAdditionalInfo: number = 0;

    resultStatus = ResultStatus;
    additionalInfo = AdditionalInfo;
    
    ngOnChanges(changes: SimpleChanges): void {
        /* Implement change tracking and trigger 1sec timer to show red/green
            if position is lost or gained
            changes['position'].previousValue;
            changes['position'].currentValue;

            Note: sorting on backend might be an issue in tracking
        */
    }
}