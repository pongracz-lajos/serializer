namespace Serialization
{
    interface IElementSerializer
    {
        void Serialize(Element element);

        void Serialize(CompositeElement compositeElement);

        bool PreSerializeElement(Element element);

        bool PreSerializeElement(CompositeElement compositeElement);
    }
}
