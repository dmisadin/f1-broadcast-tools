using F1GameDataParser.Enums;

namespace F1GameDataParser.Services.GameSwitch
{
    public static class GameDetector
    {
        private const int SAMPLE_EVERY_N_PACKETS = 200;
        private static readonly TimeSpan MIN_DETECTION_INTERVAL = TimeSpan.FromSeconds(10);

        private static GameManager? gameManager;

        private static long packetCounter;
        private static DateTime lastCheckUtc = DateTime.MinValue;

        /// <summary>
        /// Initializes the detector with a GameManager instance and starts with F125 as default.
        /// </summary>
        public static void Initialize(GameManager manager)
        {
            gameManager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>
        /// Start detection by defaulting to F125 and switching immediately.
        /// </summary>
        public static void Start()
        {
            EnsureInitialized();
            gameManager!.SwitchGame(PacketFormat.F125);
        }

        /// <summary>
        /// Process the PacketFormat from any received header.
        /// </summary>
        public static bool ProcessPacketFormat(PacketFormat packetFormat)
        {
            EnsureInitialized();

            packetCounter++;

            if (packetCounter < SAMPLE_EVERY_N_PACKETS)
                return false;

            var now = DateTime.UtcNow;

            if (now - lastCheckUtc < MIN_DETECTION_INTERVAL)
                return false;

            lastCheckUtc = now;

            if (gameManager!.SwitchGame(packetFormat))
            {
                packetCounter = 0;
                return true;
            }

            return false;
        }

        private static void EnsureInitialized()
        {
            if (gameManager == null)
                throw new InvalidOperationException("GameDetector has not been initialized with a GameManager.");
        }
    }
}
