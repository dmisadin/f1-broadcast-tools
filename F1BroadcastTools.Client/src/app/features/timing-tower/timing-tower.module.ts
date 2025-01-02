import { NgModule } from "@angular/core";
import { TimingTowerComponent } from "./timing-tower.component";
import { DriverTimingDetailsComponent } from "./driver-timing-details/driver-timing-details.component";
import { CommonModule } from "@angular/common";
import { TimingTowerRoutingModule } from "./timing-tower-routing.module";
import { SharedModule } from "../../shared/shared.module";

@NgModule({
    imports: [
        CommonModule,
        TimingTowerRoutingModule,
        SharedModule
    ],
    declarations: [
        TimingTowerComponent,
        DriverTimingDetailsComponent
    ]
})
export class TimingTowerModule { }