import { SectorTimeStatus } from "../models/stopwatch.model";

export const mapSectorTimeStatusClass = (s: SectorTimeStatus | null | undefined): string => {
    switch (s) {
        case SectorTimeStatus.FasterThanLeader: return 'purple';
        case SectorTimeStatus.PersonalBest: return 'green';
        case SectorTimeStatus.NoImprovement: return 'yellow';
        default: return '';
    }
};