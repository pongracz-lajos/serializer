namespace Serialization.Console
{
    class BlogDataExchangeService : IDataExchangeService<Blog>
    {
        public ISerializerFactory SerializerFactory { get; set; }

        public string GetHeader(ICustomSerializable<Blog> serializableObject)
        {
            var blog = serializableObject as Blog;
            var serializer = SerializerFactory.GetSerializer<Blog>();

            return serializer.Alias("Name", "Title")
                    .Ignore("Comments")
                    .Ignore("Date")
                    .Ignore("Author")
                    .Serialize(blog);
        }

        public string GetFull(ICustomSerializable<Blog> serializableObject)
        {
            var blog = serializableObject as Blog;
            var serializer = SerializerFactory.GetSerializer<Blog>();

            return serializer.Serialize(blog);
        }

        public string GetFullForExchange(ICustomSerializable<Blog> serializableObject)
        {
            var blog = serializableObject as Blog;
            var serializer = SerializerFactory.GetSerializer<Blog>();

            return serializer.Alias("Name", "Title")
                    .Alias("CommentsItem", "Comment")
                    .Alias("Date", "PublicationDate")
                    .Serialize(blog);
        }
    }
}
