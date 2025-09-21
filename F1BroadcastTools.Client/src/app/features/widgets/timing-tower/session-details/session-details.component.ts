import { ChangeDetectionStrategy, Component, Input, computed, effect, input, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SectorsYellowFlagsPipe } from '../pipes/sectors-yellow-flags.pipe';
import { SafetyCarStatus } from '../../../../shared/models/Enumerations';

@Component({
    selector: 'session-details',
    templateUrl: './session-details.component.html',
    styleUrl: './session-details.component.css',
    imports: [CommonModule, SectorsYellowFlagsPipe],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SessionDetailsComponent {
    currentLap = input<number>();
    totalLaps = input<number>();
    safetyCarStatus = input<SafetyCarStatus>();
    sectorYellowFlags = input<boolean[]>([]);
    isRaceSession = input<boolean>();
    isSessionFinished = input<boolean>();
    sessionTimeLeft = input<string | null>(null);
    greenFlagVisible = signal(false);
    private previousHadYellow = signal(false);
    hasAnyYellow = computed(() => this.sectorYellowFlags().some(f => f));

    private timeoutHandle: any;

    constructor() {
        effect(() => {
            const current = this.hasAnyYellow();
            const previous = this.previousHadYellow();

            // Detect transition: yellow → green
            if (previous && !current) {
                this.greenFlagVisible.set(true);
                clearTimeout(this.timeoutHandle);
                this.timeoutHandle = setTimeout(() => {
                    this.greenFlagVisible.set(false);
                }, 4000);
            }

            this.previousHadYellow.set(current);
        });
    }

    SafetyCarStatus = SafetyCarStatus;
}
