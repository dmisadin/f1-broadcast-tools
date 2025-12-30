import { Component, inject, OnInit, signal } from '@angular/core';
import { WidgetBaseComponent } from '../widget-base.component';
import { SpeedTrapLeaderboard } from '../../../shared/models/speed-trap-leaderboard.model';
import { RestService } from '../../../core/services/rest.service';
import { TeamLogoComponent } from "../../../shared/components/game/team-logo/team-logo.component";

@Component({
	selector: 'speed-trap-leaderboard',
	imports: [TeamLogoComponent],
	templateUrl: './speed-trap-leaderboard.component.html',
	styleUrl: './speed-trap-leaderboard.component.css'
})
export class SpeedTrapLeaderboardComponent extends WidgetBaseComponent<SpeedTrapLeaderboard[]> implements OnInit {
	private restService = inject(RestService);

	cars = signal<SpeedTrapLeaderboard[]>([])

	ngOnInit(): void {
		this.restService.get<SpeedTrapLeaderboard[] | null>("/static-widget/get-speed-trap-leaderboard").subscribe(res => {
			this.setState(res ?? []);
		});
	}

	protected override setState(data: SpeedTrapLeaderboard[]): void {
		this.cars.set(data);
	}
}
