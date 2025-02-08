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
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
