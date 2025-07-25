import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: 'settings',
        loadChildren: () => import('./features/settings/settings.module').then(m => m.SettingsModule)
    },
    {
        path: 'timing-tower',
        loadChildren: () => import('./features/timing-tower/timing-tower.module').then(m => m.TimingTowerModule)
    },
    {
        path: 'players',
        loadChildren: () => import('./features/players/players.module').then(m => m.PlayersModule)
    },
    {
        path: 'minimap',
        loadComponent: () => import('./features/minimap/minimap.component').then(m => m.MinimapComponent)
    },
    {
        path: 'stopwatch',
        loadComponent: () => import('./features/stopwatch-list/stopwatch-list.component').then(m => m.StopwatchListComponent)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
