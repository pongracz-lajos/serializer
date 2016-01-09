namespace Serialization.Json
{
    public class JsonSerializerFactory : ISerializerFactory
    {
        public ISerializer<T> GetSerializer<T>()
        {
            return new JsonSerializer<T>();
        }
    }
}
