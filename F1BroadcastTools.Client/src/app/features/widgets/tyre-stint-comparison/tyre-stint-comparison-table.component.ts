import { Component, computed, inject, signal } from '@angular/core';
import { RestService } from '../../../core/services/rest.service';
import { GameYear } from '../../../shared/models/Enumerations';
import { WidgetBaseComponent } from '../widget-base.component';
import { TyreStintComparison } from '../../../shared/models/tyre-stint-comparison.model';
import test from "./test2.json"

@Component({
	selector: 'tyre-stint-comparison',
	imports: [],
	templateUrl: './tyre-stint-comparison-table.component.html',
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
	GameYear = GameYear;

	ngOnInit(): void {
				this.setState(test); // TODO: PREBACI SVE U GRID BRATE
		/* this.restService.get<TyreStintComparison>("/static-widget/get-tyre-stint-comparison")
			.subscribe(res => 
			{
				this.setState(res);
			});  */ 
	}

	protected override setState(data: TyreStintComparison): void {
		this.state.set(data);
	}
}
