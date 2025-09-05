import { BaseDto } from "./base.dto";

export enum WidgetType {
    TimingTower = 1,
    Stopwatch,
    Minimap, 
    HaloHUD
}

export interface WidgetDto extends BaseDto {
    overlayId: number;
    widgetType: WidgetType;
    positionX: number;
    positionY: number;
}