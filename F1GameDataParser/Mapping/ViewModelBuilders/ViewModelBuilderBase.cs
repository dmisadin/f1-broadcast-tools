namespace F1GameDataParser.Mapping.ViewModelBuilders
{
    public abstract class ViewModelBuilderBase<TViewModel> : IViewModelBuilder<TViewModel>
        where TViewModel : class
    {
        public abstract TViewModel? Generate();
    }
}
