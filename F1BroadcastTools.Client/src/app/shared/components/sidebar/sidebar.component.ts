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
        { icon: '⛹️‍♂️', label: 'Players', route: '/players'},
        { icon: '🏎️', label: 'Timing Tower', route: '/timing-tower' },
        { icon: '🗺️', label: 'Minimap', route: '/minimap' },
        { icon: '⏱️', label: 'Stopwatch', route: '/stopwatch' },
        { icon: '🗠', label: 'Halo Telemetry', route: '/halo-hud' },
    ];
}