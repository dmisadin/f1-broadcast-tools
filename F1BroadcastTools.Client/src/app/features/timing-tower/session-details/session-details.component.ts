import { Component, Input, computed, effect, input, signal } from '@angular/core';
import { SafetyCarStatus } from '../../../shared/models/Enumerations';

@Component({
    standalone: false,
    selector: 'session-details',
    templateUrl: './session-details.component.html',
    styleUrl: './session-details.component.css'
})
export class SessionDetailsComponent {
    @Input() currentLap: number;
    @Input() totalLaps: number;
    @Input() safetyCarStatus: SafetyCarStatus;
    sectorYellowFlags = input<boolean[]>([]);
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