using CodeGeneration_Attributes;
using Newtonsoft.Json;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a <see cref="OLWorkData"/>'s ratings.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLRatingsData : OLContainer
    {
        /// <summary>
        /// The corresponding work's average rating. Null if there are no ratings.
        /// </summary>
        [JsonProperty("average")]
        public float? Average { get; init; } = null;
        /// <summary>
        /// The amount of ratings the corresponding work has received.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; init; } = -1;
    }
}
