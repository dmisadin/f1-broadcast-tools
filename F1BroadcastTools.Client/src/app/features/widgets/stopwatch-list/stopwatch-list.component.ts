import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, signal } from "@angular/core";
import { CommonModule } from "@angular/common";
import { StopwatchComponent } from "./stopwatch/stopwatch.component";
import { WebSocketService } from "../../../core/services/websocket.service";
import { StopwatchList, SectorTimeStatus, FastestQualifyingLap, StopwatchCar } from "../../../shared/models/stopwatch.model";
import { WidgetBaseComponent } from "../widget-base.component";
import { GameYear } from "../../../shared/models/Enumerations";

@Component({
    selector: 'stopwatch-list',
    templateUrl: 'stopwatch-list.component.html',
    styleUrl: 'stopwatch-list.component.css',
    imports: [CommonModule, StopwatchComponent],
    providers: [WebSocketService],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class StopwatchListComponent extends WidgetBaseComponent<StopwatchList> implements OnInit, OnDestroy {
    gameYear = signal<GameYear>(GameYear.F123);
    fastestLap = signal<FastestQualifyingLap | null>(null);
    secondFastestLap = signal<FastestQualifyingLap | null>(null);

    cars = signal<StopwatchCar[]>([]);

    isGapToLeaderVisible: boolean = false;
    positionChange = signal<number>(0);
    currentPoleLap: { lapTime: string, driverName: string };
    isLapValid: boolean = true;

    SectorTimeStatus = SectorTimeStatus;

    constructor(private webSocketService: WebSocketService<StopwatchList>) { super(); }

    ngOnInit(): void {
        const placeholder = this.placeholderData();
        if (placeholder) {
            this.setState(placeholder);
            return;
        }

        this.webSocketService.connect('ws://localhost:5000/ws/stopwatch');

        this.webSocketService.onMessage().subscribe((data: StopwatchList) => {
            this.setState(data);
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }

    protected override setState(data: StopwatchList): void {
        this.gameYear.set(data.gameYear);
        this.cars.set(data.cars);
        this.updateFastestLaps(data.fastestLap, data.secondFastestLap);
    }

    private updateFastestLaps(fastestLap: FastestQualifyingLap | null, secondFastestLap?: FastestQualifyingLap | null) {
        const currentFastestLap = this.fastestLap();
        const currentSecondFastestLap = this.secondFastestLap();

        if (fastestLap && fastestLap.lapTime != currentFastestLap?.lapTime)
            this.fastestLap.set(fastestLap);
        if (secondFastestLap && secondFastestLap.lapTime != currentSecondFastestLap?.lapTime)
            this.secondFastestLap.set(secondFastestLap);
    }
}
