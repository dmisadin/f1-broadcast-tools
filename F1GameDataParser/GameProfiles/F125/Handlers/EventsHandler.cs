using F1GameDataParser.GameProfiles.F125.ModelFactories;
using F1GameDataParser.GameProfiles.F125.Packets.Event;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.Event;
using F1GameDataParser.Services;

namespace F1GameDataParser.GameProfiles.F125.Handlers
{
    // This should be a stateless handler, just send data to client
    public class EventsHandler : GenericHandler<EventPacket, Event>
    {
        private readonly SessionEventService sessionEventService;

        public EventsHandler(SessionEventService sessionEventService) 
        { 
            this.sessionEventService = sessionEventService;
        }

        protected override IModelFactory<EventPacket, Event> ModelFactory => new EventModelFactory();

        public override void OnReceived(EventPacket packet)
        {
            var eventModel = ModelFactory.ToModel(packet);
            if (!EventCodes.EnabledEvents.Contains(eventModel.EventStringCode))
                return;

            sessionEventService.HandleSessionEvents(eventModel);
        }
    }
}
