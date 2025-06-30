namespace F1GameDataParser.Models
{
    public interface IMergeable<T>
    {
        void MergeFrom(T source);
    }

}
