namespace Serialization
{
    interface IHasElementTreeFactory
    {
        IElementTreeFactory TreeFactory { get; set; }
    }
}
