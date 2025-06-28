import { Component, input } from '@angular/core';
import { Game, Team } from '../../../models/Enumerations';
import { F123TeamLogoComponent } from "../f1-23/f1-23-team-logo/f1-23-team-logo.component";

@Component({
    selector: 'team-logo',
    standalone: true,
    imports: [F123TeamLogoComponent],
    templateUrl: './team-logo.component.html',
    styleUrl: './team-logo.component.css'
})
export class TeamLogoComponent {
    team = input<Team>();
    color = input<string>();
    game = input<Game>(Game.F123);

    Game = Game;
}
