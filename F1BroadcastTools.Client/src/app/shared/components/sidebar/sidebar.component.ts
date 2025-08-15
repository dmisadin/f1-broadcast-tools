import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { RouterLink } from "@angular/router";

@Component({
    selector: "sidebar",
    templateUrl: "./sidebar.component.html",
    styleUrl: "./sidebar.component.css",
    imports: [CommonModule, RouterLink]
})
export class SidebarComponent {
    isCollapsed = false;

    toggleSidebar() {
        this.isCollapsed = !this.isCollapsed;
    }

    menuItems = [
        { icon: '🏠', label: 'Home', route: '/' },
        { icon: '⚙️', label: 'Settings', route: '/settings' },
        { icon: '⚙️', label: 'Driver Overrides', route: '/settings/driver-overrides' },
        { icon: '⛹️‍♂️', label: 'Players', route: '/settings/players'},
        { icon: '🏎️', label: 'Timing Tower', route: '/widgets/timing-tower' },
        { icon: '🗺️', label: 'Minimap', route: '/widgets/minimap' },
        { icon: '⏱️', label: 'Stopwatch', route: '/widgets/stopwatch' },
        { icon: '🗠', label: 'Halo Telemetry', route: '/widgets/halo-hud' },
    ];
}