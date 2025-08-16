import { Component, computed, effect, input, signal } from "@angular/core";
import { trigger, transition, style, animate } from "@angular/animations";
import { CommonModule } from "@angular/common";
import { TeamLogoComponent } from "../../../../shared/components/game/team-logo/team-logo.component";
import { AdditionalInfo, GameYear, ResultStatus } from "../../../../shared/models/Enumerations";
import { DriverTimingDetails } from "../../../../shared/models/TimingTower";

@Component({
    selector: 'driver-timing-details',
    templateUrl: 'driver-timing-details.component.html',
    styleUrl: 'driver-timing-details.component.css',
    imports: [CommonModule, TeamLogoComponent],
    animations: [
        trigger('fadeInOut', [
            transition(':enter', [
                style({ opacity: 0 }),
                animate('300ms ease-in', style({ opacity: 1 }))
            ]),
            transition(':leave', [
                animate('300ms ease-out', style({ opacity: 0 }))
            ])
        ])
    ],
})
export class DriverTimingDetailsComponent {
    isSpectated = input(false);
    showAdditionalInfo = input(AdditionalInfo.None);
    driver = input<DriverTimingDetails>({} as DriverTimingDetails);
    gameYear = input(GameYear.F123);

    resultStatus = ResultStatus;
    AdditionalInfo = AdditionalInfo;

    private previousPosition = signal<number>(0);
    positionChange = signal<number>(0);
    readonly isOutOfSession = computed(() => {
        const status = this.driver().resultStatus;
        return status !== this.resultStatus.Active && status !== this.resultStatus.Finished;
    });

    private timeoutHandle: any;

    constructor() {
        effect(() => {
            const driver = this.driver();
            const newPosition = driver?.position ?? 0;
            const prevPosition = this.previousPosition();

            if (newPosition == prevPosition)
                return;

            const delta = prevPosition - newPosition;

            if (delta !== 0) {
                this.positionChange.set(delta);
                clearTimeout(this.timeoutHandle);

                this.timeoutHandle = setTimeout(() => {
                    this.positionChange.set(0);
                }, 3000);
            }
            this.previousPosition.set(newPosition);
        });
    }
}