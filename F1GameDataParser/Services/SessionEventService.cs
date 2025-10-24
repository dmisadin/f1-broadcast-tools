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
            var id = sessionEvent.Header.FrameIdentifier;
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
                default:
                    await HandleSimpleEvents(sessionEvent, id);
                    break;
            }
        }


        private async Task HandleFastestLap(FastestLap fastestLap, uint id)
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

        private async Task HandleRetirement(Retirement retirement, uint id)
        {
            var driver = driverOverrideService.GetDriverBasicDetails(retirement.VehicleIdx);

            if (driver is null)
                return;

            var eventModel = new SessionEvent
            {
                Id = id,
                Driver = driver,
                Title = $"{driver.Name} retired.",
                Description = retirement.Reason.ToString()
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

        private async Task HandleDrsDisabled(DrsDisabled drsDisabled, uint id)
        {
            var eventModel = new SessionEvent
            {
                Id = id,
                Title = "DRS has been disabled.",
                Description = drsDisabled.DRSDisabledReason.ToString()
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

        private async Task HandleDriveThroughPenaltyServed(DriveThroughPenaltyServed driveThroughPenaltyServed, uint id)
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

        private async Task HandleStopGoPenaltyServed(StopGoPenaltyServed stopGoPenaltyServed, uint id)
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

        private async Task HandlePenaltyIssued(Penalty penalty, uint id)
        {

            var driver = driverOverrideService.GetDriverBasicDetails(penalty.VehicleIdx);

            if (driver is null)
                return;

            var eventModel = new SessionEvent
            {
                Id = id,
                Driver = driver,
                Title = $"{driver.Name} has received a {penalty.PenaltyType.ToString()} penalty.",
                Description = penalty.InfringementType.ToString()
            };

            await webSocketBroadcastService.BroadcastAsync(WidgetType.SessionEvents, eventModel);
        }

        private async Task HandleSimpleEvents(Event sessionEvent, uint id)
        {
            var eventMessage = sessionEvent.EventStringCode switch
            {
                    EventCodes.DrsEnabled => "DRS enabled.",
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
