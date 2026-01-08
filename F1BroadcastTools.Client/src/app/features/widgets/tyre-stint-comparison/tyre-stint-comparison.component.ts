import { Component, computed, inject, signal } from '@angular/core';
import { RestService } from '../../../core/services/rest.service';
import { GameYear } from '../../../shared/models/Enumerations';
import { WidgetBaseComponent } from '../widget-base.component';
import { TyreStintComparison } from '../../../shared/models/tyre-stint-comparison.model';

@Component({
	selector: 'tyre-stint-comparison',
	imports: [],
	templateUrl: './tyre-stint-comparison.component.html',
	styleUrl: './tyre-stint-comparison.component.css'
})
export class TyreStintComparisonComponent extends WidgetBaseComponent<TyreStintComparison> {
	private restService = inject(RestService);

	state = signal<TyreStintComparison | null>(null);
	GameYear = GameYear;

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
