using System.Linq.Expressions;

namespace F1GameDataParser.GameProfiles.F1Common
{
    public interface IModelFactory<TPacket, TModel>
        where TPacket : struct
        where TModel : class 
    {
        Expression<Func<TPacket, TModel>> ToModelExpression();
        TModel ToModel(TPacket packet);
    }
}
