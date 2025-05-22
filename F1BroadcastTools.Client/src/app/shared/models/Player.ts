import { Nationality, Team } from "./Enumerations";

export interface Player {
    id?: number;
    name: string;
    nationality: Nationality
}