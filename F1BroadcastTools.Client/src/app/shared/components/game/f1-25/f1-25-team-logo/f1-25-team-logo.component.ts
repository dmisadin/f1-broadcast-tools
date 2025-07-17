import { Component, input } from '@angular/core';
import { Team } from '../../../../models/Enumerations';
import { MercedesLogoComponent } from "../../shared/mercedes-logo/mercedes-logo.component";
import { FerrariLogoComponent } from "../../shared/ferrari-logo/ferrari-logo.component";
import { RedBullRacingLogoComponent } from "../../shared/red-bull-racing-logo/red-bull-racing-logo.component";
import { WilliamsLogoComponent } from "../../shared/williams-logo/williams-logo.component";
import { AstonMartinLogoComponent } from "../../shared/aston-martin-logo/aston-martin-logo.component";
import { AlpineLogoComponent } from "../../shared/alpine-logo/alpine-logo.component";
import { HaasLogoComponent } from "../../shared/haas-logo/haas-logo.component";
import { MclarenLogoComponent } from "../../shared/mclaren-logo/mclaren-logo.component";
import { RacingBullsLogoComponent } from './racing-bulls-logo/racing-bulls-logo.component';
import { SauberLogoComponent } from './sauber-logo/sauber-logo.component';

@Component({
    selector: 'f1-25-team-logo',
    imports: [MercedesLogoComponent, FerrariLogoComponent, RedBullRacingLogoComponent, WilliamsLogoComponent, AstonMartinLogoComponent,
            AlpineLogoComponent, RacingBullsLogoComponent, HaasLogoComponent, MclarenLogoComponent, SauberLogoComponent],
    templateUrl: './f1-25-team-logo.component.html',
    styleUrl: './f1-25-team-logo.component.css'
})
export class F125TeamLogoComponent {
    team = input<Team>();
    color = input<string>();
    
    Team = Team;
}
