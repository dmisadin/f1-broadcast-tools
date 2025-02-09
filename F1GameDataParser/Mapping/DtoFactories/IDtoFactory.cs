namespace F1GameDataParser.Mapping.DtoFactories
{
    public interface IDtoFactory<TDto> where TDto : class
    {
        TDto? Generate();
    }
}
