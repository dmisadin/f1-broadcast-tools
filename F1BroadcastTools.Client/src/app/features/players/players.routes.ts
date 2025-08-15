import { Routes } from '@angular/router';

export const PLAYERS_ROUTES: Routes = [
    {
        path: '',
        loadComponent: () => import('./players/players.component').then(c => c.PlayersComponent)
    }
];