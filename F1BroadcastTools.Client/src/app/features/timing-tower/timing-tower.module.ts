import { NgModule } from "@angular/core";
import { TimingTowerComponent } from "./timing-tower.component";
import { DriverTimingDetailsComponent } from "./driver-timing-details/driver-timing-details.component";
import { CommonModule } from "@angular/common";
import { TimingTowerRoutingModule } from "./timing-tower-routing.module";
import { SharedModule } from "../../shared/shared.module";
import { SectorsYellowFlagsPipe } from './pipes/sectors-yellow-flags.pipe';

@NgModule({
    imports: [
        CommonModule,
        TimingTowerRoutingModule,
        SharedModule
    ],
    declarations: [
        TimingTowerComponent,
        DriverTimingDetailsComponent,
        SectorsYellowFlagsPipe
    ]
})
export class TimingTowerModule { }