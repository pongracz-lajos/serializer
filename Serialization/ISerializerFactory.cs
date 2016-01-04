namespace Serialization
{
    public interface ISerializerFactory
    {
        ISerializer<T> GetSerializer<T>();
    }
}
