import { NgModule } from "@angular/core";
import { SettingsRoutingModule } from "./settings-routing.module";
import { CommonModule } from "@angular/common";
import { PlayerOverrides } from "./player-overrides/player-overrides.component";
import { FormsModule } from "@angular/forms";

@NgModule({
    imports: [
        SettingsRoutingModule,
        CommonModule,
        FormsModule
    ],
    declarations: [
        PlayerOverrides
    ]
})
export class SettingsModule { }