using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET
{
    /// <summary>
    /// Composite storage of various data related to a work.
    /// </summary>
    public sealed record OLWork
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

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.<br/>
        /// Compares purely by value, including collections.
        /// </summary>
        /// <param name="data">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(OLWork? data)
        {
            return data != null &&
                data.ID == ID &&
                data.Data == Data &&
                data.Ratings == Ratings &&
                data.Bookshelves == Bookshelves &&
                GeneralUtility.SequenceEqual(data.Editions, Editions);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}
