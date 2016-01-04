namespace Serialization.Console
{
    class BlogDataExchangeService : IDataExchangeService<Blog>
    {
        public ISerializerFactory SerializerFactory { get; set; }

        public string GetFull(ICustomSerializable<Blog> serializableObject)
        {
            var blog = serializableObject as Blog;
            var serializer = SerializerFactory.GetSerializer<Blog>();

            return serializer.Alias("Name", "Title")
                    .Alias("Date", "CreationDate")
                    .Serialize(blog);
        }

        public string GetHeader(ICustomSerializable<Blog> serializableObject)
        {
            var blog = serializableObject as Blog;
            var serializer = SerializerFactory.GetSerializer<Blog>();

            return serializer.Alias("Name", "Title")
                    .Ignore("Comments")
                    .Ignore("Date")
                    .Serialize(blog);
        }
    }
}
