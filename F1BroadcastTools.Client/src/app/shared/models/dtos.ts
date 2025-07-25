import { Team } from "./Enumerations";

export interface DriverOverrideDto {
    id: number;
    racingNumber: number;
    name: string;
    position?: number;
    team?: Team;

    playerId?: number;
    player?: DriverPlayerDto;
}

export interface DriverPlayerDto {
    driverId: number;
    playerId: number;
    playerName: string;
}