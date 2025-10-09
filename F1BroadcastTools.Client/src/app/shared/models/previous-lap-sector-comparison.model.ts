import { DriverBasicDetails } from "./driver.model";
import { SectorTimeStatus } from "./stopwatch.model";

export interface PreviousLapSectorComparison {
    lapNumber: number;
    driverPreviousLapDetails: DriverPreviousLapDetails;
    comparingDriverPreviousLapDetails: DriverPreviousLapDetails;
}

export interface DriverPreviousLapDetails {
    position: number;
    vehicleIdx: number;
    visualTyreCompound: string;
    driver: DriverBasicDetails;
    lapTiming: LapTiming;
}

export interface LapTiming {
    lapTime: string;
    sector1Time: string;
    sector2Time: string;
    sector3Time: string;
    lapTimeStatus?: SectorTimeStatus;
    sector1TimeStatus?: SectorTimeStatus;
    sector2TimeStatus?: SectorTimeStatus;
    sector3TimeStatus?: SectorTimeStatus;
}