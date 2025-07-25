import { Team } from "./Enumerations";
import { TeamDetails } from "./team.model";

export interface DriverBasicDetails {
    vehicleIdx: number;
    teamId: Team;
    teamDetails?: TeamDetails;
    name: string;
}