import { Component, computed, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { GameYear } from '../../../shared/models/Enumerations';
import { RestService } from '../../../core/services/rest.service';
import { DriverStateService } from '../../../shared/services/states/driver-state.service';
import { NgSelectComponent } from '@ng-select/ng-select';
import { CommonModule } from '@angular/common';
import { LookupDto } from '../../../shared/models/common';

@Component({
	selector: 'tyre-stint-comparison-form',
	imports: [CommonModule, ReactiveFormsModule, NgSelectComponent],
	templateUrl: './tyre-stint-comparison-form.component.html',
	styleUrl: './tyre-stint-comparison-form.component.css'
})
export class TyreStintComparisonFormComponent implements OnInit {
	form: FormGroup;
	isLoading = signal(false);
	drivers = computed(() => Object.entries(this.driverState.driversSignal()).map(([k, v]) => ({
		id: k,
		label: v.name,
		teamId: v.teamId,
		gameYear: v.teamDetails?.gameYear
	})));
	GameYear = GameYear;

	constructor (private restService: RestService,
		private formBuilder: FormBuilder,
		private driverState: DriverStateService) 
	{ }

	ngOnInit(): void {
		this.form = this.formBuilder.group({
			selectedVehicles: new FormControl<LookupDto[]>([], { nonNullable: true })
		});

		this.isLoading.set(true);
		this.restService.get<LookupDto[]>("/widget-state/get-tyre-stint-comparison-lookup")
			.subscribe({
				next: res => { this.form.setValue({ selectedVehicles: res }) },
				error: () => this.isLoading.set(false),
				complete: () => this.isLoading.set(false)
			});
	}

	onFormSubmit() {
		const selectedVehicles = this.form.value.selectedVehicles.map((lookup: LookupDto) => Number(lookup.id));

		this.isLoading.set(true);
		this.restService.post("/widget-state/update-tyre-stint-comparison", selectedVehicles ).subscribe(() => {
			this.isLoading.set(false);
		});
	}

	onFormClear() {
		this.form.setValue({ selectedVehicles: [] });
	}
}
