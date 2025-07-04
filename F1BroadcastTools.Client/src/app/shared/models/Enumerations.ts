export enum ResultStatus {
    Invalid = 0,
    Inactive,
    Active,
    Finished,
    DNF,
    DSQ,
    NC,
    Retired
}

export enum Team {
    Mercedes,
    Ferrari,
    RedBullRacing,
    Williams,
    AstonMartin,
    Alpine,
    AlphaTauri,
    Haas,
    McLaren,
    AlfaRomeo
}

export enum SafetyCarStatus {
    None,
    Full,
    Virtual,
    FormationLap
}

export enum Nationality {
    OTHER = 0,
    US = 1,
    AR = 2,
    AU = 3,
    AT = 4,
    AZ = 5,
    BH = 6,
    BE = 7,
    BO = 8,
    BR = 9,
    GR = 10,
    BG = 11,
    CM = 12,
    CA = 13,
    CL = 14,
    CN = 15,
    CO = 16,
    CR = 17,
    HR = 18,
    CY = 19,
    CZ = 20,
    DK = 21,
    NL = 22,
    EC = 23,
    GB_ENG = 24,
    AE = 25,
    EE = 26,
    FI = 27,
    FR = 28,
    DE = 29,
    GH = 30,
    GK = 31,
    GT = 32,
    HN = 33,
    HK = 34,
    HU = 35,
    IS = 36,
    IN = 37,
    ID = 38,
    IE = 39,
    IL = 40,
    IT = 41,
    JM = 42,
    JP = 43,
    JO = 44,
    KW = 45,
    LV = 46,
    LB = 47,
    LT = 48,
    LU = 49,
    MY = 50,
    MT = 51,
    MX = 52,
    MC = 53,
    NZ = 54,
    NI = 55,
    GB_NIR = 56,
    NO = 57,
    OM = 58,
    PK = 59,
    PA = 60,
    PY = 61,
    PE = 62,
    PL = 63,
    PT = 64,
    QA = 65,
    RO = 66,
    RU = 67,
    SV = 68,
    SA = 69,
    GB_SCT = 70,
    RS = 71,
    SG = 72,
    SK = 73,
    SI = 74,
    KR = 75,
    ZA = 76,
    ES = 77,
    SE = 78,
    CH = 79,
    TH = 80,
    TR = 81,
    UY = 82,
    UA = 83,
    VE = 84,
    BB = 85,
    GB_WLS = 86,
    VN = 87
}

export enum AdditionalInfo {
    None = 0,
    Warnings = 1 << 0,
    Penalties = 1 << 1,
    NumPitStops = 1 << 2,
    PositionsGained = 1 << 3
}

export enum Game {
    F123 = 23,
    F125 = 25
}