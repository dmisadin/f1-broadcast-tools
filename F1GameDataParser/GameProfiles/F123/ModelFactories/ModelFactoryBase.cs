using F1GameDataParser.GameProfiles.F1Common.Packets;
using F1GameDataParser.Models;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;

namespace F1GameDataParser.GameProfiles.F123.ModelFactories
{
    public abstract class ModelFactoryBase<TPacket, TModel> : IModelFactory<TPacket, TModel>
        where TPacket : struct
        where TModel : class
    {
        protected readonly Func<PacketHeader, Header> HeaderExpressionCompiled; // YET TO TEST!
        public ModelFactoryBase()
        {
            HeaderExpressionCompiled = HeaderExpression().Compile();
        }

        public abstract Expression<Func<TPacket, TModel>> ToModelExpression();

        public virtual TModel ToModel(TPacket packet) 
        { 
            var expression = ToModelExpression();

            return expression.Compile().Invoke(packet);
        }

        private Expression<Func<PacketHeader, Header>> HeaderExpression()
        {
            return packet => new Header
            {
                PacketFormat = packet.packetFormat,
                GameYear = packet.gameYear,
                GameMajorVersion = packet.gameMajorVersion,
                GameMinorVersion = packet.gameMinorVersion,
                PacketVersion = packet.packetVersion,
                PacketId = packet.packetId,
                SessionUID = packet.sessionUID,
                SessionTime = packet.sessionTime,
                FrameIdentifier = packet.frameIdentifier,
                OverallFrameIdentifier = packet.overallFrameIdentifier,
                PlayerCarIndex = packet.playerCarIndex,
                SecondaryPlayerCarIndex = packet.secondaryPlayerCarIndex
            };
        }

        protected string NullTerminatedASCIIToString(char[] ascii)
        {
            return Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(ascii)).TrimEnd((char)0);
        }
    }
}
