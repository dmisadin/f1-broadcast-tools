﻿using F1GameDataParser.Models.DriverOverride;

namespace F1GameDataParser.State
{
    public class DriverOverrideState : ListStateBase<DriverOverride>
    {
        // UPDATE() is missing logic when override is removed (press X in ng-select)
        public override List<DriverOverride> GetAll()
        {
            return this.State.Select((model, key) => new DriverOverride
            {
                Id = key,
                PlayerId = model.Value.PlayerId,
                Player = model.Value.Player
            }).ToList(); // TODO: triba napravit dohvacanje svih entryja 
        }
    }
}
