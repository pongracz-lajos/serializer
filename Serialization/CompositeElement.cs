using System.Collections.Generic;

namespace Serialization
{
    class CompositeElement : IElement
    {
        public string Name { get; set; }

        public bool IsCollection { get; set; }

        public ICollection<IElement> ChildElements { get; set; }

        public CompositeElement()
        {
            IsCollection = false;
            ChildElements = new List<IElement>();
        }

        public void Accept(IElementSerializer serializer)
        {
            bool isNotIgnored = serializer.PreSerializeElement(this);

            if (isNotIgnored)
            {
                foreach (var element in ChildElements)
                {
                    element.Accept(serializer);
                }

                serializer.Serialize(this);
            }
        }
    }
}
