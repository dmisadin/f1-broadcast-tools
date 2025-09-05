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

export interface HybridTelemetryDashboard extends TelemetryDashboard {
    ersDeployMode: ERSDeployMode;
}

export interface HaloTelemetryDashboard extends HybridTelemetryDashboard {
    turn: number;
    driver: DriverBasicDetails | null;
    nextDriver: DriverBasicDetails | null;
}

export enum ERSDeployMode {
    None = 0,
    Medium,
    Hotlap,
    Overtake
}