import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { StopwatchCar, StopwatchSpectated } from '../../../shared/models/stopwatch.model';
import { WebSocketService } from '../../../core/services/websocket.service';
import { StopwatchBaseComponent } from '../stopwatch-base.component';
import { StopwatchComponent } from "../stopwatch-list/stopwatch/stopwatch.component";
import { CommonModule } from '@angular/common';

@Component({
    selector: 'stopwatch-spectated',
    imports: [CommonModule, StopwatchComponent],
    templateUrl: './stopwatch-spectated.component.html',
    providers: [WebSocketService]
})
export class StopwatchSpectatedComponent extends StopwatchBaseComponent<StopwatchSpectated> implements OnInit, OnDestroy {
    car = signal<StopwatchCar | null>(null);

    constructor(private webSocketService: WebSocketService<StopwatchSpectated>) { super(); }

    ngOnInit(): void {
        this.webSocketService.connect('ws://localhost:5000/ws/stopwatch-spectated');

        this.webSocketService.onMessage().subscribe(data => this.setState(data));
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }

    protected override setState(data: StopwatchSpectated): void {
        this.gameYear.set(data.gameYear);
        this.car.set(data.car);
        this.updateFastestLaps(data.fastestLap, data.secondFastestLap);
    }
}
