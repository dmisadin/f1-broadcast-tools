import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TimingTowerComponent } from './timing-tower.component';

const routes: Routes = [
    {
        path: '', component: TimingTowerComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TimingTowerRoutingModule { }
