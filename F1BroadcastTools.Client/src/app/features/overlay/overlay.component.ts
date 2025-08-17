import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { RestService } from '../../core/services/rest.service';
import { NavigationService } from '../../core/services/navigation.service';
import { OverlayDto } from '../../shared/dtos/overlay.dto';
import { OverlayEndpoints } from '../../shared/constants/apiUrls';
import { WidgetType } from '../../shared/dtos/widget.dto';
import { TimingTowerComponent } from "../widgets/timing-tower/timing-tower.component";
import { StopwatchListComponent } from "../widgets/stopwatch-list/stopwatch-list.component";
import { MinimapComponent } from "../widgets/minimap/minimap.component";
import { HaloHudComponent } from "../widgets/halo-hud/halo-hud.component";

@Component({
    selector: 'overlay',
    imports: [RouterLink, TimingTowerComponent, StopwatchListComponent, MinimapComponent, HaloHudComponent],
    templateUrl: './overlay.component.html',
    styleUrl: './overlay.component.css',
    providers: [NavigationService]
})
export class OverlayComponent implements OnInit {
    overlay?: OverlayDto;
    WidgetType = WidgetType;

    constructor(private restService: RestService,
                private navigationService: NavigationService) { }

    ngOnInit(): void {
        const overlayId = parseInt(this.navigationService.getId());

        this.restService.get<OverlayDto>(OverlayEndpoints.get(overlayId))
            .subscribe(result => {this.overlay = result; console.log(result)});
    }

}
