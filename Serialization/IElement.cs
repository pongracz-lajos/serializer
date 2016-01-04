namespace Serialization
{
    interface IElement
    {
        string Name { get; set; }

        void Accept(IElementSerializer serializer);
    }
}
