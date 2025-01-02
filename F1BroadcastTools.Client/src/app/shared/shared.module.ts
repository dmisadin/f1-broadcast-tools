import { NgModule } from "@angular/core";
import { MilisecondsToGapPipe } from "./pipes/milisecondsToGap.pipe";

//const components = [];

const pipes = [
    MilisecondsToGapPipe
];

const allDeclarations = [
    //...components,
    ...pipes
];

@NgModule({
    imports: [],
    declarations: allDeclarations,
    exports: allDeclarations
})
export class SharedModule { }