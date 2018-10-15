using Newtonsoft.Json;

namespace PortalTask.SerializationModels
{
    public class AlbumsModel
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
