using F1GameDataParser.Models.FinalClassification;
using F1GameDataParser.Packets.FinalClassification;
using System.Linq.Expressions;

namespace F1GameDataParser.Mapping.ModelFactories
{
    public class FinalClassificationModelFactory : ModelFactoryBase<FinalClassificationPacket, FinalClassification>
    {
        public override Expression<Func<FinalClassificationPacket, FinalClassification>> ToModelExpression()
        {
            return packet => new FinalClassification
            {
                Header = HeaderExpressionCompiled.Invoke(packet.header),
                NumCars = packet.numCars,
                Details = packet.classificationDetails.Select(result => new FinalClassificationDetails
                    {
                        Position = result.position,
                        NumLaps = result.numLaps,
                        GridPosition = result.gridPosition,
                        Points = result.points,
                        NumPitStops = result.numPitStops,
                        ResultStatus = result.resultStatus,
                        BestLapTimeInMS = result.bestLapTimeInMS,
                        TotalRaceTime = result.totalRaceTime,
                        PenaltiesTime = result.penaltiesTime,
                        NumPenalties = result.numPenalties,
                        NumTyreStints = result.numTyreStints,
                        TyreStintsActual = result.tyreStintsActual,
                        TyreStintsVisual = result.tyreStintsVisual,
                        TyreStintsEndLaps = result.tyreStintsEndLaps
                    }).ToArray()
            };
        }
    }
}
