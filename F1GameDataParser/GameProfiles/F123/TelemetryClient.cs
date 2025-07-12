using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F123.Packets;
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
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Timer = System.Timers.Timer;

namespace F1GameDataParser.GameProfiles.F123
{
    public class TelemetryClient
    {
        private const float TIMEOUT = 500.0f;

        private UdpClient _client;
        private IPEndPoint? _peerEndPoint;
        private Timer _timeoutTimer;
        private GCHandle _handle;
        private PacketType[] _defaultPackets = new PacketType[] {
        PacketType.LAP_DATA, PacketType.EVENT, PacketType.SESSION,
        PacketType.PARTICIPANTS, PacketType.CAR_TELEMETRY, PacketType.CAR_STATUS,
        PacketType.FINAL_CLASSIFICATION, PacketType.CAR_DAMAGE, PacketType.SESSION_HISTORY,
        PacketType.LOBBY_INFO
        };

        public bool Connected { get; private set; }
        public PacketType[] EnabledPackets { get; private set; }

        public delegate void ConnectedStatusChangeDelegate(bool connected);
        public event ConnectedStatusChangeDelegate? OnConnectedStatusChange;

        public event Action<LapPacket>? OnLapReceive;
        public event Action<EventPacket>? OnEventReceive;
        public event Action<ParticipantsPacket>? OnParticipantsReceive;
        public event Action<SessionPacket>? OnSessionReceive;
        public event Action<CarTelemetryPacket>? OnCarTelemetryReceive;
        public event Action<CarStatusPacket>? OnCarStatusReceive;
        public event Action<FinalClassificationPacket>? OnFinalClassificationReceive;
        public event Action<CarDamagePacket>? OnCarDamageReceive;
        public event Action<SessionHistoryPacket>? OnSessionHistoryReceive;
        public event Action<LobbyInfoPacket>? OnLobbyInfoReceive;

        public TelemetryClient(int port, PacketType[]? enabledPackets = null)
        {
            _client = new UdpClient(port);
            _peerEndPoint = new IPEndPoint(IPAddress.Any, port);

            _timeoutTimer = new Timer(TIMEOUT)
            {
                AutoReset = true
            };
            _timeoutTimer.Elapsed += TimeoutEvent;

            Connected = true;
            OnConnectedStatusChange?.Invoke(true);

            EnabledPackets = enabledPackets ?? _defaultPackets;

            Console.WriteLine("Listening for F1 23...");
            _client.BeginReceive(new AsyncCallback(ReceiveCallback), null);
        }

        private void TimeoutEvent(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Connected = false;
            OnConnectedStatusChange?.Invoke(false);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                byte[] data = _client.EndReceive(result, ref _peerEndPoint);

                _handle = GCHandle.Alloc(data, GCHandleType.Pinned);

                PacketHeader header = (PacketHeader)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(PacketHeader));

                if (EnabledPackets.Contains(header.packetId))
                {
                    switch (header.packetId)
                    {
                        /*
                        case PacketType.MOTION:
                            MotionPacket motionPacket = (MotionPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(MotionPacket));
                            OnMotionDataReceive?.Invoke(motionPacket);
                            break;
                        */
                        case PacketType.LAP_DATA:
                            LapPacket lapDataPacket = (LapPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(LapPacket));
                            OnLapReceive?.Invoke(lapDataPacket);
                            break;
                        case PacketType.EVENT:
                            EventPacket eventPacket = (EventPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(EventPacket));
                            OnEventReceive?.Invoke(eventPacket);
                            break;
                        case PacketType.SESSION:
                            SessionPacket sessionPacket = (SessionPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(SessionPacket));
                            OnSessionReceive?.Invoke(sessionPacket);
                            break;
                        case PacketType.PARTICIPANTS:
                            ParticipantsPacket participantsPacket = (ParticipantsPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(ParticipantsPacket));
                            OnParticipantsReceive?.Invoke(participantsPacket);
                            break;
                        /*
                        case PacketType.CAR_SETUPS:
                            CarSetupPacket carSetupPacket = (CarSetupPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(CarSetupPacket));
                            OnCarSetupDataReceive?.Invoke(carSetupPacket);
                            break;
                        */
                        case PacketType.CAR_TELEMETRY:
                            CarTelemetryPacket carTelemetryPacket = (CarTelemetryPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(CarTelemetryPacket));
                            OnCarTelemetryReceive?.Invoke(carTelemetryPacket);
                            break;
                        case PacketType.CAR_STATUS:
                            CarStatusPacket carStatusPacket = (CarStatusPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(CarStatusPacket));
                            OnCarStatusReceive?.Invoke(carStatusPacket);
                            break;
                        case PacketType.FINAL_CLASSIFICATION:
                            FinalClassificationPacket finalClassificationPacket = (FinalClassificationPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(FinalClassificationPacket));
                            OnFinalClassificationReceive?.Invoke(finalClassificationPacket);
                            break;
                        case PacketType.LOBBY_INFO:
                            LobbyInfoPacket lobbyInfoPacket = (LobbyInfoPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(LobbyInfoPacket));
                            OnLobbyInfoReceive?.Invoke(lobbyInfoPacket);
                            break;
                        case PacketType.CAR_DAMAGE:
                            CarDamagePacket carDamagePacket = (CarDamagePacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(CarDamagePacket));
                            OnCarDamageReceive?.Invoke(carDamagePacket);
                            break;
                        case PacketType.SESSION_HISTORY:
                            SessionHistoryPacket sessionHistoryPacket = (SessionHistoryPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(SessionHistoryPacket));
                            OnSessionHistoryReceive?.Invoke(sessionHistoryPacket);
                            break;
                            /*
                            case PacketType.TYRE_SET:
                                TyreSetPacket tyreSetPacket = (TyreSetPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(TyreSetPacket));
                                OnTyreSetDataReceive?.Invoke(tyreSetPacket);
                                break;
                            case PacketType.MOTION_EX:
                                MotionExPacket motionExPacket = (MotionExPacket)Marshal.PtrToStructure(_handle.AddrOfPinnedObject(), typeof(MotionExPacket));
                                OnMotionExDataReceive?.Invoke(motionExPacket);
                                break;
                            */
                    }
                }
            }
            catch
            {
                // Ignore
            }
            finally
            {
                if (_handle.IsAllocated)
                {
                    _handle.Free();
                }

                _client?.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            }
        }
    }
}