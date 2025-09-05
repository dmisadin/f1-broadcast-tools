import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { WebSocketService } from './core/services/websocket.service';
import { DriverStateService } from './shared/services/states/driver-state.service';

@Component({
    selector: 'app-root',
    imports: [RouterOutlet],
    template: '<router-outlet />',
    styleUrl: './app.component.css',
    providers: [WebSocketService, DriverStateService]
})
export class AppComponent {

    constructor(private driverStateService: DriverStateService) { }
}
