using F1GameDataParser.Enums;
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
        public event Action<SessionPacket>? OnSessionReceive;
        public event Action<LapPacket>? OnLapReceive;
        public event Action<EventPacket>? OnEventReceive;
        public event Action<ParticipantsPacket>? OnParticipantsReceive;
        public event Action<CarTelemetryPacket>? OnCarTelemetryReceive;
        public event Action<CarStatusPacket>? OnCarStatusReceive;
        public event Action<FinalClassificationPacket>? OnFinalClassificationReceive;
        public event Action<CarDamagePacket>? OnCarDamageReceive;
        public event Action<LobbyInfoPacket>? OnLobbyInfoReceive;
        public event Action<SessionHistoryPacket>? OnSessionHistoryReceive;

        public F123TelemetryClient(int port = 20777)
            : base(port)
        {
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
                    OnSessionReceive?.Invoke(session);
                    break;
                case PacketType.LAP_DATA:
                    var lapData = ByteArrayToStruct<LapPacket>(data);
                    OnLapReceive?.Invoke(lapData);
                    break;
                case PacketType.EVENT:
                    var evt = ByteArrayToStruct<EventPacket>(data);
                    OnEventReceive?.Invoke(evt);
                    break;
                case PacketType.PARTICIPANTS:
                    var participants = ByteArrayToStruct<ParticipantsPacket>(data);
                    OnParticipantsReceive?.Invoke(participants);
                    break;
                case PacketType.CAR_TELEMETRY:
                    var carTelemetry = ByteArrayToStruct<CarTelemetryPacket>(data);
                    OnCarTelemetryReceive?.Invoke(carTelemetry);
                    break;
                case PacketType.CAR_STATUS:
                    var carStatus = ByteArrayToStruct<CarStatusPacket>(data);
                    OnCarStatusReceive?.Invoke(carStatus);
                    break;
                case PacketType.FINAL_CLASSIFICATION:
                    var finalClassification = ByteArrayToStruct<FinalClassificationPacket>(data);
                    OnFinalClassificationReceive?.Invoke(finalClassification);
                    break;
                case PacketType.CAR_DAMAGE:
                    var carDamage = ByteArrayToStruct<CarDamagePacket>(data);
                    OnCarDamageReceive?.Invoke(carDamage);
                    break;
                case PacketType.LOBBY_INFO:
                    var lobbyInfo = ByteArrayToStruct<LobbyInfoPacket>(data);
                    OnLobbyInfoReceive?.Invoke(lobbyInfo);
                    break;
                case PacketType.SESSION_HISTORY:
                    var sessionHistory = ByteArrayToStruct<SessionHistoryPacket>(data);
                    OnSessionHistoryReceive?.Invoke(sessionHistory);
                    break;
            }
        }
    }
}