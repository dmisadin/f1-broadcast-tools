import { Directive, input } from "@angular/core";

@Directive()
export abstract class TeamLogoBase {
    color = input<string>();
}