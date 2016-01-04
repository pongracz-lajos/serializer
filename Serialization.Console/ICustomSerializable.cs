namespace Serialization.Console
{
    interface ICustomSerializable<T>
    {
        string Serialize(ISerializer<T> serializer);
    }
}
