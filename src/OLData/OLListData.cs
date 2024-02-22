using CodeGeneration_Attributes;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a user-created list.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLListData : OLContainer
    {
        /// <summary>
        /// The list's ID.
        /// </summary>
        public string ID => Regex.Match(URL, "(?<=/)[^/]+$").ToString();

        /// <summary>
        /// The list's name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; init; } = "";
        /// <summary>
        /// The URL pointing to the list.
        /// </summary>
        [JsonProperty("url")]
        public string URL { get; init; } = "";
        /// <summary>
        /// The timestamp of the last update made to the list.
        /// </summary>
        [JsonProperty("last_update")]
        public string LastUpdate { get; init; } = "";
    }
}
