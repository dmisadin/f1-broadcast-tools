import { DriverBasicDetails } from "./driver.model";

export interface Stopwatch {
    fastestLap?: FastestQualifyingLap | null;
    secondFastestLap?: FastestQualifyingLap | null;
    
    cars: StopwatchCar[];
}

export interface StopwatchCar {
    position: number;
    lastLapTime: string;
    currentTime: string;
    isLapValid: boolean;
    lapProgress: number;
    tyreCompoundVisual: string;

    sector1TimeStatus?: SectorTimeStatus;
    sector2TimeStatus?: SectorTimeStatus;
    sector3TimeStatus?: SectorTimeStatus;
    lapTimeStatus?: SectorTimeStatus;

    sector1GapToLeader?: string;
    sector2GapToLeader?: string;
    sector3GapToLeader?: string;
    lapGapToLeader?: string;

    sector1TimeStatusRelativeToPole?: SectorTimeStatus;
    sector2TimeStatusRelativeToPole?: SectorTimeStatus;
    lapTimeStatusRelativeToPole?: SectorTimeStatus;

    driver: DriverBasicDetails;
}

export enum SectorTimeStatus {
    NoImprovement = 0,
    PersonalBest = 1,
    FasterThanLeader = 2,
}

export interface FastestQualifyingLap {
    driver: DriverBasicDetails;
    lapTime: string; // m:ss.sss
    sector1Time: string;
    sector1And2Time: string;
}