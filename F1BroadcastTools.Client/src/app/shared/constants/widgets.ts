import { WidgetType } from "../dtos/widget.dto";

export const WidgetDetails: Record<WidgetType, string> = {
  [WidgetType.TimingTower]: "Timing Tower",
  [WidgetType.Stopwatch]: "Stopwatch",
  [WidgetType.Minimap]: "Minimap",
  [WidgetType.HaloHUD]: "Halo Telemetry",
};

