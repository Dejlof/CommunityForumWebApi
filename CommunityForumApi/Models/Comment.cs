﻿namespace CommunityForumApi.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int PostId { get; set; }

        public Post Posts { get; set; }
    }
}
