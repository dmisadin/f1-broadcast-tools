import { DriverBasicDetails } from "./driver.model";

export interface TyreStintComparison {
    totalLaps: number;
    totalSizePercentage: number;
    driver: DriverBasicDetails;
    tyreStints: TyreStint[];
}

export interface TyreStint {
    tyreCompound: string;
    tyreColor: string;
    endLap: number | null;
    duration: number;
    sizePercentage: number;
}