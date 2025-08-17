import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, signal } from "@angular/core";
import { CommonModule } from "@angular/common";
import { StopwatchComponent } from "./stopwatch/stopwatch.component";
import { animate, style, transition, trigger } from "@angular/animations";
import { WebSocketService } from "../../../core/services/websocket.service";
import { Stopwatch, SectorTimeStatus, FastestQualifyingLap, StopwatchCar } from "../../../shared/models/stopwatch.model";
import { WidgetBaseComponent } from "../widget-base.component";
import { GameYear } from "../../../shared/models/Enumerations";

@Component({
    selector: 'stopwatch-list',
    templateUrl: 'stopwatch-list.component.html',
    styleUrl: 'stopwatch-list.component.css',
    imports: [CommonModule, StopwatchComponent],
    providers: [WebSocketService],
    animations: [
        trigger('gapFade', [
            transition(':enter', [
                style({ width: "0rem" }),
                animate('300ms ease-out', style({ width: "20rem" }))
            ]),
            transition(':leave', [
                animate('500ms ease-in', style({ width: "0rem" }))
            ])
        ])
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class StopwatchListComponent extends WidgetBaseComponent<Stopwatch> implements OnInit, OnDestroy {
    gameYear = signal<GameYear>(GameYear.F123);
    fastestLap = signal<FastestQualifyingLap | null>(null);
    secondFastestLap = signal<FastestQualifyingLap | null>(null);

    cars = signal<StopwatchCar[]>([]);

    isGapToLeaderVisible: boolean = false;
    positionChange = signal<number>(0);
    currentPoleLap: { lapTime: string, driverName: string };
    isLapValid: boolean = true;

    SectorTimeStatus = SectorTimeStatus;

    constructor(private webSocketService: WebSocketService<Stopwatch>) { super(); }

    ngOnInit(): void {
        const placeholder = this.placeholderData();
        if (placeholder) {
            this.setState(placeholder);
            return;
        }

        this.webSocketService.connect('ws://localhost:5000/ws/stopwatch');

        this.webSocketService.onMessage().subscribe((data: Stopwatch) => {
            this.setState(data);
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }

    protected override setState(data: Stopwatch): void {
        this.gameYear.set(data.gameYear);
        this.fastestLap.set(data.fastestLap ?? null);
        this.secondFastestLap.set(data.secondFastestLap ?? null);
        this.cars.set(data.cars);
    }
}
