import { Component } from "@angular/core";

@Component({
    standalone: false,
    selector: "sidebar",
    templateUrl: "./sidebar.component.html",
    styleUrl: "./sidebar.component.css"
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
        { icon: '🏎️', label: 'Timing Tower', route: '/overlay/timing-tower' },
        { icon: '🗺️', label: 'Minimap', route: '/overlay/minimap' },
        { icon: '⏱️', label: 'Stopwatch', route: '/overlay/stopwatch' },
        { icon: '🗠', label: 'Halo Telemetry', route: '/overlay/halo-hud' },
    ];
}