using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Timers;
using Timer = System.Timers.Timer;

namespace F1GameDataParser.GameProfiles.F1Common
{
    public abstract class TelemetryClientBase<THeader, TPacketId> : ITelemetryClient
        where THeader : struct
        where TPacketId : Enum
    {
        private readonly float TIMEOUT = 500.0f;
        private readonly UdpClient _client;
        private IPEndPoint _peerEndPoint;
        private readonly Timer _timeoutTimer;

        public bool Connected { get; private set; }

        public event Action<bool>? OnConnectedStatusChange;

        protected TelemetryClientBase(int port)
        {
            _client = new UdpClient(port);
            _peerEndPoint = new IPEndPoint(IPAddress.Any, port);

            _timeoutTimer = new Timer(TIMEOUT) { AutoReset = true };
            _timeoutTimer.Elapsed += TimeoutEvent;

            Connected = true;
            OnConnectedStatusChange?.Invoke(true);

            //_client.BeginReceive(ReceiveCallback, null);
        }

        private void TimeoutEvent(object? sender, ElapsedEventArgs e)
        {
            Connected = false;
            OnConnectedStatusChange?.Invoke(false);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            GCHandle handle = default;

            try
            {
                byte[] data = _client.EndReceive(result, ref _peerEndPoint);
                handle = GCHandle.Alloc(data, GCHandleType.Pinned);

                var header = (THeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(THeader))!;

                if (ShouldProcessPacket(header, out var packetId))
                {
                    ParseAndDispatchPacket(data, packetId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ReceiveCallback] Error: {ex.Message}");
            }
            finally
            {
                if (handle.IsAllocated)
                    handle.Free();

                _client.BeginReceive(ReceiveCallback, null);
            }
        }

        /// <summary>
        /// Extract the packet ID from the header.
        /// </summary>
        protected abstract bool ShouldProcessPacket(THeader header, out TPacketId packetId);

        /// <summary>
        /// Actual game-specific dispatch logic (based on header & packet type)
        /// </summary>
        protected abstract void ParseAndDispatchPacket(byte[] data, TPacketId packetId);

        protected static T ByteArrayToStruct<T>(byte[] data) where T : struct
        {
            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject())!;
            }
            finally
            {
                handle.Free();
            }
        }
        public virtual void Start()
        {
            _client.BeginReceive(ReceiveCallback, null);
            _timeoutTimer.Start();
        }

        public virtual void Stop()
        {
            _timeoutTimer.Stop();
            _client.Close();
        }
    }
}
