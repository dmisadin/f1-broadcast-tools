import { Routes } from '@angular/router';

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
    },
    {
        path: ':id/editor',
        loadComponent: () => import('./overlay-editor/overlay-editor.component').then(c => c.OverlayEditorComponent),
        children: [
            {
                path: 'widget',
                loadComponent: () => import('./overlay-editor/widget-form/widget-form.component').then(c => c.WidgetFormComponent),
            },
            {
                path: 'widget/:id',
                loadComponent: () => import('./overlay-editor/widget-form/widget-form.component').then(c => c.WidgetFormComponent),
            },
        ],
    }
];