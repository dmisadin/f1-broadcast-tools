export interface TelemetryDashboard {
    vehicleIdx: number;
    speed: number;
    throttle: number;
    brake: number;
    gear: number;
    engineRPM: number;
    drs: boolean;
    revLightsPercent: number;
}

export interface HaloTelemetryDashboard extends TelemetryDashboard {
    
}
