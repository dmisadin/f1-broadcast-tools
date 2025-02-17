import { NgModule } from "@angular/core";
import { PlayersComponent } from "./players/players.component";
import { CommonModule } from "@angular/common";
import { PlayersRoutingModule } from "./player-routing.module";
import { SharedModule } from "../../shared/shared.module";
import { DataTableModule } from "../../thirdparty/ng-datatable/ng-datatable.module";

@NgModule({
    imports: [
    CommonModule,
    PlayersRoutingModule,
    SharedModule,
    DataTableModule
],
    declarations: [
        PlayersComponent
    ],
})
export class PlayersModule { }