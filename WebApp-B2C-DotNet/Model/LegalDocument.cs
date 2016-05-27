using Newtonsoft.Json;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model
{
    public class LegalDocument
    {
        [JsonProperty(PropertyName = "author")]
        public string AuthorID { get; set; }

        [JsonProperty(PropertyName = "chpaters")]
        public Chapter[] Chapters { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public string OwnerID { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }

    public class Chapter
    {
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}