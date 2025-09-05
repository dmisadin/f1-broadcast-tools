import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

@Injectable()
export class NavigationService {
    constructor(private router: Router,
        private activatedRoute: ActivatedRoute,
        private location: Location) { }

    public navigateToParent(isEdit = false) {
        if (isEdit) {
            this.router.navigate(["../../"], { relativeTo: this.activatedRoute })
            return;
        }

        this.router.navigate(["../"], { relativeTo: this.activatedRoute })
    }

    public navigateToGrandParent() {
        this.router.navigate(["../../"], { relativeTo: this.activatedRoute })
    }

    public isEdit(): boolean {
        return !!this.activatedRoute.snapshot.params['id'];
    }

    public getId(shouldGetParentId = false, isNullable = false): string {
        let selector = 'id';

        if (isNullable)
            selector = 'id?';

        if (shouldGetParentId)
            return this.activatedRoute.parent!.snapshot.params[selector];

        return this.activatedRoute.snapshot.params[selector];
    }

    public goBack() {
        this.location.back();
    }

    public getCustomDataRouteParam(dataProperty: string) {
        return this.activatedRoute.snapshot.params[dataProperty];
    }

    public navigateToRelative(...pathFragments: string[]) {
        this.router.navigate(pathFragments, { relativeTo: this.activatedRoute })
    }
}
