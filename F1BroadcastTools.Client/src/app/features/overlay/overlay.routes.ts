import { Routes } from '@angular/router';
import { SharedFormService } from '../../shared/services/shared-form.service';

export const OVERLAY_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./overlay-list/overlay-list.component').then(c => c.OverlayListComponent),
        children: [
            {
                path: 'add',
                loadComponent: () => import('./overlay-form/overlay-form.component').then(c => c.OverlayFormComponent),
            },
        ],
    },
    {
        path: ':id',
        loadComponent: () => import('./overlay.component').then(c => c.OverlayComponent),
        children: [
            {
                path: 'widget',
                loadComponent: () => import('./overlay-editor/widget-form/widget-form.component').then(c => c.WidgetFormComponent),
            },
            { path: 'widget:id', redirectTo: 'general', pathMatch: 'full' },
            {
                path: 'widget/:id',
                loadComponent: () => import('./widget-details-form/widget-details-form.component').then(c => c.WidgetDetailsFormComponent),
                providers: [SharedFormService],
                children: [
                    { path: '', redirectTo: 'general', pathMatch: 'full' },
                    {
                        path: 'general',
                        loadComponent: () => import('./widget-details-form/widget-general-form/widget-general-form.component').then(c => c.WidgetGeneralFormComponent)
                    },
                    {
                        path: 'specific',
                        loadComponent: () => import('./widget-details-form/widget-specific-form/widget-specific-form.component').then(c => c.WidgetSpecificFormComponent)
                    }
                ]
            },
        ],
    }
];
