using Serialization.Xml;

namespace Serialization.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Blog blog = new Blog();
            blog.Id = 1;
            blog.Name = "Design Patterns";
            blog.Comments.Add("Visitor pattern");
            blog.Comments.Add("Abstract factory pattern");
            blog.Comments.Add("Composite pattern");

            var serializerFactory = new UnicodeXmlSerializerFactory();
            var service = new BlogDataExchangeService();
            service.SerializerFactory = serializerFactory;

            System.Console.WriteLine("{0}:\n{1}\n", "Header as Unicode XML Document", service.GetHeader(blog));
            System.Console.WriteLine("{0}:\n{1}\n", "Full as Unicode XML Document", service.GetFull(blog));

            System.Console.WriteLine("Execution finished. Press a key to exit.");
            System.Console.ReadLine();
        }
    }
}
