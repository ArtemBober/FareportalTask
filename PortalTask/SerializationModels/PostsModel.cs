using Newtonsoft.Json;
using System;

namespace PortalTask.Requests
{
    [Serializable]
    public class PostsModel
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }

        public PostsModel(int userId, string title, string body)
        {
            this.UserId = userId;
            this.Title = title;
            this.Body = body;
        }
    }
}
