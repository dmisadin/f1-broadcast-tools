import { Component, effect, input, signal } from '@angular/core';
import { FastestQualifyingLap, SectorTimeStatus, StopwatchCar } from '../../../shared/models/stopwatch.model';
import { CommonModule } from '@angular/common';
import { TeamLogoComponent } from '../../../shared/components/game/team-logo/team-logo.component';

@Component({
    selector: 'stopwatch',
    imports: [CommonModule, TeamLogoComponent],
    templateUrl: './stopwatch.component.html',
    styleUrl: './stopwatch.component.css'

})
export class StopwatchComponent {
    car = input<StopwatchCar>({} as StopwatchCar);
    leaderLap = input<FastestQualifyingLap>({} as FastestQualifyingLap);

    showSector1Gap = signal(false);
    showSector2Gap = signal(false);
    showLapGap = signal(false)

    SectorTimeStatus = SectorTimeStatus;

    private lastGapValues = {
        sector1: null as string | null,
        sector2: null as string | null,
        lap: null as string | null
    };

    private gapTimers = {
        sector1: null as ReturnType<typeof setTimeout> | null,
        sector2: null as ReturnType<typeof setTimeout> | null,
        lap: null as ReturnType<typeof setTimeout> | null
    };

    constructor() {
        this.setupGapEffect(
            () => this.car().sector1GapToLeader,
            this.showSector1Gap,
            'sector1'
        );

        this.setupGapEffect(
            () => this.car().sector2GapToLeader,
            this.showSector2Gap,
            'sector2'
        );

        this.setupGapEffect(
            () => this.car().lapGapToLeader,
            this.showLapGap,
            'lap'
        );
    }

    private setupGapEffect(
        getGap: () => string | null | undefined,
        visibilitySignal: ReturnType<typeof signal<boolean>>,
        key: 'sector1' | 'sector2' | 'lap') 
    {
        effect(() => {
            console.log(this.car().driver.name, this.showSector1Gap(), this.showSector2Gap(), this.showLapGap())
            const newGap = getGap();
            if (newGap && newGap !== this.lastGapValues[key]) {
                this.lastGapValues[key] = newGap;
                visibilitySignal.set(true);

                clearTimeout(this.gapTimers[key]!);
                this.gapTimers[key] = setTimeout(() => {
                    visibilitySignal.set(false);
                }, 4000);
            }
        });
    }
}
