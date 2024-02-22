using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;
using CodeGeneration_Attributes;

namespace OpenLibraryNET
{
    /// <summary>
    /// Composite storage of various data related to a work.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLWork
    {
        /// <summary>
        /// The ID of the work.
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; init; } = "";
        /// <summary>
        /// Data about the work itself.
        /// </summary>
        [JsonProperty("data")]
        public OLWorkData? Data { get; init; } = null;
        /// <summary>
        /// The work's ratings.
        /// </summary>
        [JsonProperty("ratings")]
        public OLRatingsData? Ratings { get; init; } = null;
        /// <summary>
        /// The work's bookshelves.
        /// </summary>
        [JsonProperty("bookshelves")]
        public OLBookshelvesData? Bookshelves { get; init; } = null;
        /// <summary>
        /// Editions of the work.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<OLEditionData>? Editions
        {
            get => _editions == null ? null : new ReadOnlyCollection<OLEditionData>(_editions);
            init { if (value == null) _editions = null; else _editions = value.ToArray(); }
        }

        [JsonProperty("editions")]
        private OLEditionData[]? _editions { get; init; } = null;
    }
}
