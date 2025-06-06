﻿using F1GameDataParser.Mapping.ModelFactories;
using F1GameDataParser.Models.SessionHistory;
using F1GameDataParser.Packets.SessionHistory;
using F1GameDataParser.State;

namespace F1GameDataParser.Handlers
{
    public class SessionHistoryHandler : GenericHandler<SessionHistoryPacket, SessionHistory>
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly SessionHistoryState _sessionHistoryState;

        public SessionHistoryHandler(TelemetryClient telemetryClient,
                                     SessionHistoryState sessionHistoryState)
        {
            _telemetryClient = telemetryClient;
            _sessionHistoryState = sessionHistoryState;

            _telemetryClient.OnSessionHistoryReceive += OnRecieved;
        }

        protected override IModelFactory<SessionHistoryPacket, SessionHistory> ModelFactory => new SessionHistoryModelFactory();

        protected override void OnRecieved(SessionHistoryPacket packet)
        {
            var sessionHistoryModel = ModelFactory.ToModel(packet);

            if (sessionHistoryModel != null)
                _sessionHistoryState.Update(sessionHistoryModel);
            /*
                This packet contains lap times and tyre usage for the session. This packet works slightly differently
                to other packets. To reduce CPU and bandwidth, each packet relates to a specific vehicle and is
                sent every 1/20 s, and the vehicle being sent is cycled through. Therefore in a 20 car race you
                should receive an update for each vehicle at least once per second.
            */
        }
    }
}
