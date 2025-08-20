import { Component, OnInit } from "@angular/core";
import { RestService } from "../../../core/services/rest.service";
import { DriverOverrideDto } from "../../../shared/models/dtos";
import { DriverOverrideEndpoints } from "../../../shared/constants/apiUrls";
import { PlayerSearchComponent } from "../../../shared/components/player-search/player-search.component";
import { TeamLogoComponent } from "../../../shared/components/game/team-logo/team-logo.component";

@Component({
    selector: 'driver-overrides',
    templateUrl: 'driver-overrides.component.html',
    styleUrl: 'driver-overrides.component.css',
    imports: [PlayerSearchComponent, TeamLogoComponent]
})
export class DriverOverridesComponent implements OnInit {
    drivers: DriverOverrideDto[]

    constructor(private restService: RestService) { }

    ngOnInit(): void {
        this.refresh();
    }

    onPlayerChange(driverId: number, playerId?: number) {
        const driver = this.drivers.find(d => d.id === driverId);
        if(driver)
            driver.playerId = playerId;
    }

    refresh() {
        this.restService.get<DriverOverrideDto[]>(DriverOverrideEndpoints.getAll).subscribe(result => {
            this.drivers = result;
        });
    }
    
    update() {
        this.restService.post<DriverOverrideDto[]>(DriverOverrideEndpoints.update, this.drivers).subscribe();
    }
}