import { Endpoints } from "../models/common";

function BuildGenericEndpoints(controllerName: string): Endpoints {
    return {
        get: (id) => `/${controllerName}/get?id=${id}`,
        add: `/${controllerName}/add`,
        getGridStructure: `/${controllerName}/get-grid-structure`,
        getGridData: `/${controllerName}/get-grid-data`,
        customEndpoint: (endpoint) => `/${controllerName}/${endpoint}`
    }
}

export const PlayerEndpoints: Endpoints = BuildGenericEndpoints("player");

export const DriverOverrideEndpoints = {
    getAll: "/driver-override/get-all"
}