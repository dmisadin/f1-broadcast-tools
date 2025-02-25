namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public interface IViewModelFactory<TViewModel> where TViewModel : class
    {
        TViewModel? Generate();
    }
}
