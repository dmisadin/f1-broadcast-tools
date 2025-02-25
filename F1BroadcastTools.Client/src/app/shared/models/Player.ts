import { Nationality, Team } from "./Enumerations";

export interface DriverOverride {
    id: number;
    racingNumber: number;
    name: string;
    playerId?: number;
    position?: number;
    team?: Team;
}

export interface Player {
    id?: number;
    name: string;
    nationality: Nationality
}