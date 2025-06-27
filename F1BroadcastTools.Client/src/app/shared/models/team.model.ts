import { Team } from "./Enumerations"

export interface TeamDetails {
    id: Team;
    name: string;
    primaryColor: string;
    secondaryColor: string;
    textColor: string;
}