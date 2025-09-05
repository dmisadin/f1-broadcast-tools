import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { OverlayEndpoints } from '../../../shared/constants/apiUrls';
import { OverlayDto } from '../../../shared/dtos/overlay.dto';
import { BaseListComponent } from '../../../shared/components/list/base-list.component';
import { NavigationService } from '../../../core/services/navigation.service';
import { GridComponent } from '../../../shared/components/grid/grid/grid.component';
import { RestService } from '../../../core/services/rest.service';

@Component({
    selector: 'overlay-list',
    imports: [RouterLink, RouterOutlet],
    templateUrl: './overlay-list.component.html',
    styleUrl: './overlay-list.component.css',
    providers: [NavigationService]
})
export class OverlayListComponent extends BaseListComponent implements OnInit{
    protected override pageTitle: string = "Overlays";
    overlays: OverlayDto[] = [];

    constructor(private restService: RestService) {
        super();
    }

    ngOnInit(): void {
        this.restService.get<OverlayDto[]>(OverlayEndpoints.customEndpoint("get-all"))
                        .subscribe(result => this.overlays = result);
    }

    onRowClick(id: number) {
        this.navigationService.navigateToRelative(id.toString());
    }
}
