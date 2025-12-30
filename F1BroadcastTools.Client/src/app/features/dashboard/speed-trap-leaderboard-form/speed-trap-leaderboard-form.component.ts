import { Component, computed, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { GameYear } from '../../../shared/models/Enumerations';
import { RestService } from '../../../core/services/rest.service';
import { DriverStateService } from '../../../shared/services/states/driver-state.service';
import { SpeedTrapLeaderboardModel } from '../../../shared/models/speed-trap-leaderboard.model';
import { NgSelectComponent } from '@ng-select/ng-select';
import { CommonModule } from '@angular/common';
import { LookupDto } from '../../../shared/models/common';

@Component({
	selector: 'speed-trap-leaderboard-form',
	imports: [CommonModule, ReactiveFormsModule, NgSelectComponent],
	templateUrl: './speed-trap-leaderboard-form.component.html',
	styleUrl: './speed-trap-leaderboard-form.component.css'
})
export class SpeedTrapLeaderboardFormComponent implements OnInit {
	form: FormGroup;
	isLoading = signal(false);
	drivers = computed(() => Object.entries(this.driverState.driversSignal()).map(([k, v]) => ({
		id: k,
		label: v.name,
		teamId: v.teamId,
		gameYear: v.teamDetails?.gameYear
	})));
	GameYear = GameYear;
	constructor(
		private restService: RestService,
		private formBuilder: FormBuilder,
		private driverState: DriverStateService
	) {	}

	ngOnInit(): void {
		this.form = this.formBuilder.group({
			selectedVehicles: new FormControl<LookupDto[]>([], { nonNullable: true })
		});

		this.isLoading.set(true);
		this.restService.get<SpeedTrapLeaderboardModel | null>("/widget-state/get-speed-trap-leaderboard-model")
			.subscribe({
				next: res => {
					if (!res?.selectedVehicles) return;

					const selectedLookups = res.selectedVehicles.map(idx => (
						{
							id: idx,
							label: this.drivers().find(d => Number(d.id) == idx)?.label || ""
						}));

					this.form.setValue({ selectedVehicles: selectedLookups });
				},
				error: () => this.isLoading.set(false),
				complete: () => this.isLoading.set(false)
			});
	}

	onFormSubmit() {
		const selectedVehicles = this.form.value.selectedVehicles.map((lookup: LookupDto) => Number(lookup.id));

		this.isLoading.set(true);
		this.restService.post("/widget-state/update-speed-trap-leaderboard", { selectedVehicles: selectedVehicles }).subscribe(() => {
			this.isLoading.set(false);
		});
	}

	onFormClear() {
		this.form.setValue({ selectedVehicles: [] });
	}
}
