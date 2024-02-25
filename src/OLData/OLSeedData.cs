using CodeGeneration_Attributes;
using Newtonsoft.Json;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.OLData
{
    /// <summary>
    /// Holds data about a list seed.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record class OLSeedData : OLContainer
    {
        /// <summary>
        /// The ID of this seed.
        /// </summary>
        [JsonIgnore]
        public string ID => OpenLibraryUtility.ExtractIdFromKey(Key);

        /// <summary>
        /// The key of this seed.
        /// </summary>
        [JsonProperty("url")]
        public string Key { get; init; } = "";
        /// <summary>
        /// The type of seed this is, i.e. "work", "edition" or "subject".
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; init; } = "";
        /// <summary>
        /// The title of this seed.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; init; } = "";
        /// <summary>
        /// When the object of this seed was last updated.
        /// </summary>
        [JsonProperty("last_update")]
        public string LastUpdate { get; init; } = "";
    }
}
