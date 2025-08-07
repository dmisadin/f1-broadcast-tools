import { DriverBasicDetails } from "./driver.model";

export interface TelemetryDashboard {
    vehicleIdx: number;
    speed: number;
    throttle: number;
    brake: number;
    gear: number;
    engineRPM: number;
    engineRPMPercentage: number;
    drs: boolean;

    position: number;
}

export interface HaloTelemetryDashboard extends TelemetryDashboard {
    turn: number;
    driver?: DriverBasicDetails;
    nextDriver?: DriverBasicDetails;
}
