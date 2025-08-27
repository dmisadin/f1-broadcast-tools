import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { RestService } from '../../core/services/rest.service';
import { NavigationService } from '../../core/services/navigation.service';
import { OverlayDto } from '../../shared/dtos/overlay.dto';
import { OverlayEndpoints } from '../../shared/constants/apiUrls';
import { WidgetType } from '../../shared/dtos/widget.dto';
import { WidgetDetails } from '../../shared/constants/widgets';

@Component({
    selector: 'overlay',
    imports: [RouterLink, RouterOutlet, RouterLinkActive],
    templateUrl: './overlay.component.html',
    styleUrl: './overlay.component.css',
    providers: [NavigationService]
})
export class OverlayComponent implements OnInit {
    overlay?: OverlayDto;
    WidgetType = WidgetType;
    WidgetDetails = WidgetDetails;

    constructor(private restService: RestService,
                private navigationService: NavigationService) { }

    ngOnInit(): void {
        const overlayId = parseInt(this.navigationService.getId());

        this.restService.get<OverlayDto>(OverlayEndpoints.get(overlayId))
            .subscribe(result => { this.overlay = result });
    }

    onRowClick(id: number) {
        this.navigationService.navigateToRelative('widget', id.toString());
    }
}
