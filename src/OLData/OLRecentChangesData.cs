using CodeGeneration_Attributes;
using Newtonsoft.Json;
using OpenLibraryNET.Utility;
using System.Collections.ObjectModel;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about recent changes made to OpenLibrary's data.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLRecentChangesData : OLContainer
    {
        /// <summary>
        /// The ID of the change made.
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; init; } = "";
        /// <summary>
        /// The kind of change made.
        /// </summary>
        [JsonProperty("kind")]
        public string Kind { get; init; } = "";
        /// <summary>
        /// The key of the user that made the change.
        /// </summary>
        [JsonProperty("author")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.RecentChangesAuthorConverter))]
        public string AuthorKey { get; init; } = "";
        /// <summary>
        /// The timestamp of the change.
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get; init; } = "";
        /// <summary>
        /// Short comment about the change made.
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; init; } = "";

        /// <summary>
        /// The data entries that the change was made to.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<OLChangeData> Changes
        {
            get => new ReadOnlyCollection<OLChangeData>(_changes);
            init => _changes = value.ToArray();
        }

        [JsonProperty("changes")]
        private OLChangeData[] _changes { get; init; } = Array.Empty<OLChangeData>();

        /// <summary>
        /// Holds data about the data entries that were changed.
        /// </summary>
        [CollectionValueEquality]
        public sealed partial record OLChangeData : OLContainer
        {
            /// <summary>
            /// The key of the object that was changed.
            /// </summary>
            [JsonProperty("key")]
            public string Key { get; init; } = "";
            /// <summary>
            /// Indicates which revision to the object this is.
            /// </summary>
            [JsonProperty("revision")]
            public int Revision { get; init; } = -1;
        }
    }
}
