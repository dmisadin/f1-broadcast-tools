import { Component } from '@angular/core';
import { SectorTimingComparisonFormComponent } from "./sector-timing-comparison-form/sector-timing-comparison-form.component";
import { SpeedTrapLeaderboardFormComponent } from "./speed-trap-leaderboard-form/speed-trap-leaderboard-form.component";
import { TyreStintComparisonFormComponent } from "./tyre-stint-comparison-form/tyre-stint-comparison-form.component";

@Component({
    selector: 'dashboard',
    imports: [SectorTimingComparisonFormComponent, SpeedTrapLeaderboardFormComponent, TyreStintComparisonFormComponent],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

}
