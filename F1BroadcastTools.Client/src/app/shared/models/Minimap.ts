export interface Minimap {
    trackId: number;
    cars: MinimapCar[];
    spectatorCarIdx: number;
    rotation?: number;
}

export interface MinimapCar {
    vehicleIdx: number;
    x: number;
    y: number;
    rotation: number;
    teamColor: string;
}