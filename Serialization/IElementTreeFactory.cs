namespace Serialization
{
    interface IElementTreeFactory
    {
        IElement GetRootElement<T>(T objectToSerialize);
    }
}
