import { DriverBasicDetails } from "./driver.model";

export interface TyreStintComparison {
    driver: DriverBasicDetails;
    tyreStints: TyreStint[];
}

export interface TyreStint {
    tyreCompound: string;
    endLap?: number;
    duration: number;
    sizePercentage: number;
}