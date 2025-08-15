import { signal, inject, Component } from "@angular/core";
import { NavigationService } from "../../../core/services/navigation.service";

@Component({
    template: '',
    providers: [NavigationService]
})
export abstract class BaseListComponent {
    isLoading = signal(false);

    protected abstract pageTitle: string;

    protected navigationService = inject(NavigationService);
}