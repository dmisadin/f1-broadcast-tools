import { Component } from '@angular/core';
import { RouterOutlet } from "@angular/router";
import { SidebarComponent } from '../../shared/components/sidebar/sidebar.component';

@Component({
    selector: 'settings',
    imports: [RouterOutlet, SidebarComponent],
    templateUrl: './settings.component.html',
    styleUrl: './settings.component.css'
})
export class SettingsComponent {

}
