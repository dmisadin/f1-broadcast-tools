import { FormGroup } from "@angular/forms";

export interface KeyNumberPair { 
    key: string; 
    value: number 
}

export interface Endpoints {
    get: (id: number) => string;
    add: string;
    addMany: string;
    update: string;
    updateMany: string;
    upsert: string;
    getGridStructure: string;
    getGridData: string;
    customEndpoint: (endpoint: string) => string;
}

export interface LookupDto {
    id: number;
    label: string;
}

export interface Coordinates {
    x: number;
    y: number;
}

export interface DragItemCoordinates {
    id: number;
    position: Coordinates;
}

export interface TabStructure {
    title: string;
    route: string;
    color?: string;
    formGroup?: FormGroup;
}