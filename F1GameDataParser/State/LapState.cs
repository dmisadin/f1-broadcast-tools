﻿using F1GameDataParser.Enums;
using F1GameDataParser.Models.Lap;

namespace F1GameDataParser.State
{
    public class LapState : ListStateBase<LapDetails>
    {

        private Queue<byte> UnservedPenalties { get; set; } = new Queue<byte>();
        private bool IsServingPenalty { get; set; }
        public object Lock => _lock;

        protected override void OnModelMerged(int key, LapDetails existingModel, LapDetails newModel)
        {
            this.CheckAndServeTimePenalites(existingModel);
        }


        public void CheckAndServeTimePenalites(LapDetails existingModel)
        {
            if (existingModel.PitStatus != PitStatus.None)
                this.RemoveServedPenalty(existingModel);
            else
                existingModel.IsServingPenalty = false;
        }

        public void RemoveServedPenalty(LapDetails driverLapData)
        {
            if (driverLapData.UnservedPenalties.Count() == 0
                || driverLapData.NumUnservedDriveThroughPens != 0
                || driverLapData.NumUnservedStopGoPens != 0
                || driverLapData.IsServingPenalty)
                return;
            // Speeding in the pit lane could become an edge case
            try
            {
                driverLapData.UnservedPenalties.Dequeue();
                driverLapData.IsServingPenalty = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error removing penalty from queue: " + e.Message);
            }
        }
    }
}
