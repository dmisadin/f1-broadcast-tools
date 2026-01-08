import { DriverBasicDetails } from "./driver.model";

export interface TyreStintComparison {
    totalLaps: number;
    totalSizePercentage: number;
    cars: CarTyreStints[];
    pitStopLapMarkers: number[];
}

export interface TyreStint {
    tyreCompound: string;
    tyreColor: string;
    endLap: number | null;
    duration: number;
    sizePercentage: number;
}

export interface CarTyreStints {
    driver: DriverBasicDetails;
    tyreStints: TyreStint[];
}