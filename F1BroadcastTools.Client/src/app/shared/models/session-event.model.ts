import { DriverBasicDetails } from "./driver.model";

export interface SessionEvent {
    id: number;
    driver: DriverBasicDetails | null;
    involvedDriver: DriverBasicDetails | null;
    title: string;
    description: string | null;
}