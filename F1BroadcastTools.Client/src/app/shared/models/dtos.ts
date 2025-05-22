import { LookupDto } from "./common";
import { Team } from "./Enumerations";

export interface DriverOverrideDto {
    id: number;
    racingNumber: number;
    name: string;
    position?: number;
    team?: Team;

    playerId?: number;
    player?: LookupDto;
}