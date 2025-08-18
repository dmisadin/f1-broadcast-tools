import { ChangeDetectionStrategy, Component, computed, effect, input, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TeamLogoComponent } from '../../../../shared/components/game/team-logo/team-logo.component';
import { GameYear } from '../../../../shared/models/Enumerations';
import { FastestQualifyingLap, SectorTimeStatus } from '../../../../shared/models/stopwatch.model';
import { DriverBasicDetails } from '../../../../shared/models/driver.model';

@Component({
    selector: 'stopwatch',
    imports: [CommonModule, TeamLogoComponent],
    templateUrl: './stopwatch.component.html',
    styleUrl: './stopwatch.component.css',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class StopwatchComponent {
    position = input.required<number>();
    lastLapTime = input.required<string>();
    currentTime = input.required<string>();
    isLapValid = input.required<boolean>();
    lapProgress = input.required<number>();
    tyreCompoundVisual = input.required<string>();
    driver = input.required<DriverBasicDetails>();

    sector1TimeStatus = input<SectorTimeStatus | null | undefined>();
    sector2TimeStatus = input<SectorTimeStatus | null | undefined>();
    sector3TimeStatus = input<SectorTimeStatus | null | undefined>();
    lapTimeStatus = input<SectorTimeStatus | null | undefined>();

    sector1GapToLeader = input<string | null | undefined>();
    sector2GapToLeader = input<string | null | undefined>();
    sector3GapToLeader = input<string | null | undefined>();
    lapGapToLeader = input<string | null | undefined>();

    sector1TimeStatusRelativeToPole = input<SectorTimeStatus | null | undefined>();
    sector2TimeStatusRelativeToPole = input<SectorTimeStatus | null | undefined>();
    lapTimeStatusRelativeToPole = input<SectorTimeStatus | null | undefined>();


    gameYear = input<GameYear | undefined>(GameYear.F123);
    fastestLap = input<FastestQualifyingLap | null>();

    secondFastestLap = input<FastestQualifyingLap | null>();

    showSector1Gap = signal(false);
    showSector2Gap = signal(false);
    showSector3Gap = signal(false);
    showLapGap = signal(false)
    positionChange = signal<number>(0);

    sector1Class = computed(() => this.mapStatusClass(this.sector1TimeStatus()));
    sector2Class = computed(() => this.mapStatusClass(this.sector2TimeStatus()));
    lapClass     = computed(() => this.mapStatusClass(this.lapTimeStatus()));

    readonly displayedGap = computed(() => {
        if (this.showLapGap()) return this.lapGapToLeader();
        if (this.showSector2Gap()) return this.sector2GapToLeader();
        if (this.showSector1Gap()) return this.sector1GapToLeader();
        return null;
    });

    readonly displayedStatus = computed<SectorTimeStatus | null | undefined>(() => {
        if (this.showLapGap()) return this.lapTimeStatusRelativeToPole();
        if (this.showSector2Gap()) return this.sector2TimeStatusRelativeToPole();
        if (this.showSector1Gap()) return this.sector1TimeStatusRelativeToPole();
        return null;
    });

    readonly displayedClass = computed(() => {
        switch (this.displayedStatus()) {
            case SectorTimeStatus.FasterThanLeader: return 'green-time';
            case SectorTimeStatus.NoImprovement: return 'yellow-time';
            default: return '';
        }
    });

    private previousPosition = signal<number>(1);
    private timeoutHandle: any;
    SectorTimeStatus = SectorTimeStatus;
    GameYear = GameYear;

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
            () => this.sector1GapToLeader(),
            this.showSector1Gap,
            'sector1'
        );

        this.setupGapEffect(
            () => this.sector2GapToLeader(),
            this.showSector2Gap,
            'sector2'
        );

        this.setupGapEffect(
            () => this.sector3GapToLeader(),
            this.showSector3Gap,
            'sector3'
        );

        this.setupGapEffect(
            () => this.lapGapToLeader(),
            this.showLapGap,
            'lap'
        );

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

    private mapStatusClass = (s: SectorTimeStatus | null | undefined): string => {
        switch (s) {
            case SectorTimeStatus.FasterThanLeader: return 'purple';
            case SectorTimeStatus.PersonalBest: return 'green';
            case SectorTimeStatus.NoImprovement: return 'yellow';
            default: return '';
        }
    };
}
