namespace Serialization
{
    class Element : IElement
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public void Accept(IElementSerializer serializer)
        {
            bool isNotIgnored = serializer.PreSerializeElement(this);

            if (isNotIgnored)
            {
                serializer.Serialize(this);
            }
        }
    }
}
