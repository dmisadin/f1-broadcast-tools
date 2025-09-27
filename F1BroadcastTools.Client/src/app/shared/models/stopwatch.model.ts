import { DriverBasicDetails } from "./driver.model";
import { GameYear } from "./Enumerations";

export interface StopwatchBase {
    gameYear: GameYear;
    fastestLap: FastestQualifyingLap | null;
    secondFastestLap: FastestQualifyingLap | null;
}

export interface StopwatchSpectated extends StopwatchBase {
    car: StopwatchCar;
}

export interface StopwatchList extends StopwatchBase {
    cars: StopwatchCar[];
}

export interface StopwatchCar {
    vehicleIdx: number;
    position: number;
    lastLapTime: string;
    currentTime: string;
    isLapValid: boolean;
    lapProgress: number;
    tyreCompoundVisual: string;

    sector1TimeStatus?: SectorTimeStatus | null;
    sector2TimeStatus?: SectorTimeStatus | null;
    sector3TimeStatus?: SectorTimeStatus | null;
    lapTimeStatus?: SectorTimeStatus | null;

    sector1GapToLeader?: string | null;
    sector2GapToLeader?: string | null;
    sector3GapToLeader?: string | null;
    lapGapToLeader?: string | null;

    sector1TimeStatusRelativeToPole?: SectorTimeStatus | null;
    sector2TimeStatusRelativeToPole?: SectorTimeStatus | null;
    lapTimeStatusRelativeToPole?: SectorTimeStatus | null;
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