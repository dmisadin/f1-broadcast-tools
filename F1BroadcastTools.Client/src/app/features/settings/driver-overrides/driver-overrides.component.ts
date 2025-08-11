import { Component, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { Team } from "../../../shared/models/Enumerations";
import { RestService } from "../../../core/services/rest.service";
import { DriverOverrideDto } from "../../../shared/models/dtos";
import { DriverOverrideEndpoints, PlayerEndpoints } from "../../../shared/constants/apiUrls";
import { SharedModule } from "../../../shared/shared.module";

@Component({
    selector: 'driver-overrides',
    templateUrl: 'driver-overrides.component.html',
    styleUrl: 'driver-overrides.component.css',
    imports: [SharedModule]
})
export class DriverOverridesComponent implements OnInit, OnChanges {
    drivers: DriverOverrideDto[]
    /*   = [
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
    ]; */

    constructor(private restService: RestService) { }
    ngOnChanges(changes: SimpleChanges): void {
        console.log(changes)
    }

    ngOnInit(): void {
        this.refresh();
    }

    onPlayerChange(driverId: number, playerId?: number) {
        const driver = this.drivers.find(d => d.id === driverId);
        console.log(driverId, playerId)
        if(driver)
            driver.playerId = playerId;

        console.log(driver)
    }

    refresh() {
        this.restService.get<DriverOverrideDto[]>(DriverOverrideEndpoints.getAll).subscribe(result => {
            this.drivers = result;
        });
    }
    
    update() { // TODO: zapravo napraviti filtriranje promjenjenih drivera prije slanja ili imati .push()
        this.restService.post<DriverOverrideDto[]>(PlayerEndpoints.customEndpoint("update"), this.drivers).subscribe();
    }
}