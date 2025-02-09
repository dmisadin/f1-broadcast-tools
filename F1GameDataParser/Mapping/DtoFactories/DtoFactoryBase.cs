namespace F1GameDataParser.Mapping.DtoFactories
{
    public abstract class DtoFactoryBase<TDto> : IDtoFactory<TDto>
        where TDto : class
    {
        public virtual TDto? Generate()
        {
            return null;
        }
        public virtual IList<TDto>? GenerateList()
        {
            return null;
        }
    }
}
