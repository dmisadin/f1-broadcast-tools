export interface TimingTower {
    currentLap: number;
    totalLaps: number;
    driverTimingDetails: DriverTimingDetails[]
}

export interface DriverTimingDetails {
    vehicleIdx: number;
    position: number;
    teamId: number;
    name: string;
    tyreAge: number;
    visualTyreCompound: string;
    gapOrResultStatus: string;
    penalties: number;
    warnings: number;
    hasFastestLap: boolean;
}