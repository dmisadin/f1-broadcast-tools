import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { WidgetBaseComponent } from '../widget-base.component';
import { SessionEvent } from '../../../shared/models/session-event.model';
import { WebSocketService } from '../../../core/services/websocket.service';

@Component({
	selector: 'session-events',
	imports: [],
	templateUrl: './session-events.component.html',
	styleUrl: './session-events.component.css',
	providers: [WebSocketService]
})
export class SessionEventsComponent extends WidgetBaseComponent<SessionEvent> implements OnInit, OnDestroy {
	events = signal<SessionEvent[]>([]);

	constructor(private webSocketService: WebSocketService<SessionEvent>) {
		super();
	}

	ngOnInit(): void {
		this.webSocketService.connect('ws://localhost:5000/ws/session-events');

		this.webSocketService.onMessage().subscribe((data: SessionEvent) => {
			console.log(data)
			this.setState(data);
		});
	}

    ngOnDestroy(): void {
        this.webSocketService.disconnect();
    }

	protected override setState(event: SessionEvent): void {
		this.events.update(current => [...current, event]);

		setTimeout(() => this.removeEvent(event.id), 8000);
	}

	private removeEvent(id: number): void {
		this.events.update(current => current.filter(e => e.id !== id));
	}
}
