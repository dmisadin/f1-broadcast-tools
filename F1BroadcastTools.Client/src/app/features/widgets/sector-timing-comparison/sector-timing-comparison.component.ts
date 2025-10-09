import { Component, computed, OnInit, signal } from '@angular/core';
import { DriverPreviousLapDetails, PreviousLapSectorComparison } from '../../../shared/models/previous-lap-sector-comparison.model';
import { WidgetBaseComponent } from '../widget-base.component';
import { RestService } from '../../../core/services/rest.service';
import { CommonModule } from '@angular/common';
import { TeamLogoComponent } from "../../../shared/components/game/team-logo/team-logo.component";
import { GameYear } from '../../../shared/models/Enumerations';
import { mapSectorTimeStatusClass } from '../../../shared/utlity/timing.utility';

@Component({
    selector: 'previous-lap-sector-comparison',
    imports: [CommonModule, TeamLogoComponent],
    templateUrl: './sector-timing-comparison.component.html',
    styleUrl: './sector-timing-comparison.component.css'
})
export class SectorTimingComparisonComponent
    extends WidgetBaseComponent<PreviousLapSectorComparison> implements OnInit {
    lapNumber = signal<number>(1);
    driverPreviousLapDetails = signal<DriverPreviousLapDetails | null>(null);
    comparingDriverPreviousLapDetails = signal<DriverPreviousLapDetails | null>(null);

    tyreCompoundUrl = computed(() => `/images/icons/tyres/${this.driverPreviousLapDetails()?.visualTyreCompound.toLowerCase()}.svg`);
    comparingTyreCompoundUrl = computed(() => `/images/icons/tyres/${this.comparingDriverPreviousLapDetails()?.visualTyreCompound.toLowerCase()}.svg`);
    gradient = computed(() =>
        `linear-gradient(rgba(0,0,0,0.5), rgba(0,0,0,0.5)), 
        linear-gradient(90deg, ${this.driverPreviousLapDetails()?.driver.teamDetails?.primaryColor ?? '#000000'}ee 0%, 
        #181b29 35%, 
        #181b29 65%, 
        ${this.comparingDriverPreviousLapDetails()?.driver.teamDetails?.primaryColor ?? '#000000'}ee 100%)`
    );
    
    comparingLapStatusClass = computed(() => mapSectorTimeStatusClass(this.comparingDriverPreviousLapDetails()?.lapTiming.lapTimeStatus));
    comparingSector1StatusClass = computed(() => mapSectorTimeStatusClass(this.comparingDriverPreviousLapDetails()?.lapTiming.sector1TimeStatus));
    comparingSector2StatusClass = computed(() => mapSectorTimeStatusClass(this.comparingDriverPreviousLapDetails()?.lapTiming.sector2TimeStatus));
    comparingSector3StatusClass = computed(() => mapSectorTimeStatusClass(this.comparingDriverPreviousLapDetails()?.lapTiming.sector3TimeStatus));

    GameYear = GameYear;

    constructor(private restService: RestService) { super(); }

    ngOnInit(): void {
        this.restService.get<PreviousLapSectorComparison | null>("/static-widget/previous-lap-sector-comparison")
            .subscribe(data => data && this.setState(data));
    }

    protected override setState(data: PreviousLapSectorComparison): void {
        this.lapNumber.set(data.lapNumber);
        this.driverPreviousLapDetails.set(data.driverPreviousLapDetails);
        this.comparingDriverPreviousLapDetails.set(data.comparingDriverPreviousLapDetails);
    }
}
