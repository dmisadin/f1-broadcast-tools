namespace F1GameDataParser.Mapping.ViewModelBuilders
{
    public abstract class ViewModelBuilderBase<TViewModel> : IViewModelBuilder<TViewModel>
        where TViewModel : class
    {
        public virtual TViewModel? Generate()
        {
            return null;
        }
        public virtual IList<TViewModel>? GenerateList()
        {
            return null;
        }
    }
}
