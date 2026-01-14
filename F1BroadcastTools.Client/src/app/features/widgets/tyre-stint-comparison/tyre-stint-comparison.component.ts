import { Component, computed, inject, signal } from '@angular/core';
import { RestService } from '../../../core/services/rest.service';
import { GameYear } from '../../../shared/models/Enumerations';
import { WidgetBaseComponent } from '../widget-base.component';
import { TyreStintComparison } from '../../../shared/models/tyre-stint-comparison.model';
import { TeamLogoComponent } from "../../../shared/components/game/team-logo/team-logo.component";

@Component({
	selector: 'tyre-stint-comparison',
	imports: [TeamLogoComponent],
	templateUrl: './tyre-stint-comparison.component.html',
	styleUrl: './tyre-stint-comparison.component.css'
})
export class TyreStintComparisonComponent extends WidgetBaseComponent<TyreStintComparison> {
	private restService = inject(RestService);

	state = signal<TyreStintComparison | null>(null);
	doesLapHavePitStop = computed(() => {
		let totalLaps: boolean[] = [];
		let state = this.state();
		if (!state) return totalLaps;

		for (let i = 0; i < state.totalLaps; i++) {
			totalLaps.push(!!state.pitStopLapMarkers.find(lap => lap === i + 1));
		}
		return totalLaps;
	});

	ngOnInit(): void {
		this.restService.get<TyreStintComparison>("/static-widget/get-tyre-stint-comparison")
			.subscribe(res => 
			{
				this.setState(res);
			}); 
	}

	protected override setState(data: TyreStintComparison): void {
		this.state.set(data);
	}
}
