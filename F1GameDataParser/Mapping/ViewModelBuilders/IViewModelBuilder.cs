namespace F1GameDataParser.Mapping.ViewModelBuilders
{
    public interface IViewModelBuilder<TViewModel> where TViewModel : class
    {
        TViewModel? Generate();
    }
}
