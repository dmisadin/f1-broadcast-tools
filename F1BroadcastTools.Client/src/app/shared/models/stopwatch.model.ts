import { DriverBasicDetails } from "./driver.model";

export interface Stopwatch {
    leaderLap: FastestQualifyingLap;
    
    cars: StopwatchCar[];
}

export interface StopwatchCar {
    position: number;
    currentTime: string;
    isLapValid: boolean;
    lapProgress: number;
    tyreCompoundVisual: string;

    sector1TimeStatus?: SectorTimeStatus;
    sector2TimeStatus?: SectorTimeStatus;
    lapTimeStatus?: SectorTimeStatus;

    sector1GapToLeader?: string;
    sector2GapToLeader?: string;
    lapGapToLeader?: string;

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