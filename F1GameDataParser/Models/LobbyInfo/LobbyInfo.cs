﻿namespace F1GameDataParser.Models.LobbyInfo
{
    public class LobbyInfo : MergeableBase<LobbyInfo>
    {
        public byte NumPlayers { get; set; }
        public LobbyInfoDetails[] Details { get; set; } = new LobbyInfoDetails[Sizes.MaxPlayers];
    }
}
