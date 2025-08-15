import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { RestService } from '../../core/services/rest.service';
import { NavigationService } from '../../core/services/navigation.service';
import { OverlayDto } from '../../shared/dtos/overlay.dto';
import { OverlayEndpoints } from '../../shared/constants/apiUrls';

@Component({
    selector: 'overlay',
    imports: [RouterLink],
    templateUrl: './overlay.component.html',
    styleUrl: './overlay.component.css',
    providers: [NavigationService]
})
export class OverlayComponent implements OnInit {
    overlay: OverlayDto;

    constructor(private restService: RestService,
                private navigationService: NavigationService) { }

    ngOnInit(): void {
        const overlayId = parseInt(this.navigationService.getId());

        this.restService.get<OverlayDto>(OverlayEndpoints.get(overlayId))
            .subscribe(result => this.overlay = result);
    }

}
