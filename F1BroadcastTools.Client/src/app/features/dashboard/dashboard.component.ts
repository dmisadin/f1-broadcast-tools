import { Component } from '@angular/core';
import { SectorTimingComparisonFormComponent } from "./sector-timing-comparison-form/sector-timing-comparison-form.component";

@Component({
    selector: 'dashboard',
    imports: [SectorTimingComparisonFormComponent],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.css'
})
export class DashboardComponent {

}
