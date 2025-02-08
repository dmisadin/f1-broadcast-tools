import { Team } from "./Enumerations";

export interface PlayerOverride {
    id: number;
    racingNumber: number;
    name: string;
    nameOverride?: string;
    position?: number;
    team?: Team;
}