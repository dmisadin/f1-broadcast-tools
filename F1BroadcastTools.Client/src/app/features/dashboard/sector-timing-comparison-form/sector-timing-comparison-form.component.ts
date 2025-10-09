import { Component, computed, OnInit, signal } from '@angular/core';
import { RestService } from '../../../core/services/rest.service';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DriverStateService } from '../../../shared/services/states/driver-state.service';
import { NgSelectModule } from '@ng-select/ng-select';
import { TeamLogoComponent } from "../../../shared/components/game/team-logo/team-logo.component";
import { GameYear } from '../../../shared/models/Enumerations';
import { SectorTimingComparisonModel } from '../../../shared/models/sector-comparison.model';

@Component({
    selector: 'sector-timing-comparison-form',
    imports: [FormsModule, NgSelectModule, ReactiveFormsModule, TeamLogoComponent],
    templateUrl: './sector-timing-comparison-form.component.html',
    styleUrl: './sector-timing-comparison-form.component.css'
})
export class SectorTimingComparisonFormComponent implements OnInit {
    form: FormGroup;
    isLoading = signal(false);
    drivers = computed(() => Object.entries(this.driverState.driversSignal()).map(([k, v]) => ({
        id: k,
        label: v.name,
        teamId: v.teamId,
        gameYear: v.teamDetails?.gameYear
    })));
    selectedVehicleIdx = signal<number | null>(null);
    GameYear = GameYear;

    constructor(private restService: RestService,
        private formBuilder: FormBuilder,
        private driverState: DriverStateService) {
    }

    ngOnInit(): void {
        this.form = this.formBuilder.group({
            vehicleIdx: [null],
            comparingVehicleIdx: [null],
            lapNumber: [null, Validators.max(255)]
        });

        this.restService.get<SectorTimingComparisonModel | null>("/widget-state/get-sector-timing-comparison-model").subscribe(res => {
            if (!res) return;
            this.form.patchValue({
                vehicleIdx: res.vehicleIdx ? `${res.vehicleIdx}` : null,
                comparingVehicleIdx: res.comparingVehicleIdx ? `${res.comparingVehicleIdx}` : null,
                lapNumber: res.lapNumber
            });
        });
    }

    onFormClear() {
        this.form.setValue({ vehicleIdx: null, comparingVehicleIdx: null, lapNumber: null })
    }

    onFormSubmit() {
        const formValue = this.form.value as SectorTimingComparisonModel;
        const body = {
            vehicleIdx: formValue.vehicleIdx ? Number(formValue.vehicleIdx) : null,
            comparingVehicleIdx: formValue.comparingVehicleIdx ? Number(formValue.comparingVehicleIdx) : null,
            lapNumber: formValue.lapNumber
        };

        this.restService.post("/widget-state/update-sector-timing-comparison", body).subscribe();
    }
}
