using System.Collections.Generic;
using System.Text;

namespace Serialization.Json
{
    class JsonSerializer<T> : ISerializer<T>, IElementSerializer, IHasElementTreeFactory
    {
        private Dictionary<string, string> aliases;

        private ICollection<string> ignoreList;

        private List<List<string>> childJsonElements;

        private string result;

        public IElementTreeFactory TreeFactory { get; set; }

        public JsonSerializer()
        {
            TreeFactory = new ElementTreeFactory();
            ignoreList = new List<string>();
            aliases = new Dictionary<string, string>();
            childJsonElements = new List<List<string>>();
            childJsonElements.Add(new List<string>());
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

            return "{\n" + result + "\n}";
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

            childJsonElements.Add(new List<string>());
            return true;
        }

        public void Serialize(Element element)
        {
            var name = element.Name;

            if (aliases.ContainsKey(name))
            {
                name = aliases[name];
            }

            var jsonElement = string.Format("{0}\"{1}\" : \"{2}\"", Indentation() + "  ", name, element.Value);
            childJsonElements[childJsonElements.Count - 1].Add(jsonElement);
        }

        public void Serialize(CompositeElement compositeElement)
        {
            var name = compositeElement.Name;

            if (aliases.ContainsKey(name))
            {
                name = aliases[name];
            }

            var childElements = childJsonElements[childJsonElements.Count - 1];
            var jsonElement = string.Empty;

            if (compositeElement.IsCollection)
            {
                jsonElement = string.Format("{0}\"{1}\" : [\n{2}\n{0}]", Indentation(), name, string.Join(",\n", childElements));
            }
            else
            {
                jsonElement = string.Format("{0}\"{1}\" : {{\n{2}\n{0}}}", Indentation(), name, string.Join(",\n", childElements));
            }
            
            childJsonElements.Remove(childElements);
            childJsonElements[childJsonElements.Count - 1].Add(jsonElement);

            result = jsonElement;
        }

        private string Indentation()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 1; i < childJsonElements.Count; i++)
            {
                builder.Append("  ");
            }

            return builder.ToString();
        }
    }
}
