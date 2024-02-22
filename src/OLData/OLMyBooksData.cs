using Newtonsoft.Json;
using System.Collections.ObjectModel;
using CodeGeneration_Attributes;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a user's reading log.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLMyBooksData : OLContainer
    {
        /// <summary>
        /// Pagination data about the reading log.<br/>
        /// This data point appears to be unused at the moment (always being set to 1).
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; init; } = -1;

        /// <summary>
        /// All entries in the reading log.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<OLReadingLogEntry> ReadingLogEntries
        {
            get => new ReadOnlyCollection<OLReadingLogEntry>(_readingLogEntries);
            init => _readingLogEntries = value.ToArray();
        }

        [JsonProperty("reading_log_entries")]
        private OLReadingLogEntry[] _readingLogEntries { get; init; } = Array.Empty<OLReadingLogEntry>();

        /// <summary>
        /// Holds data about an entry in a given reading log.
        /// </summary>
        [CollectionValueEquality]
        public sealed partial record OLReadingLogEntry : OLContainer
        {
            /// <summary>
            /// The corresponding <see cref="OLWorkData"/>.
            /// </summary>
            [JsonProperty("work")]
            public OLWorkData? Work { get; init; } = null;

            /// <summary>
            /// The key of the actual edition that was added to the reading log.
            /// </summary>
            [JsonProperty("logged_edition")]
            public string? LoggedEditionKey { get; init; } = null;

            /// <summary>
            /// The date and time this entry was added to the reading log.
            /// </summary>
            [JsonProperty("logged_date")]
            public string? LoggedDate { get; init; } = null;
        }
    }
}
