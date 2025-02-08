import { NgModule } from "@angular/core";
import { MilisecondsToGapPipe } from "./pipes/milisecondsToGap.pipe";
import { SidebarComponent } from "./components/sidebar/sidebar.component";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";

const components = [
    SidebarComponent
];

const pipes = [
    MilisecondsToGapPipe
];

const allDeclarations = [
    ...components,
    ...pipes
];

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
    ],
    declarations: allDeclarations,
    exports: allDeclarations
})
export class SharedModule { }