import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    {
        path: 'home',
        loadComponent: () => import('./features/home/home.component').then(c => c.HomeComponent)
    },
    {
        path: 'settings',
        loadComponent: () => import('./features/settings/settings.component').then(c => c.SettingsComponent),
        children: [
            {
                path: 'driver-overrides',
                loadComponent: () => import('./features/settings/driver-overrides/driver-overrides.component').then(c => c.DriverOverridesComponent)
            },
            {
                path: 'players',
                loadChildren: () => import('./features/players/players.module').then(m => m.PlayersModule)
            },
        ]
    },
    {
        path: 'overlays',
        loadComponent: () => import('./features/overlay/overlay-list/overlay-list.component').then(c => c.OverlayListComponent),
        children: [
            {
                path: 'add',
                loadComponent: () => import('./features/overlay/overlay-form/overlay-form.component').then(c => c.OverlayFormComponent)
            }
        ]
    },
    {
        path: 'overlays/:id',
        loadComponent: () => import('./features/overlay/overlay.component').then(c => c.OverlayComponent),
    },
    {
        path: 'overlays/:id/editor',
        loadComponent: () => import('./features/overlay/overlay-editor/overlay-editor.component').then(c => c.OverlayEditorComponent),
        children: [
            {
                path: 'widget',
                loadComponent: () => import('./features/overlay/overlay-editor/widget-form/widget-form.component').then(c => c.WidgetFormComponent)
            },
            {
                path: 'widget/:id',
                loadComponent: () => import('./features/overlay/overlay-editor/widget-form/widget-form.component').then(c => c.WidgetFormComponent)
            }
        ]
    },
    {
        path: 'overlays/timing-tower',
        loadChildren: () => import('./features/timing-tower/timing-tower.module').then(m => m.TimingTowerModule)
    },
    {
        path: 'overlays/minimap',
        loadComponent: () => import('./features/minimap/minimap.component').then(c => c.MinimapComponent)
    },
    {
        path: 'overlays/stopwatch',
        loadComponent: () => import('./features/stopwatch-list/stopwatch-list.component').then(c => c.StopwatchListComponent)
    },
    {
        path: 'overlays/halo-hud',
        loadComponent: () => import('./features/halo-hud/halo-hud.component').then(c => c.HaloHudComponent)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
