using System;

namespace WebApplication.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? PublishDate { get; private set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public int Votes { get; set; }
        public int SmallGroupVotes { get; set; }
        public int LargeGroupVotes { get; set; }
        public bool Published { get; private set; }
        public bool Hidden { get; set; }
        public string DeviceToken { get; set; }

        public static Post Create(string text, string author)
        {
            return new Post
            {
                CreationDate = DateTime.UtcNow,
                Text = text,
                Author = author
            };
        }

        public void Publish()
        {
            Published = true;
            PublishDate = DateTime.UtcNow;
        }
    }
}