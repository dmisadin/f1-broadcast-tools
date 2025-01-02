namespace F1GameDataParser.Enums
{
    /// <summary>
    /// These flags are used in the telemetry packet to determine if any buttons are being held on the controlling device.
    /// If the value below logical ANDed with the button status is set then the corresponding button is being held.
    /// </summary>
    public enum Button : uint
    {
        CROSS_OR_A = 0x00000001,
        TRIANGLE_OR_Y = 0x00000002,
        CIRCLE_OR_B = 0x00000004,
        SQUARE_OR_X = 0x00000008,
        DPAD_LEFT = 0x00000010,
        DPAD_RIGHT = 0x00000020,
        DPAD_UP = 0x00000040,
        DPAD_DOWN = 0x00000080,
        OPTIONS_OR_MENU = 0x00000100,
        L1_OR_LB = 0x00000200,
        R1_OR_RB = 0x00000400,
        L2_OR_LT = 0x00000800,
        R2_OR_RT = 0x00001000,
        LEFT_STICK_CLICK = 0x00002000,
        RIGHT_STICK_CLICK = 0x00004000,
        RIGHT_STICK_LEFT = 0x00008000,
        RIGHT_STICK_RIGHT = 0x00010000,
        RIGHT_STICK_UP = 0x00020000,
        RIGHT_STICK_DOWN = 0x00040000,
        SPECIAL = 0x00080000,
        UDP_ACTION_1 = 0x00100000,
        UDP_ACTION_2 = 0x00200000,
        UDP_ACTION_3 = 0x00400000,
        UDP_ACTION_4 = 0x00800000,
        UDP_ACTION_5 = 0x01000000,
        UDP_ACTION_6 = 0x02000000,
        UDP_ACTION_7 = 0x04000000,
        UDP_ACTION_8 = 0x08000000,
        UDP_ACTION_9 = 0x10000000,
        UDP_ACTION_10 = 0x20000000,
        UDP_ACTION_11 = 0x40000000,
        UDP_ACTION_12 = 0x80000000
    }
}
