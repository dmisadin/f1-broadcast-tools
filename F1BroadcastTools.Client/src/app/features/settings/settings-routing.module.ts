import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { PlayerOverrides } from "./player-overrides/player-overrides.component";

const routes: Routes = [
    {
        path: '',
        component: PlayerOverrides
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SettingsRoutingModule { }
