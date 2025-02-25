import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { DriverOverrides } from "./driver-overrides/driver-overrides.component";

const routes: Routes = [
    {
        path: '',
        component: DriverOverrides
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SettingsRoutingModule { }
