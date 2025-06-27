import { ResultStatus, SafetyCarStatus, Team } from "./Enumerations";
import { TeamDetails } from "./team.model";

export interface TimingTower {
    currentLap: number;
    totalLaps: number;

    safetyCarStatus: SafetyCarStatus;
    sectorYellowFlags: boolean[];
    showAdditionalInfo: number;

    driverTimingDetails: DriverTimingDetails[];
    spectatorCarIdx: number;
}

export interface DriverTimingDetails {
    vehicleIdx: number;
    position: number;
    teamId: Team;
    teamDetails?: TeamDetails;
    name: string;
    tyreAge: number;
    visualTyreCompound: string;
    gap: string;
    resultStatus: ResultStatus;
    penalties: number;
    warnings: number;
    hasFastestLap: boolean;
    isInPits: boolean;
    numPitStops: number;
    positionsGained: number;
}

export enum Sector
{
    Sector1,
    Sector2,
    Sector3
}