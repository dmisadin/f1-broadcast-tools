import { Component, OnInit } from "@angular/core";
import { PlayerOverride } from "../../../shared/models/Player";
import { Team } from "../../../shared/models/Enumerations";
import { RestService } from "../../../core/services/rest.service";

@Component({
    standalone: false,
    selector: 'player-overrides',
    templateUrl: 'player-overrides.component.html',
    styleUrl: 'player-overrides.component.css'
})
export class PlayerOverrides implements OnInit {
    drivers: PlayerOverride[] = [
        {
            id: 1,
            name: "Dominik",
            racingNumber: 22,
            position: 1,
            team: Team.Ferrari
        },
        {
            id: 2,
            name: "Mate",
            racingNumber: 52,
            team: Team.Mercedes
        },
    ];

    constructor(private restService: RestService) { }

    ngOnInit(): void {
        // API call for drivers in lobby
    }

}