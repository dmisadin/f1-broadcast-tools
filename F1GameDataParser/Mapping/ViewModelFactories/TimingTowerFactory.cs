﻿using F1GameDataParser.Enums;
using F1GameDataParser.GameProfiles.F123;
using F1GameDataParser.Models;
using F1GameDataParser.State;
using F1GameDataParser.Utility;
using F1GameDataParser.ViewModels.TimingTower;

namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public class TimingTowerFactory : ViewModelFactoryBase<TimingTower>
    {
        private readonly LapState lapState;
        private readonly SessionState sessionState;
        private readonly ParticipantsState participantsState;
        private readonly CarStatusState carStatusState;
        private readonly SessionHistoryState sessionHistoryState;
        private readonly DriverOverrideState driverOverrideState;

        public TimingTowerFactory(LapState lapState,
            SessionState sessionState,
            ParticipantsState participantsState,
            CarStatusState carStatusState,
            SessionHistoryState sessionHistoryState,
            DriverOverrideState driverOverrideState)
        {
            this.lapState = lapState;
            this.sessionState = sessionState;
            this.participantsState = participantsState;
            this.carStatusState = carStatusState;
            this.sessionHistoryState = sessionHistoryState;
            this.driverOverrideState = driverOverrideState;
        }

        public override TimingTower? Generate()
        {
            if (lapState.State == null
                || lapState.State.Count() == 0
                || sessionState.State == null 
                || participantsState.State == null
                || carStatusState.State == null 
                || sessionHistoryState.State == null)
                return null;

            var firstPlaceDriver = lapState.State.Where(detail => detail.Value.CarPosition == 1).FirstOrDefault().Value;
            var currentLap = firstPlaceDriver?.CurrentLapNum ?? 0;
            var totalLaps = sessionState.State.TotalLaps;
            var currentLapDistance = this.CurrentLapDistanceDonePercentage(firstPlaceDriver?.LapDistance ?? -1, sessionState.State.TrackLength);

            var driverTimingDetails = new DriverTimingDetails[22];
            var fastestLapVehicleIdx = GetFastestLapVehicleIndex();

            for (int i = 0; i < Sizes.MaxPlayers; i++)
            {
                // TO DO: Find a way to skip some, maybe not maxplayers
                if (!lapState.State.TryGetValue(i, out var lapDetails))
                    continue;
                var participantDetails = participantsState.State.ParticipantList[i];
                var carStatusDetails = carStatusState.State.Details[i];
                var driverOverride = driverOverrideState.GetModel(i);
                TeamDetails teamDetails;

                driverTimingDetails[i] = new DriverTimingDetails
                {
                    VehicleIdx = i,
                    Position = lapDetails.CarPosition,
                    TeamId = participantDetails.TeamId, //ToString()
                    TeamDetails = Teams.AllTeams.TryGetValue(participantDetails.TeamId, out teamDetails) ? teamDetails : null,
                    Name = driverOverride?.Player.Name ?? participantDetails.Name,
                    TyreAge = carStatusDetails.TyresAgeLaps,
                    VisualTyreCompound = carStatusDetails.VisualTyreCompound.ToString(),
                    Gap = GetGapOrResultStatus(lapDetails.DeltaToCarInFrontInMS, lapDetails.CarPosition, lapDetails.ResultStatus),
                    ResultStatus = lapDetails.ResultStatus,
                    Penalties = lapDetails.Penalties + (lapDetails.UnservedPenalties?.Sum(p => p) ?? 0),
                    Warnings = (byte)(lapDetails.CornerCuttingWarnings % 3),
                    HasFastestLap = i == fastestLapVehicleIdx,
                    IsInPits = lapDetails.PitStatus != PitStatus.None,
                    NumPitStops = lapDetails.NumPitStops,
                    PositionsGained = lapDetails.GridPosition - lapDetails.CarPosition
                };
            }

            return new TimingTower
            {
                CurrentLap = currentLap,
                TotalLaps = totalLaps,
                SafetyCarStatus = sessionState.State.SafetyCarStatus,
                SectorYellowFlags = GetFIAFlags(),
                ShowAdditionalInfo = ShouldShowAdditionalInfo(currentLap, totalLaps, currentLapDistance),
                DriverTimingDetails = driverTimingDetails.Where(x => x.Position > 0).OrderBy(x => x.Position).ToArray(),
                SpectatorCarIdx = sessionState.State.SpectatorCarIndex
            };
        }

        private uint GetFastestLapVehicleIndex()
        {
            if (sessionHistoryState.State == null)
                return 255;

            var fastestDriverSessionHistory = sessionHistoryState.State
                                                .Where(driver => driver != null
                                                    && driver.LapHistoryDetails != null
                                                    && driver.BestLapTimeLapNum < driver.LapHistoryDetails.Count()
                                                    && driver.LapHistoryDetails.ElementAt(driver.BestLapTimeLapNum).LapTimeInMS > 0)
                                                .OrderBy(driver => driver.LapHistoryDetails.ElementAt(driver.BestLapTimeLapNum).LapTimeInMS)
                                                .FirstOrDefault(); // Kinda unsafe to use ElementAt...

            if (fastestDriverSessionHistory == null)
                return 255;

            return fastestDriverSessionHistory.CarIdx;
        }

        private string GetGapOrResultStatus(long gap, int position, ResultStatus resultStatus = ResultStatus.Active)
        {
            if (resultStatus == ResultStatus.Active || resultStatus == ResultStatus.Finished)
            {
                if (position == 1)
                    return "Interval"; // TO DO: Implement Leader Gap
                else if (gap == 0)
                    return "-";
                return  $"+{TimeUtility.MillisecondsToGap(gap)}";
            }

            return resultStatus.GetShortLabel();
        }

        private IEnumerable<bool> GetFIAFlags()
        {
            if (sessionState.State == null)
                return new bool[3];

            var marshalZones = sessionState.State.MarshalZones;
            var numberOfZones = sessionState.State.NumMarshalZones;

            // approximation of sectors:
            int s2index = (int)Math.Round(numberOfZones / 3.0f);
            int s3index = (int)Math.Round(numberOfZones * 0.67f);

            float sector2Start = marshalZones.ElementAt(s2index).ZoneStart;
            float sector3Start = marshalZones.ElementAt(s3index).ZoneStart;

            bool isSector1Yellow = marshalZones.Any(zone => zone.ZoneStart < sector2Start && zone.ZoneFlag == ZoneFlag.Yellow);
            bool isSector2Yellow = marshalZones.Any(zone => zone.ZoneStart >= sector2Start && zone.ZoneStart < sector3Start && zone.ZoneFlag == ZoneFlag.Yellow);
            bool isSector3Yellow = marshalZones.Any(zone => zone.ZoneStart >= sector3Start && zone.ZoneFlag == ZoneFlag.Yellow);

            return [isSector1Yellow, isSector2Yellow, isSector3Yellow];
        }

        private AdditionalInfoType ShouldShowAdditionalInfo(byte currentLap, byte totalLaps, float currentLapDistancePercentage)
        {
            AdditionalInfoType showAdditionalInfo = AdditionalInfoType.None;
            bool hasLeaderCrossedHalfOfLap = currentLapDistancePercentage > 0.5;

            if (hasLeaderCrossedHalfOfLap || currentLap >= totalLaps)
                return showAdditionalInfo;

            if (currentLap % 2 == 0 && this.DoesAnyDriverHaveWarnings())
                showAdditionalInfo |= AdditionalInfoType.Warnings;
            else
                showAdditionalInfo &= ~AdditionalInfoType.Warnings;

            if (currentLap % 3 == 0 && this.DoesAnyDriverHavePenalties())
                showAdditionalInfo |= AdditionalInfoType.Penalties;
            else
                showAdditionalInfo &= ~AdditionalInfoType.Penalties;

            if (currentLap % 5 == 0 && currentLap > totalLaps / 3)
                showAdditionalInfo |= AdditionalInfoType.NumPitStops;
            else
                showAdditionalInfo &= ~AdditionalInfoType.NumPitStops;

            if (currentLap == 2 || currentLap % 7 == 0)
                showAdditionalInfo |= AdditionalInfoType.PositionsGained;
            else
                showAdditionalInfo &= ~AdditionalInfoType.PositionsGained;

            return showAdditionalInfo;
        }
        
        private float CurrentLapDistanceDonePercentage(float lapDistance, float trackLength)
        {
            if (lapDistance < 0)
                return 0;
            return lapDistance / trackLength;
        }

        private bool DoesAnyDriverHavePenalties ()
        {
            return this.lapState.State?.Any(l => l.Value.ResultStatus == ResultStatus.Active && l.Value.Penalties > 0) ?? false;
        }

        private bool DoesAnyDriverHaveWarnings()
        {
            return this.lapState.State?.Any(l => l.Value.ResultStatus == ResultStatus.Active && l.Value.CornerCuttingWarnings > 0) ?? false;
        }
    }
}
