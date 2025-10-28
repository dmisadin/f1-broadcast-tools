using F1GameDataParser.Database.Entities.Widgets;
using F1GameDataParser.Enums;
using F1GameDataParser.Models.Event;
using F1GameDataParser.State;
using F1GameDataParser.Utility;
using F1GameDataParser.ViewModels.SessionEvent;

namespace F1GameDataParser.Services
{
    public class SessionEventService
    {
        private readonly LapState lapState;
        private readonly WebSocketBroadcastService webSocketBroadcastService;
        private readonly DriverOverrideService driverOverrideService;

        public SessionEventService(LapState lapState,
                                WebSocketBroadcastService webSocketBroadcastService,
                                DriverOverrideService driverOverrideService)
        {
            this.lapState = lapState;
            this.webSocketBroadcastService = webSocketBroadcastService;
            this.driverOverrideService = driverOverrideService;
        }

        public void HandlePenaltyTracking(Penalty penalty, GameYear gameYear)
        {
            // Note: In F1 23, you can't serve 10s time penalty, only 5s. Tested in 50% race.
            if (penalty.Time != 5)
                return;

            lock (lapState.Lock)
            {
                if (!lapState.State.TryGetValue(penalty.VehicleIdx, out var driverLapData))
                    return;

                driverLapData?.UnservedPenalties.Enqueue(penalty.Time);
            }
        }

        public async void HandleSessionEvents(Event sessionEvent)
        {
            var id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            switch (sessionEvent.EventDetails.Details)
            {
                case FastestLap fastestLap:
                    await HandleFastestLap(fastestLap, id);
                    break;
                case Retirement retirement:
                    await HandleRetirement(retirement, id);
                    break;
                case DrsDisabled drsDisabled:
                    await HandleDrsDisabled(drsDisabled, id);
                    break;
                case DriveThroughPenaltyServed driveThroughPenaltyServed:
                    await HandleDriveThroughPenaltyServed(driveThroughPenaltyServed, id);
                    break;
                case StopGoPenaltyServed stopGoPenaltyServed:
                    await HandleStopGoPenaltyServed(stopGoPenaltyServed, id);
                    break;
                case Penalty penalty:
                    await HandlePenaltyIssued(penalty, id);
                    break;
                case SafetyCar safetyCar:
                    await HandleSafetyCar(safetyCar, id);
                    break;
                default:
                    await HandleSimpleEvents(sessionEvent, id);
                    break;
            }
        }

        private async Task HandleFastestLap(FastestLap fastestLap, long id)
        {
            var driver = driverOverrideService.GetDriverBasicDetails(fastestLap.VehicleIdx);

            if (driver is null)
                return;

            var eventModel = new SessionEvent
            {
                Id = id,
                Driver = driver,
                Title = $"New Fastest Lap of the Session",
                Description = $"{driver.Name} - {TimeUtility.SecondsToTime(fastestLap.LapTime)}"
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

        private async Task HandleRetirement(Retirement retirement, long id)
        {
            var driver = driverOverrideService.GetDriverBasicDetails(retirement.VehicleIdx);

            if (driver is null)
                return;

            string? description = retirement.Reason >= ResultReason.Retired ? retirement.Reason?.GetLabel() : null;

            var eventModel = new SessionEvent
            {
                Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Driver = driver,
                Title = $"{driver.Name} retired.",
                Description = description
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

        private async Task HandleDrsDisabled(DrsDisabled drsDisabled, long id)
        {
            var eventModel = new SessionEvent
            {
                Id = id,
                Title = "DRS has been disabled.",
                Description = drsDisabled.DRSDisabledReason.ToString()
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

        private async Task HandleDriveThroughPenaltyServed(DriveThroughPenaltyServed driveThroughPenaltyServed, long id)
        {
            var driver = driverOverrideService.GetDriverBasicDetails(driveThroughPenaltyServed.VehicleIdx);

            if (driver is null)
                return;

            var eventModel = new SessionEvent
            {
                Id = id,
                Driver = driver,
                Title = $"{driver.Name} has served a Drive-Through penalty."
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

        private async Task HandleStopGoPenaltyServed(StopGoPenaltyServed stopGoPenaltyServed, long id)
        {
            var driver = driverOverrideService.GetDriverBasicDetails(stopGoPenaltyServed.VehicleIdx);

            if (driver is null)
                return;

            var eventModel = new SessionEvent
            {
                Id = id,
                Driver = driver,
                Title = $"{driver.Name} has served a Stop-Go penalty."
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

        private async Task HandlePenaltyIssued(Penalty penalty, long id)
        {
            var driver = driverOverrideService.GetDriverBasicDetails(penalty.VehicleIdx);

            if (driver is null)
                return;

            string title = "";

            switch (penalty.PenaltyType)
            {
                case PenaltyType.DriveThrough:
                    title = $"{driver.Name} has received a drive-through penalty";
                    break;
                //case PenaltyType.StopGo: // Bugged as of: 28.10.2025.
                //    title = $"{driver.Name} has received a stop-go penalty";
                //    break;
                case PenaltyType.GridPenalty:
                    title = $"{driver.Name} has received a grid-drop penalty";
                    break;
                case PenaltyType.StopGo:
                case PenaltyType.TimePenalty:
                    title = $"{driver.Name} has received a {penalty.Time}s time penalty";
                    break;
                case PenaltyType.Disqualified:
                    title = $"{driver.Name} has been disqualified";
                    break;
                case PenaltyType.RemovedFromFormationLap:
                    title = $"{driver.Name} has been removed from the formation lap";
                    break;
                case PenaltyType.ParkedTooLongTimer: // je li ovo samo za timer ili bas penal?
                    title = $"{driver.Name} has been parked for too long";
                    break;
                case PenaltyType.TyreRegulations:
                    title = $"{driver.Name} has breached tyre regulations";
                    break;
                //case PenaltyType.Retired:
                //    title = $"{driver.Name} has retired";
                //    break;
                case PenaltyType.BlackFlagTimer: // je li ovo samo za timer ili bas penal?
                    title = $"{driver.Name} has received black flag";
                    break;
                default:
                    return;
            }

            var eventModel = new SessionEvent
            {
                Id = id,
                Driver = driver,
                Title = title,
                Description = penalty.InfringementType.GetLabel()
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

        private async Task HandleSafetyCar(SafetyCar safetyCar, long id)
        {
            string eventType;
            if (safetyCar.EventType == SafetyCarEventType.Deployed)
                eventType = "has been deployed";
            else if (safetyCar.EventType == SafetyCarEventType.Returning)
                eventType = "is returning";
            else
                return;

            var eventModel = new SessionEvent
            {
                Id = id,
                Title = $"{safetyCar.SafetyCarType.GetLabel()} {eventType}"
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

        private async Task HandleSimpleEvents(Event sessionEvent, long id)
        {
            var eventMessage = sessionEvent.EventStringCode switch
            {
                    EventCodes.DrsEnabled => "DRS enabled.",
                    // TODO: Implement older games events that have no details (like SCAR)
                    _ => null
            };

            if (eventMessage is null)
                return;

            var eventModel = new SessionEvent
            {
                Id = id,
                Title = eventMessage
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }
    }
}
