using F1GameDataParser.Mapping.ModelFactories;
using F1GameDataParser.Models.Event;
using F1GameDataParser.Packets.Event;
using F1GameDataParser.Services;

namespace F1GameDataParser.Handlers
{
    // This should be a stateless handler, just send data to client
    public class EventsHandler : GenericHandler<EventPacket, Event>
    {
        private readonly TelemetryClient telemetryClient;
        private readonly EventService eventService;

        public EventsHandler(TelemetryClient telemetryClient,
                            EventService eventService) 
        { 
            this.telemetryClient = telemetryClient;
            this.eventService = eventService;

            this.telemetryClient.OnEventReceive += OnRecieved;
        }

        protected override IModelFactory<EventPacket, Event> ModelFactory => new EventModelFactory();

        protected override void OnRecieved(EventPacket packet)
        {
            var eventModel = ModelFactory.ToModel(packet);
            if (!EventCodes.EnabledEvents.Contains(eventModel.EventStringCode))
                return;

            switch (eventModel.EventDetails.Details)
            {
                case Models.Event.Penalty penalty:
                    this.eventService.HandlePenalty(penalty);
                    break;
                default:
                    break;
            }
        }
    }
}
