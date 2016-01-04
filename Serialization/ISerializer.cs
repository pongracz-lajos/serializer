namespace Serialization
{
    public interface ISerializer<T>
    {
        ISerializer<T> Alias(string propertyName, string alias);

        ISerializer<T> Ignore(string propertyName);

        string Serialize(T objectToSerialize);
    }
}
