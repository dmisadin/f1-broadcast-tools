import { DriverStatus, GameYear, ResultStatus, SafetyCarStatus, Team } from "./Enumerations";
import { TeamDetails } from "./team.model";

export interface TimingTower {
    gameYear: GameYear;
    isRaceSession: boolean;
    isSessionFinished: boolean;
    currentLap: number;
    totalLaps: number;

    safetyCarStatus: SafetyCarStatus;
    sectorYellowFlags: boolean[];
    showAdditionalInfo: number;
    sessionTimeLeft: string | null;

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
    driverStatus: DriverStatus;
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