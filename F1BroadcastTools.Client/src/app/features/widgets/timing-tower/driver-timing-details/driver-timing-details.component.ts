import { ChangeDetectionStrategy, Component, computed, effect, input, signal } from "@angular/core";
import { trigger, transition, style, animate } from "@angular/animations";
import { CommonModule } from "@angular/common";
import { TeamLogoComponent } from "../../../../shared/components/game/team-logo/team-logo.component";
import { AdditionalInfo, GameYear, ResultStatus, Team } from "../../../../shared/models/Enumerations";
import { TeamDetails } from "../../../../shared/models/team.model";

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
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class DriverTimingDetailsComponent {
    isSpectated = input(false);
    showAdditionalInfo = input(AdditionalInfo.None);
    gameYear = input(GameYear.F123);

    vehicleIdx       = input.required<number>();
    position         = input.required<number>();
    teamId           = input.required<Team>();
    teamDetails      = input<TeamDetails | undefined>();
    name             = input.required<string>();
    tyreAge          = input.required<number>();
    visualTyreCompound = input.required<string>();
    gapInterval      = input.required<string>();
    resultStatus     = input.required<ResultStatus>();
    penalties        = input.required<number>();
    warnings         = input.required<number>();
    hasFastestLap    = input.required<boolean>();
    isInPits         = input(false);
    numPitStops      = input.required<number>();
    positionsGained  = input.required<number>();

    ResultStatus = ResultStatus;
    AdditionalInfo = AdditionalInfo;

    private previousPosition = signal<number>(0);
    positionChange = signal<number>(0);

    readonly isOutOfSession = computed(() => {
        const status = this.resultStatus();
        return status !== ResultStatus.Active && status !== ResultStatus.Finished;
    });

    readonly positionsGainedAbsolute = computed(() => Math.abs(this.positionsGained()));
    readonly tyreBackgroundUrl = computed(() => `url(images/icons/tyres/${this.visualTyreCompound()}_empty.svg)`);

    private timeoutHandle: any;

    constructor() {
        effect(() => {
            const newPosition = this.position() ?? 0;
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