import { Component } from '@angular/core';
import { SectorTimingComparisonFormComponent } from "./sector-timing-comparison-form/sector-timing-comparison-form.component";
import { SpeedTrapLeaderboardFormComponent } from "./speed-trap-leaderboard-form/speed-trap-leaderboard-form.component";

@Component({
    selector: 'dashboard',
    imports: [SectorTimingComparisonFormComponent, SpeedTrapLeaderboardFormComponent],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

}
