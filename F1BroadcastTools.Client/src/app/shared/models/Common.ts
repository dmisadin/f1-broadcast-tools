export interface KeyNumberPair { 
    key: string; 
    value: number 
}

export interface Endpoints {
    get: (id: number) => string;
    add: string;
    getGridStructure: string;
    getGridData: string;
    customEndpoint: (endpoint: string) => string;
}

export interface LookupDto {
    id: number;
    label: string;
}