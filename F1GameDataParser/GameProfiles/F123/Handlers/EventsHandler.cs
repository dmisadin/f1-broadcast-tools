using F1GameDataParser.GameProfiles.F123.ModelFactories;
using F1GameDataParser.GameProfiles.F123.Packets.Event;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.Models.Event;
using F1GameDataParser.Services;

namespace F1GameDataParser.GameProfiles.F123.Handlers
{
    // This should be a stateless handler, just send data to client
    public class EventsHandler : GenericHandler<EventPacket, Event>
    {
        private readonly SessionEventService eventService;

        public EventsHandler(SessionEventService eventService) 
        { 
            this.eventService = eventService;
        }

        protected override IModelFactory<EventPacket, Event> ModelFactory => new EventModelFactory();

        public override void OnReceived(EventPacket packet)
        {
            var eventModel = ModelFactory.ToModel(packet);
            if (!EventCodes.EnabledEvents.Contains(eventModel.EventStringCode))
                return;

            switch (eventModel.EventDetails.Details)
            {
                case Models.Event.Penalty penalty:
                    eventService.HandlePenalty(penalty, eventModel.Header.GameYear);
                    break;
                default:
                    break;
            }
        }
    }
}
