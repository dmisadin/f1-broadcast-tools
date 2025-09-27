import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, signal } from "@angular/core";
import { CommonModule } from "@angular/common";
import { StopwatchComponent } from "./stopwatch/stopwatch.component";
import { WebSocketService } from "../../../core/services/websocket.service";
import { StopwatchList, StopwatchCar } from "../../../shared/models/stopwatch.model";
import { StopwatchBaseComponent } from "../stopwatch-base.component";

@Component({
    selector: 'stopwatch-list',
    templateUrl: 'stopwatch-list.component.html',
    styleUrl: 'stopwatch-list.component.css',
    imports: [CommonModule, StopwatchComponent],
    providers: [WebSocketService],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class StopwatchListComponent extends StopwatchBaseComponent<StopwatchList> implements OnInit, OnDestroy {
    cars = signal<StopwatchCar[]>([]);

    constructor(private webSocketService: WebSocketService<StopwatchList>) { super(); }

    ngOnInit(): void {
        const placeholder = this.placeholderData();
        if (placeholder) {
            this.setState(placeholder);
            return;
        }

        this.webSocketService.connect('ws://localhost:5000/ws/stopwatch-list');

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
}
