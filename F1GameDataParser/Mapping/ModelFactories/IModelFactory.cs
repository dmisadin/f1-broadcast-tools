using System.Linq.Expressions;

namespace F1GameDataParser.Mapping.ModelFactories
{
    public interface IModelFactory<TPacket, TModel>
        where TPacket : struct
        where TModel : class 
    {
        Expression<Func<TPacket, TModel>> ToModelExpression();
        TModel ToModel(TPacket packet);
    }
}
