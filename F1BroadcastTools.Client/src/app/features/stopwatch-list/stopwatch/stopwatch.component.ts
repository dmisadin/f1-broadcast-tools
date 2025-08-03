import { Component, effect, input, OnChanges, signal, SimpleChanges } from '@angular/core';
import { FastestQualifyingLap, SectorTimeStatus, StopwatchCar } from '../../../shared/models/stopwatch.model';
import { CommonModule } from '@angular/common';
import { TeamLogoComponent } from '../../../shared/components/game/team-logo/team-logo.component';
import { GameYear } from '../../../shared/models/Enumerations';

@Component({
    selector: 'stopwatch',
    imports: [CommonModule, TeamLogoComponent],
    templateUrl: './stopwatch.component.html',
    styleUrl: './stopwatch.component.css'

})
export class StopwatchComponent {
    gameYear = input<GameYear>(GameYear.F123);
    car = input<StopwatchCar>({} as StopwatchCar);
    fastestLap = input<FastestQualifyingLap | null>();
    secondFastestLap = input<FastestQualifyingLap | null>();

    showSector1Gap = signal(false);
    showSector2Gap = signal(false);
    showSector3Gap = signal(false);
    showLapGap = signal(false)
    positionChange = signal<number>(0);
    
    private previousPosition = signal<number>(1);
    private timeoutHandle: any;
    SectorTimeStatus = SectorTimeStatus;

    private lastGapValues = {
        sector1: null as string | null,
        sector2: null as string | null,
        sector3: null as string | null,
        lap: null as string | null
    };

    private gapTimers = {
        sector1: null as ReturnType<typeof setTimeout> | null,
        sector2: null as ReturnType<typeof setTimeout> | null,
        sector3: null as ReturnType<typeof setTimeout> | null,
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
            () => this.car().sector3GapToLeader,
            this.showSector3Gap,
            'sector3'
        );

        this.setupGapEffect(
            () => this.car().lapGapToLeader,
            this.showLapGap,
            'lap'
        );

        effect(() => {
            const driver = this.car();
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
    /* 
        ngOnChanges(changes: SimpleChanges): void {
            if (this.car().lapGapToLeader == this.fastestLap()?.lapTime){
                console.log("Car on pole;", this.car().driver.name, this.car().lapGapToLeader, this.fastestLap()?.lapTime);
                console.log("Second place;", this.secondFastestLap()?.driver.name, this.secondFastestLap()?.lapTime, this.fastestLap()?.lapTime)
            }
        }
     */
    private setupGapEffect(
        getGap: () => string | null | undefined,
        visibilitySignal: ReturnType<typeof signal<boolean>>,
        key: 'sector1' | 'sector2' | 'sector3' | 'lap') {
        effect(() => {
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
