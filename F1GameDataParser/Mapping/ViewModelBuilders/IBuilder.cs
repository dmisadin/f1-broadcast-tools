namespace F1GameDataParser.Mapping.ViewModelBuilders
{
    public interface IBuilder<TViewModel> : IViewModelBuilder<TViewModel>
        where TViewModel : class
    {
    }
}
