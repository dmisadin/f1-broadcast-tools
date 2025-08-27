import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: '', redirectTo: 'app', pathMatch: 'full' },
    {
        path: 'app',
        loadComponent: () => import('./features/home/home.component').then(c => c.HomeComponent),
        children: [
            {
                path: 'driver-overrides',
                loadComponent: () => import('./features/settings/driver-overrides/driver-overrides.component')
                    .then(c => c.DriverOverridesComponent),
            },
            {
                path: 'players',
                loadChildren: () => import('./features/players/players.routes').then(r => r.PLAYERS_ROUTES),
            },
            {
                path: 'overlays',
                loadChildren: () => import('./features/overlay/overlay.routes').then(r => r.OVERLAY_ROUTES)
            },
        ],
    },
    {
        path: 'settings',
        loadComponent: () => import('./features/settings/settings.component').then(c => c.SettingsComponent),
    },

    {
        path: 'widgets',
        loadChildren: () => import('./features/widgets/widgets.routes').then(r => r.WIDGETS_ROUTES)
    }
];
