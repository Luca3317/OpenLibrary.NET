using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET
{
    /// <summary>
    /// Composite storage of various data related to an author.
    /// </summary>
    public sealed record OLAuthor
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

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.<br/>
        /// Compares purely by value, including collections.
        /// </summary>
        /// <param name="data">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(OLAuthor? data)
        {
            return data != null &&
                data.ID == ID &&
                data.Data == Data &&
                GeneralUtility.SequenceEqual(data._works, _works);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}