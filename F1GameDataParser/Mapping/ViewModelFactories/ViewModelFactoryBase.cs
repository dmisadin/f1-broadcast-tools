namespace F1GameDataParser.Mapping.ViewModelFactories
{
    public abstract class ViewModelFactoryBase<TViewModel> : IViewModelFactory<TViewModel>
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
