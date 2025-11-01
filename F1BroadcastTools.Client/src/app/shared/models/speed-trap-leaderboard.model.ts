import { DriverBasicDetails } from "./driver.model";

export interface SpeedTrapLeaderboard {
    vehicleIdx: number;
    driver: DriverBasicDetails;
    speed: number;
    ordinalNumber: number;
    needSeparator: boolean;
}