import { computed, effect, inject, Injectable, Signal, signal } from "@angular/core";
import { DriverBasicDetails } from "../../models/driver.model";
import { WebSocketService } from "../../../core/services/websocket.service";

@Injectable({ providedIn: 'root' })
export class DriverStateService {
    private drivers = signal<Record<number, DriverBasicDetails>>({});
    webSocketService = inject(WebSocketService<DriverBasicDetails[]>)

    constructor() {
        this.webSocketService.connect('ws://localhost:5000/ws/driver/driver-details');

        this.webSocketService.onMessage().subscribe((data: DriverBasicDetails[]) => {
            this.setDrivers(data);
        });
    }

    readonly driversSignal = this.drivers.asReadonly();

    getDriver(vehicleIdx: number): Signal<DriverBasicDetails | undefined> {
        return computed(() => this.drivers()[vehicleIdx]);
    }

    setDrivers(drivers: DriverBasicDetails[]): void {
        const map: Record<number, DriverBasicDetails> = {};
        for (const d of drivers) {
            map[d.vehicleIdx] = d;
        }
        this.drivers.set(map);
    }

    updateDriver(driver: DriverBasicDetails): void {
        this.drivers.update(curr => ({
            ...curr,
            [driver.vehicleIdx]: driver
        }));
    }
}
