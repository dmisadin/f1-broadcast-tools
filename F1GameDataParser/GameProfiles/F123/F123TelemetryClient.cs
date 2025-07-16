using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F123.Handlers;
using F1GameDataParser.GameProfiles.F123.Packets.CarDamage;
using F1GameDataParser.GameProfiles.F123.Packets.CarStatus;
using F1GameDataParser.GameProfiles.F123.Packets.CarTelemetry;
using F1GameDataParser.GameProfiles.F123.Packets.Event;
using F1GameDataParser.GameProfiles.F123.Packets.FinalClassification;
using F1GameDataParser.GameProfiles.F123.Packets.Lap;
using F1GameDataParser.GameProfiles.F123.Packets.LobbyInfo;
using F1GameDataParser.GameProfiles.F123.Packets.Participants;
using F1GameDataParser.GameProfiles.F123.Packets.Session;
using F1GameDataParser.GameProfiles.F123.Packets.SessionHistory;
using F1GameDataParser.GameProfiles.F1Common;
using F1GameDataParser.GameProfiles.F1Common.Packets;

namespace F1GameDataParser.GameProfiles.F123
{
    public class F123TelemetryClient : TelemetryClientBase<PacketHeader, PacketType>
    {
        private readonly SessionHandler _sessionHandler;
        private readonly LapHandler _lapHandler;
        private readonly EventsHandler _eventsHandler;
        private readonly ParticipantsHandler _participantsHandler;
        private readonly CarTelemetryHandler _carTelemetryHandler;
        private readonly CarStatusHandler _carStatusHandler;
        private readonly FinalClassificationHandler _finalClassificationHandler;
        private readonly CarDamageHandler _carDamageHandler;
        private readonly LobbyInfoHandler _lobbyInfoHandler;
        private readonly SessionHistoryHandler _sessionHistoryHandler;

        public F123TelemetryClient(SessionHandler sessionHandler, 
                                    LapHandler lapHandler,
                                    EventsHandler eventsHandler,
                                    ParticipantsHandler participantsHandler,
                                    CarTelemetryHandler carTelemetryHandler,
                                    CarStatusHandler carStatusHandler,
                                    FinalClassificationHandler finalClassificationHandler,
                                    CarDamageHandler carDamageHandler,
                                    LobbyInfoHandler lobbyInfoHandler,
                                    SessionHistoryHandler sessionHistoryHandler)
            : base(20777)
        {
            _sessionHandler = sessionHandler;
            _lapHandler = lapHandler;
            _eventsHandler = eventsHandler;
            _participantsHandler = participantsHandler;
            _carTelemetryHandler = carTelemetryHandler;
            _carStatusHandler = carStatusHandler;
            _finalClassificationHandler = finalClassificationHandler;
            _carDamageHandler = carDamageHandler;
            _lobbyInfoHandler = lobbyInfoHandler;
            _sessionHistoryHandler = sessionHistoryHandler;

            Console.WriteLine("Listening for F1 23...");
        }

        protected override bool ShouldProcessPacket(PacketHeader header, out PacketType packetId)
        {
            packetId = header.packetId;
            return true;
        }

        protected override void ParseAndDispatchPacket(byte[] data, PacketType packetId)
        {
            switch (packetId)
            {
                case PacketType.SESSION:
                    var session = ByteArrayToStruct<SessionPacket>(data);
                    _sessionHandler.OnReceived(session);
                    break;
                case PacketType.LAP_DATA:
                    var lapData = ByteArrayToStruct<LapPacket>(data);
                    _lapHandler.OnReceived(lapData);
                    break;
                case PacketType.EVENT:
                    var evt = ByteArrayToStruct<EventPacket>(data);
                    _eventsHandler.OnReceived(evt);
                    break;
                case PacketType.PARTICIPANTS:
                    var participants = ByteArrayToStruct<ParticipantsPacket>(data);
                    _participantsHandler.OnReceived(participants);
                    break;
                case PacketType.CAR_TELEMETRY:
                    var carTelemetry = ByteArrayToStruct<CarTelemetryPacket>(data);
                    _carTelemetryHandler.OnReceived(carTelemetry);
                    break;
                case PacketType.CAR_STATUS:
                    var carStatus = ByteArrayToStruct<CarStatusPacket>(data);
                    _carStatusHandler.OnReceived(carStatus);
                    break;
                case PacketType.FINAL_CLASSIFICATION:
                    var finalClassification = ByteArrayToStruct<FinalClassificationPacket>(data);
                    _finalClassificationHandler.OnReceived(finalClassification);
                    break;
                case PacketType.CAR_DAMAGE:
                    var carDamage = ByteArrayToStruct<CarDamagePacket>(data);
                    _carDamageHandler.OnReceived(carDamage);
                    break;
                case PacketType.LOBBY_INFO:
                    var lobbyInfo = ByteArrayToStruct<LobbyInfoPacket>(data);
                    _lobbyInfoHandler.OnReceived(lobbyInfo);
                    break;
                case PacketType.SESSION_HISTORY:
                    var sessionHistory = ByteArrayToStruct<SessionHistoryPacket>(data);
                    _sessionHistoryHandler.OnReceived(sessionHistory);
                    break;
            }
        }
    }
}