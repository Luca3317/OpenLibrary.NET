using Newtonsoft.Json;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Data
{
    public class OLRecentChangesData
    {
        [JsonProperty("id")]
        public string ID { get; init; } = "";
        [JsonProperty("kind")]
        public string Kind { get; init; } = "";
        [JsonProperty("author")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.RecentChangesAuthorConverter))]
        public string Author { get; init; } = "";
        [JsonProperty("ip")]
        public string IP { get; init; } = "";
        [JsonProperty("timestamp")]
        public string Timestamp { get; init; } = "";
        [JsonProperty("comment")]
        public string Comment { get; init; } = "";
        [JsonProperty("changes")]
        public OLChangeData[]? Changes { get; init; } = null;

        public sealed record OLChangeData
        {
            [JsonProperty("key")]
            public string Key { get; init; } = "";
            [JsonProperty("revision")]
            public int Revision { get; init; } = -1;
        }
    }
}
