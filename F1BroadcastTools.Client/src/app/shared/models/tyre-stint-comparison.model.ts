import { DriverBasicDetails } from "./driver.model";

export interface TyreStintComparison {
    totalLaps: number;
    cars: CarTyreStints[];
    pitStopLapMarkers: number[];
}

export interface TyreStint {
    tyreCompound: string;
    tyreColor: string;
    startLap: number;
    endLap: number | null;
    duration: number;
}

export interface CarTyreStints {
    driver: DriverBasicDetails;
    tyreStints: TyreStint[];
}