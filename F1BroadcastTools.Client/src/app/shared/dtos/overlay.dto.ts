import { BaseDto } from "./base.dto";
import { WidgetDto } from "./widget.dto";

export interface OverlayDto extends BaseDto {
    title: string;
    widgets: WidgetDto[]
}