using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET
{
    /// <summary>
    /// Represents an (OpenLibrary) work.
    /// </summary>
    public sealed record OLWork
    {
        [JsonProperty("id")]
        public string ID { get; init; } = "";
        [JsonProperty("data")]
        public OLWorkData? Data { get; init; } = null;
        [JsonProperty("ratings")]
        public OLRatingsData? Ratings { get; init; } = null;
        [JsonProperty("bookshelves")]
        public OLBookshelvesData? Bookshelves { get; init; } = null;
        [JsonIgnore]
        public IReadOnlyList<OLEditionData>? Editions
        {
            get => _editions == null ? null : new ReadOnlyCollection<OLEditionData>(_editions);
            init { if (value == null) _editions = null; else _editions = value.ToArray(); }
        }

        [JsonProperty("editions")]
        private OLEditionData[]? _editions { get; init; } = null;

        public bool Equals(OLWork? work)
        {
            return work != null &&
                work.ID == ID &&
                work.Data == Data &&
                work.Ratings == Ratings &&
                work.Bookshelves == Bookshelves &&
                GeneralUtility.SequenceEqual(work.Editions, Editions);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
