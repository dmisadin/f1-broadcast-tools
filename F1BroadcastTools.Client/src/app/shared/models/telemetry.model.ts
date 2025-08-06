export interface TelemetryDashboard {
    vehicleIdx: number;
    speed: number;
    throttle: number;
    brake: number;
    gear: number;
    engineRPM: number;
    engineRPMPercentage: number;
    drs: boolean;
}

export interface HaloTelemetryDashboard extends TelemetryDashboard {
    
}
