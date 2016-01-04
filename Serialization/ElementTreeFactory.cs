using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Serialization
{
    class ElementTreeFactory : IElementTreeFactory
    {
        private Type type;

        public IElement GetRootElement<T>(T objectToSerialize)
        {
            type = typeof(T);

            var rootElement = new CompositeElement();
            rootElement.Name = type.Name;

            foreach (var property in type.GetProperties())
            {
                if (IsEnumerable(property))
                {
                    var element = new CompositeElement();
                    element.Name = property.Name;

                    var enumerableProperty = property.GetValue(objectToSerialize, null) as IEnumerable;
                    foreach (var value in enumerableProperty)
                    {
                        var childElement = new Element();
                        childElement.Name = element.Name + "Item";
                        childElement.Value = value.ToString();
                        element.ChildElements.Add(childElement);
                    }

                    rootElement.ChildElements.Add(element);
                }
                else
                {
                    var element = new Element();
                    element.Name = property.Name;
                    element.Value = property.GetValue(objectToSerialize, null).ToString();

                    rootElement.ChildElements.Add(element);
                }
            }

            return rootElement;
        }

        private bool IsEnumerable(PropertyInfo property)
        {
            if (!property.PropertyType.Equals(typeof(string))
                && (property.PropertyType.GetInterfaces().Contains(typeof(IEnumerable))
                || property.PropertyType.GetInterfaces().Contains(typeof(IEnumerable<>))))
            {
                return true;
            }

            return false;
        }
    }
}
