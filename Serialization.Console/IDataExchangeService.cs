namespace Serialization.Console
{
    interface IDataExchangeService<T>
    {
        ISerializerFactory SerializerFactory { get; set; }

        string GetHeader(ICustomSerializable<T> serializableObject);

        string GetFull(ICustomSerializable<T> serializableObject);

        string GetFullForExchange(ICustomSerializable<T> serializableObject);
    }
}
