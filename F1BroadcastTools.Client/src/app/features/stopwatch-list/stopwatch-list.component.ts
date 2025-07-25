import { Component, OnDestroy, OnInit, signal } from "@angular/core";
import { WebSocketService } from "../../core/services/websocket.service";
import { SectorTimeStatus, Stopwatch } from "../../shared/models/stopwatch.model";
import { CommonModule } from "@angular/common";
import { StopwatchComponent } from "./stopwatch/stopwatch.component";
import { animate, style, transition, trigger } from "@angular/animations";

@Component({
    selector: 'stopwatch-list',
    templateUrl: 'stopwatch-list.component.html',
    styleUrl: 'stopwatch-list.component.css',
    imports: [CommonModule, StopwatchComponent],
    providers: [WebSocketService],
    animations: [
        trigger('gapFade', [
            transition(':enter', [
                style({ opacity: 0 }),
                animate('300ms ease-out', style({ width: "20rem" }))
            ]), 
            transition(':leave', [
                animate('400ms 4000ms', style({ width: 0 })) // wait 4s, then instant remove
            ])
        ])
    ]
})
export class StopwatchListComponent implements OnInit, OnDestroy {
    stopwatch: Stopwatch;
    isGapToLeaderVisible: boolean = false;
    positionChange = signal<number>(0);
    currentPoleLap: { lapTime: string, driverName: string };
    isLapValid: boolean = true;

    SectorTimeStatus = SectorTimeStatus;

    constructor(private webSocketService: WebSocketService<Stopwatch>) { }

    ngOnInit(): void {
        this.webSocketService.connect('ws://localhost:5000/ws/stopwatch');

        this.webSocketService.onMessage().subscribe((data: Stopwatch) => {
            this.stopwatch = data;
            console.log(data);
        });
    }

    ngOnDestroy(): void {
        //this.webSocketService.disconnect();
    }

    /**
     * Changes that should be tracked:
     * 1. isGapToLeaderVisible -> has the sectorTime got populated in sessionHistory?
     * 2. positionChange -> track new vs old position
     * 3. currentPoleLap -> update only if new position != 1, but not when the pole has
     *                      been taken (we want to keep old pole lap up for comparison)
     * 4. isLapValid -> if it gets invalidated, show that info and then remove it
     */
}