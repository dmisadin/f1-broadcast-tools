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
        path: 'overlay',
        loadComponent: () => import('./features/overlay/overlay.component').then(c => c.OverlayComponent),
    },
    {
        path: 'overlay/edit',
        loadComponent: () => import('./features/overlay/edit-overlay/edit-overlay.component').then(c => c.EditOverlayComponent)
    }, 
    {
        path: 'overlay/timing-tower',
        loadChildren: () => import('./features/timing-tower/timing-tower.module').then(m => m.TimingTowerModule)
    },
    {
        path: 'overlay/minimap',
        loadComponent: () => import('./features/minimap/minimap.component').then(c => c.MinimapComponent)
    },
    {
        path: 'overlay/stopwatch',
        loadComponent: () => import('./features/stopwatch-list/stopwatch-list.component').then(c => c.StopwatchListComponent)
    },
    {
        path: 'overlay/halo-hud',
        loadComponent: () => import('./features/halo-hud/halo-hud.component').then(c => c.HaloHudComponent)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
