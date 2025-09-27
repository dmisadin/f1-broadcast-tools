import { Routes } from '@angular/router';

export const WIDGETS_ROUTES: Routes = [
    {
        path: 'timing-tower',
        loadComponent: () => import('./timing-tower/timing-tower.component').then(c => c.TimingTowerComponent),
    },
    {
        path: 'minimap',
        loadComponent: () => import('./minimap/minimap.component').then(c => c.MinimapComponent),
    },
    {
        path: 'stopwatch',
        loadComponent: () => import('./stopwatch-spectated/stopwatch-spectated.component').then(c => c.StopwatchSpectatedComponent),
    },
    {
        path: 'stopwatch-list',
        loadComponent: () => import('./stopwatch-list/stopwatch-list.component').then(c => c.StopwatchListComponent),
    },
    {
        path: 'halo-hud',
        loadComponent: () => import('./halo-hud/halo-hud.component').then(c => c.HaloHudComponent),
    },
];