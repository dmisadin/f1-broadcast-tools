import { inject, Injectable, OnInit, signal } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { NavigationService } from "../../../core/services/navigation.service";
import { RestService } from "../../../core/services/rest.service";

@Injectable()
export class BaseFormComponent implements OnInit{
    mode = signal<FormType>("add");
    isLoading = signal(false);
    entityId: number | null;

    protected activatedRoute = inject(ActivatedRoute);
    protected navigationService = inject(NavigationService);
    protected restService = inject(RestService);

    ngOnInit() {
        const entityId = this.activatedRoute.snapshot.paramMap.get('id');
        this.entityId = entityId ? parseInt(entityId) : null;
        this.mode.set(this.entityId ? "edit" : "add");
    }
}

export type FormType = "add" | "edit";