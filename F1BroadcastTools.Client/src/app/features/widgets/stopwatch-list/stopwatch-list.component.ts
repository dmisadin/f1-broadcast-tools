import { Component, OnDestroy, OnInit, signal } from "@angular/core";
import { CommonModule } from "@angular/common";
import { StopwatchComponent } from "./stopwatch/stopwatch.component";
import { animate, style, transition, trigger } from "@angular/animations";
import { WebSocketService } from "../../../core/services/websocket.service";
import { Stopwatch, SectorTimeStatus } from "../../../shared/models/stopwatch.model";
import { WidgetBaseComponent } from "../widget-base.component";

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
    ]
})
export class StopwatchListComponent extends WidgetBaseComponent<Stopwatch> implements OnInit, OnDestroy {
    stopwatch = signal<Stopwatch | null>(null);
    isGapToLeaderVisible: boolean = false;
    positionChange = signal<number>(0);
    currentPoleLap: { lapTime: string, driverName: string };
    isLapValid: boolean = true;

    SectorTimeStatus = SectorTimeStatus;

    constructor(private webSocketService: WebSocketService<Stopwatch>) { super(); }

    ngOnInit(): void {
        const placeholder = this.placeholderData();
        if (placeholder) {
            this.stopwatch.set(placeholder);
            return;
        }
        
        this.webSocketService.connect('ws://localhost:5000/ws/stopwatch');

        this.webSocketService.onMessage().subscribe((data: Stopwatch) => {
            this.stopwatch.set(data);
        });
    }

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }
}
