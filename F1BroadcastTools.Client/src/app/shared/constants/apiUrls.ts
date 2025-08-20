import { Endpoints } from "../models/common";

function BuildGenericEndpoints(controllerName: string): Endpoints {
    return {
        get: (id) => `/${controllerName}/get?id=${id}`,
        add: `/${controllerName}/add`,
        addMany: `/${controllerName}/add-many`,
        update: `/${controllerName}/update`,
        updateMany: `/${controllerName}/update-many`,
        upsert: `/${controllerName}/upsert`,
        getGridStructure: `/${controllerName}/get-grid-structure`,
        getGridData: `/${controllerName}/get-grid-data`,
        customEndpoint: (endpoint) => `/${controllerName}/${endpoint}`
    }
}

export const PlayerEndpoints: Endpoints = BuildGenericEndpoints("player");
export const OverlayEndpoints: Endpoints = BuildGenericEndpoints("overlay");
export const WidgetEndpoints: Endpoints = BuildGenericEndpoints("widget");

export const DriverOverrideEndpoints = {
    getAll: "/driver-override/get-all",
    update: "/driver-override/update"
}