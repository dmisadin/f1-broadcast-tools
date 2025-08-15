import { Component } from '@angular/core';
import { ModalDialogComponent } from "../../../shared/components/modal-dialog/modal-dialog.component";
import { BaseFormComponent } from '../../../shared/components/forms/base-form.component';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { NavigationService } from '../../../core/services/navigation.service';
import { OverlayEndpoints } from '../../../shared/constants/apiUrls';

@Component({
    selector: 'overlay-form',
    imports: [ModalDialogComponent, ReactiveFormsModule],
    templateUrl: './overlay-form.component.html',
    styleUrl: './overlay-form.component.css',
    providers: [NavigationService]
})
export class OverlayFormComponent extends BaseFormComponent {
    title = new FormControl('', Validators.required);

    onFormSubmit() {
        this.isLoading.set(true);
        this.restService.save(OverlayEndpoints.add, { title: this.title.value })
            .subscribe({
                next: newId => {this.isLoading.set(false); this.navigationService.navigateToRelative('../' + newId)},
                error: error => console.error(error),
                complete: () => console.info('complete')
            });
    }

    onFormDiscard() {
        this.navigationService.navigateToParent(!!this.entityId);
    }
}
