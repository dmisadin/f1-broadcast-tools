﻿using F1GameDataParser.Enums;
using F1GameDataParser.Models.SessionHistory;
using F1GameDataParser.Packets.SessionHistory;
using System.Linq.Expressions;

namespace F1GameDataParser.Mapping.ModelFactories
{
    public class SessionHistoryModelFactory : ModelFactoryBase<SessionHistoryPacket, SessionHistory>
    {
        public override Expression<Func<SessionHistoryPacket, SessionHistory>> ToModelExpression()
        {
            return packet => new SessionHistory
            {
                Header = HeaderExpressionCompiled.Invoke(packet.header),
                CarIdx = packet.carIdx,
                NumTyreStints = packet.numTyreStings,
                BestLapTimeLapNum = packet.bestLapTimeLapNum,
                BestSector1LapNum = packet.bestSector1LapNum,
                BestSector2LapNum = packet.bestSector2LapNum,
                BestSector3LapNum = packet.bestSector3LapNum,
                LapHistoryDetails = packet.lapHistoryData.Where(lap => lap.lapTimeInMS > 0)
                                            .Select(lap => new LapHistoryDetails
                                            {
                                                LapTimeInMS = lap.lapTimeInMS,
                                                Sector1TimeInMS = lap.sector1TimeInMS,
                                                Sector1TimeMinutes = lap.sector1TimeMinutes,
                                                Sector2TimeInMS = lap.sector2TimeInMS,
                                                Sector2TimeMinutes = lap.sector2TimeMinutes,
                                                Sector3TimeInMS = lap.sector3TimeInMS,
                                                Sector3TimeMinutes = lap.sector3TimeMinutes,
                                                LapValidBitFlags = lap.lapValidBitFlags
                                            }),
                TyreStintHistoryDetails = packet.tyreStintHistoryDetails.Select(tyre => new TyreStintHistoryDetails
                    {
                        EndLap = tyre.endLap,
                        TyreActualCompound = tyre.tyreActualCompound,
                        TyreVisualCompound = tyre.tyreVisualCompound
                    })
            };
        }
    }
}
