using System;
using System.Collections.Generic;

namespace Serialization.Console
{
    class Blog : ICustomSerializable<Blog>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Comments { get; set; }

        public Author Author { get; set; }

        public string Date { get; set; }

        public Blog()
        {
            Comments = new List<string>();
            Date = DateTime.Now.ToShortDateString();
        }

        public string Serialize(ISerializer<Blog> serializer)
        {
            return serializer.Serialize(this);
        }
    }
}
