import { Component, input } from '@angular/core';
import { Team } from '../../../../models/Enumerations';
import { MercedesLogoComponent } from "../../shared/mercedes-logo/mercedes-logo.component";
import { FerrariLogoComponent } from "../../shared/ferrari-logo/ferrari-logo.component";
import { RedBullRacingLogoComponent } from "../../shared/red-bull-racing-logo/red-bull-racing-logo.component";
import { WilliamsLogoComponent } from "../../shared/williams-logo/williams-logo.component";
import { AstonMartinLogoComponent } from "../../shared/aston-martin-logo/aston-martin-logo.component";
import { AlpineLogoComponent } from "../../shared/alpine-logo/alpine-logo.component";
import { AlphatauriLogoComponent } from "./alphatauri-logo/alphatauri-logo.component";
import { HaasLogoComponent } from "../../shared/haas-logo/haas-logo.component";
import { MclarenLogoComponent } from "../../shared/mclaren-logo/mclaren-logo.component";
import { AlfaRomeoLogoComponent } from "./alfa-romeo-logo/alfa-romeo-logo.component";

@Component({
    selector: 'f1-23-team-logo',
    imports: [MercedesLogoComponent, FerrariLogoComponent, RedBullRacingLogoComponent, WilliamsLogoComponent, AstonMartinLogoComponent,
            AlpineLogoComponent, AlphatauriLogoComponent, HaasLogoComponent, MclarenLogoComponent, AlfaRomeoLogoComponent],
    templateUrl: './f1-23-team-logo.component.html',
    styleUrl: './f1-23-team-logo.component.css'
})
export class F123TeamLogoComponent {
    team = input<Team>();
    color = input<string>();

    Team = Team;
}
