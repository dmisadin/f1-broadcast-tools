import { Component, input, OnChanges, SimpleChanges } from '@angular/core';
import { GameYear, Team } from '../../../models/Enumerations';
import { F123TeamLogoComponent } from "../f1-23/f1-23-team-logo/f1-23-team-logo.component";
import { F125TeamLogoComponent } from '../f1-25/f1-25-team-logo/f1-25-team-logo.component';

@Component({
    selector: 'team-logo',
    standalone: true,
    imports: [F123TeamLogoComponent, F125TeamLogoComponent],
    templateUrl: './team-logo.component.html',
    styleUrl: './team-logo.component.css'
})
export class TeamLogoComponent implements OnChanges{
    ngOnChanges(changes: SimpleChanges): void {
        console.log(this.team(), this.game())
    }
    team = input<Team>();
    color = input<string>();
    game = input<GameYear>(GameYear.F123);

    GameYear = GameYear;
    
}
