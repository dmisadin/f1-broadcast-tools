import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { OverlayEndpoints } from '../../../shared/constants/apiUrls';
import { OverlayDto } from '../../../shared/dtos/overlay.dto';
import { BaseListComponent } from '../../../shared/components/list/base-list.component';
import { NavigationService } from '../../../core/services/navigation.service';
import { GridComponent } from '../../../shared/components/grid/grid/grid.component';

@Component({
    selector: 'overlay-list',
    imports: [RouterLink, RouterOutlet, GridComponent],
    templateUrl: './overlay-list.component.html',
    styleUrl: './overlay-list.component.css',
    providers: [NavigationService]
})
export class OverlayListComponent extends BaseListComponent {
    protected override pageTitle: string = "Overlays";

    overlayEndpoints = OverlayEndpoints;

    onRowClick(data: OverlayDto) {
        this.navigationService.navigateToRelative(data.id.toString());
    }
}
