import { Component, signal } from "@angular/core";
import { FastestQualifyingLap, SectorTimeStatus } from "../../shared/models/stopwatch.model";
import { WidgetBaseComponent } from "./widget-base.component";
import { GameYear } from "../../shared/models/Enumerations";

@Component({ template: '' })
export abstract class StopwatchBaseComponent<TViewModel> extends WidgetBaseComponent<TViewModel> {
    gameYear = signal<GameYear>(GameYear.F123);
    fastestLap = signal<FastestQualifyingLap | null>(null);
    secondFastestLap = signal<FastestQualifyingLap | null>(null);

    isGapToLeaderVisible: boolean = false;
    positionChange = signal<number>(0);
    isLapValid: boolean = true;

    SectorTimeStatus = SectorTimeStatus;

    protected updateFastestLaps(fastestLap: FastestQualifyingLap | null, secondFastestLap?: FastestQualifyingLap | null) {
        const currentFastestLap = this.fastestLap();
        const currentSecondFastestLap = this.secondFastestLap();

        if (fastestLap && fastestLap.lapTime != currentFastestLap?.lapTime)
            this.fastestLap.set(fastestLap);
        if (secondFastestLap && secondFastestLap.lapTime != currentSecondFastestLap?.lapTime)
            this.secondFastestLap.set(secondFastestLap);
    }
}