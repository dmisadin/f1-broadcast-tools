import { Component, OnInit } from "@angular/core";
import { DriverOverride } from "../../../shared/models/Player";
import { Team } from "../../../shared/models/Enumerations";
import { RestService } from "../../../core/services/rest.service";

@Component({
    standalone: false,
    selector: 'driver-overrides',
    templateUrl: 'driver-overrides.component.html',
    styleUrl: 'driver-overrides.component.css'
})
export class DriverOverrides implements OnInit {
    drivers: DriverOverride[] = [
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
        {
            id: 4,
            name: "Ivan",
            racingNumber: 61,
            position: 3,
            team: Team.RedBullRacing
        },
        {
            id: 6,
            name: "Goran",
            racingNumber: 6,
            team: Team.AstonMartin
        },
    ];

    constructor(private restService: RestService) { }

    ngOnInit(): void {
        // API call for drivers in lobby
    }

    onPlayerSelected(driverId: number, playerId: number) {
        const driver = this.drivers.find(d => d.id === driverId);

        if(driver)
            driver.playerId = playerId;
    }
}