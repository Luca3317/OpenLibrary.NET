using Newtonsoft.Json;
using OpenLibraryNET.Utility;
using System.Collections.ObjectModel;

namespace OpenLibraryNET.Data
{
    public sealed record OLMyBooksData : OLContainer
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; init; } = -1;

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<OLReadingLogEntry> ReadingLogEntries
        {
            get => new ReadOnlyCollection<OLReadingLogEntry>(_readingLogEntries);
            init => _readingLogEntries = value.ToArray();
        }

        [JsonProperty("reading_log_entries")]
        private OLReadingLogEntry[] _readingLogEntries { get; init; } = Array.Empty<OLReadingLogEntry>();

        public sealed record OLReadingLogEntry : OLContainer
        {
            [JsonProperty("work")]
            public OLWorkData? Work { get; init; } = null;

            [JsonProperty("logged_edition")]
            public string? LoggedEdition { get; init; } = null;

            [JsonProperty("logged_date")]
            public string? LoggedDate { get; init; } = null;

            /// <summary>
            /// </summary>
            /// <returns></returns>
            public bool Equals(OLReadingLogEntry? data)
            {
                return
                    data != null &&
                    (Work == null ? data.Work == null : Work.Equals(data.Work)) &&
                    this.LoggedEdition == data.LoggedEdition &&
                    this.LoggedDate == data.LoggedDate;
            }

            /// <summary>
            /// Serves as the default hash function.
            /// </summary>
            /// <returns>A hash code for the current object.</returns>
            public override int GetHashCode() => base.GetHashCode();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool Equals(OLMyBooksData? data)
        {
            return
                data != null &&
                this.Page == data.Page &&
                GeneralUtility.SequenceEqual(this._readingLogEntries, data._readingLogEntries);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}
