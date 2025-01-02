using F1GameDataParser.Mapping.ModelFactories;
using F1GameDataParser.Models.Event;
using F1GameDataParser.Packets.Event;

namespace F1GameDataParser.Handlers
{
    // This should be a stateless handler, just send data to client
    public class EventsHandler : GenericHandler<EventPacket, Event>
    {
        private readonly TelemetryClient _telemetryClient;

        public EventsHandler(TelemetryClient telemetryClient) 
        { 
            _telemetryClient = telemetryClient;

            _telemetryClient.OnEventReceive += OnRecieved;
        }

        protected override IModelFactory<EventPacket, Event> ModelFactory => new EventModelFactory();

        protected override void OnRecieved(EventPacket packet)
        {
            var eventModel = ModelFactory.ToModel(packet);
            if (EventCodes.EnabledEvents.Contains(eventModel.EventStringCode))
            {
                Console.WriteLine(eventModel.EventStringCode);
            }
        }
    }
}
