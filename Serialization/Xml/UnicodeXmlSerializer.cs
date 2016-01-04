using System.Collections.Generic;
using System.Text;

namespace Serialization.Xml
{
    class UnicodeXmlSerializer<T> : ISerializer<T>, IElementSerializer, IElementor
    {
        private Dictionary<string, string> aliases;

        private ICollection<string> ignoreList;

        private List<List<string>> childXmlElements;

        private string result;

        public IElementTreeFactory TreeFactory { get; set; }

        public UnicodeXmlSerializer()
        {
            TreeFactory = new ElementTreeFactory();
            ignoreList = new List<string>();
            aliases = new Dictionary<string, string>();
            childXmlElements = new List<List<string>>();
            childXmlElements.Add(new List<string>());
        }

        public ISerializer<T> Alias(string propertyName, string alias)
        {
            aliases.Add(propertyName, alias);
            return this;
        }

        public ISerializer<T> Ignore(string propertyName)
        {
            ignoreList.Add(propertyName);
            return this;
        }

        public string Serialize(T objectToSerialize)
        {
            var rootElement = TreeFactory.GetRootElement(objectToSerialize);
            rootElement.Accept(this);

            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" + result;
        }

        public bool PreSerializeElement(Element element)
        {
            if (ignoreList.Contains(element.Name))
            {
                return false;
            }

            return true;
        }

        public bool PreSerializeElement(CompositeElement element)
        {
            if (ignoreList.Contains(element.Name))
            {
                return false;
            }

            childXmlElements.Add(new List<string>());
            return true;
        }

        public void Serialize(Element element)
        {
            var xmlElement = string.Format("{0}<{1}>{2}</{1}>", Indentation() + "  ", element.Name, element.Value);
            childXmlElements[childXmlElements.Count - 1].Add(xmlElement);
        }

        public void Serialize(CompositeElement compositeElement)
        {
            var childElements = childXmlElements[childXmlElements.Count - 1];
            var xmlElement = string.Format("{0}<{1}>\n{2}\n{0}</{1}>", Indentation(),
                compositeElement.Name, string.Join("\n", childElements));
            childXmlElements.Remove(childElements);
            childXmlElements[childXmlElements.Count - 1].Add(xmlElement);

            result = xmlElement;
        }

        private string Indentation()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 1; i < childXmlElements.Count - 1; i++)
            {
                builder.Append("  ");
            }

            return builder.ToString();
        }
    }
}
