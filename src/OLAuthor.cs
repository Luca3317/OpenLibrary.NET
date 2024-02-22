using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;
using CodeGeneration_Attributes;

namespace OpenLibraryNET
{
    /// <summary>
    /// Composite storage of various data related to an author.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLAuthor
    {
        /// <summary>
        /// The ID of the author.
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; init; } = "";
        /// <summary>
        /// Data about the author itself.
        /// </summary>
        [JsonProperty("data")]
        public OLAuthorData? Data { get; init; } = null;
        /// <summary>
        /// Works by the author.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<OLWorkData>? Works
        {
            get => _works == null ? null : new ReadOnlyCollection<OLWorkData>(_works);
            init { if (value == null) _works = null; else _works = value.ToArray(); }
        }

        [JsonProperty("works")]
        private OLWorkData[]? _works { get; init; } = null;
    }
}