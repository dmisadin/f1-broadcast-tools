namespace F1GameDataParser.Mapping.ViewModelBuilders
{
    public class GenericBuilder<TViewModel> : IBuilder<TViewModel> where TViewModel : class
    {
        public virtual TViewModel? Generate()
        {
            throw new NotImplementedException();
        }
    }
}
