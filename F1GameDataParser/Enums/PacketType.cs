namespace F1GameDataParser.Enums
{

    /// <summary>
    /// Identifiers for the packets
    /// </summary>
    public enum PacketType : byte
    {
        /// <summary>
        /// Contains all motion data for player’s car – only sent while player is in control
        /// </summary>
        MOTION = 0,
        /// <summary>
        /// Data about the session – track, time left
        /// </summary>
        SESSION = 1,
        /// <summary>
        /// Data about all the lap times of cars in the session
        /// </summary>
        LAP_DATA = 2,
        /// <summary>
        /// Various notable events that happen during a session
        /// </summary>
        EVENT = 3,
        /// <summary>
        /// List of participants in the session, mostly relevant for multiplayer
        /// </summary>
        PARTICIPANTS = 4,
        /// <summary>
        /// Packet detailing car setups for cars in the race
        /// </summary>
        CAR_SETUPS = 5,
        /// <summary>
        /// Telemetry data for all cars
        /// </summary>
        CAR_TELEMETRY = 6,
        /// <summary>
        /// Status data for all cars
        /// </summary>
        CAR_STATUS = 7,
        /// <summary>
        /// Final classification confirmation at the end of a race
        /// </summary>
        FINAL_CLASSIFICATION = 8,
        /// <summary>
        /// Information about players in a multiplayer lobby
        /// </summary>
        LOBBY_INFO = 9,
        /// <summary>
        /// Damage status for all cars
        /// </summary>
        CAR_DAMAGE = 10,
        /// <summary>
        /// Lap and tyre data for session
        /// </summary>
        SESSION_HISTORY = 11,
        /// <summary>
        /// Extended tyre set data
        /// </summary>
        TYRE_SET = 12,
        /// <summary>
        /// Extended motion data for player car
        /// </summary>
        MOTION_EX = 13, // Added in F1 24
        TIME_TRIAL = 14, // Added in F1 24
        LAP_POSITIONS = 15 // Added in F1 25
    }
}
