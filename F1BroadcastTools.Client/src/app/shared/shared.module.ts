import { NgModule } from "@angular/core";
import { MilisecondsToGapPipe } from "./pipes/milisecondsToGap.pipe";
import { SidebarComponent } from "./components/sidebar/sidebar.component";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";
import { DataTableModule } from "../thirdparty/ng-datatable/ng-datatable.module";
import { GridComponent } from "./components/grid/grid/grid.component";
import { NgSelectModule } from "@ng-select/ng-select";
import { PlayerSearchComponent } from "./components/player-search/player-search.component";
import { FormsModule } from "@angular/forms";
import { HasFlagPipe } from "./pipes/has-flag.pipe";

const components = [
    SidebarComponent,
    GridComponent,
    PlayerSearchComponent
];

const pipes = [
    MilisecondsToGapPipe,
    HasFlagPipe
];

const allDeclarations = [
    ...components,
    ...pipes
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        DataTableModule,
        NgSelectModule,
        FormsModule
    ],
    declarations: allDeclarations,
    exports: allDeclarations
})
export class SharedModule { }