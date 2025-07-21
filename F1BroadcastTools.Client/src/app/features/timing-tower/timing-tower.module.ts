import { NgModule } from "@angular/core";
import { TimingTowerComponent } from "./timing-tower.component";
import { DriverTimingDetailsComponent } from "./driver-timing-details/driver-timing-details.component";
import { CommonModule } from "@angular/common";
import { TimingTowerRoutingModule } from "./timing-tower-routing.module";
import { SharedModule } from "../../shared/shared.module";
import { SectorsYellowFlagsPipe } from './pipes/sectors-yellow-flags.pipe';
import { SessionDetailsComponent } from "./session-details/session-details.component";
import { TeamLogoComponent } from "../../shared/components/game/team-logo/team-logo.component";
import { WebSocketService } from "../../core/services/websocket.service";

@NgModule({
    imports: [
        CommonModule,
        TimingTowerRoutingModule,
        SharedModule,
        TeamLogoComponent
    ],
    declarations: [
        TimingTowerComponent,
        DriverTimingDetailsComponent,
        SectorsYellowFlagsPipe,
        SessionDetailsComponent
    ],
    providers: [WebSocketService]
})
export class TimingTowerModule { }