using Newtonsoft.Json;
using CodeGeneration_Attributes;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a book's ViewAPI.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLBookViewAPI : OLContainer
    {
        /// <summary>
        /// The book's bibkey.
        /// </summary>
        [JsonProperty("bib_key")]
        public string Bibkey { get; init; } = "";
        /// <summary>
        /// The book's information URL.
        /// </summary>
        [JsonProperty("info_url")]
        public string InfoURL { get; init; } = "";
        /// <summary>
        /// The book's preview state.
        /// </summary>
        [JsonProperty("preview")]
        public string Preview { get; init; } = "";
        /// <summary>
        /// The book's thumbnail URL.
        /// </summary>
        [JsonProperty("thumbnail_url")]
        public string ThumbnailURL { get; init; } = "";
    }
}
