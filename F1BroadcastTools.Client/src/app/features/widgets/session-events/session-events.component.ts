import { Component, signal } from '@angular/core';
import { WidgetBaseComponent } from '../widget-base.component';
import { SessionEvent } from '../../../shared/models/session-event.model';
import { WebSocketService } from '../../../core/services/websocket.service';
import { Session } from 'electron';

@Component({
	selector: 'session-events',
	imports: [],
	templateUrl: './session-events.component.html',
	styleUrl: './session-events.component.css',
	providers: [WebSocketService]
})
export class SessionEventsComponent extends WidgetBaseComponent<SessionEvent> {
	
	events = signal<SessionEvent[]>([]);

	protected override setState(data: SessionEvent): void {
		throw new Error('Method not implemented.');
	}

}
