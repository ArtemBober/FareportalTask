using Newtonsoft.Json;
using System;

namespace PortalTask.SerializationModels
{
    [Serializable]
    public class UsersModel
    {
        //I've left only properties that are needed for our task

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
