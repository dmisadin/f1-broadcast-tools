import { ChangeDetectionStrategy, Component, input } from '@angular/core';

@Component({
    selector: 'minimap-car',
    imports: [],
    templateUrl: './minimap-car.component.html',
    styleUrl: './minimap-car.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class MinimapCarComponent {
    positionX = input.required<number>();
    positionY = input.required<number>();
    teamColor = input.required<string>();
}
