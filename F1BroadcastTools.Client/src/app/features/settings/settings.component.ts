import { Component } from '@angular/core';
import { SharedModule } from "../../shared/shared.module";
import { RouterOutlet } from "@angular/router";

@Component({
  selector: 'settings',
  imports: [SharedModule, RouterOutlet],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {

}
