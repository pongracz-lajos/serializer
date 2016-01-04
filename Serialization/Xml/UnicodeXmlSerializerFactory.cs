namespace Serialization.Xml
{
    public class UnicodeXmlSerializerFactory : ISerializerFactory
    {
        public ISerializer<T> GetSerializer<T>()
        {
            return new UnicodeXmlSerializer<T>();
        }
    }
}
