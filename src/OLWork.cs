using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an (OpenLibrary) work.
    /// </summary>
    public sealed record OLWork : OLContainer
    {
        [JsonProperty("id")]
        public string ID { get; private set; }
        [JsonProperty("data")]
        public OLWorkData? Data { get; init; } = null;
        [JsonProperty("ratings")]
        public OLRatingsData? Ratings { get; init; } = null;
        [JsonProperty("bookshelves")]
        public OLBookshelvesData? Bookshelves { get; init; } = null;
        [JsonIgnore]
        public IReadOnlyList<OLEditionData>? Editions
        {
            get => editions == null ? null : new ReadOnlyCollection<OLEditionData>(editions);
            init { if (value != null) editions = value.ToArray(); }
        }

        [JsonProperty("editions")]
        private OLEditionData[]? editions = null;

        public OLWork(string id) => ID = id;

        public bool Equals(OLWork? work)
        {
            return work != null &&
                CompareExtensionData(work.extensionData) &&
                work.ID == ID &&
                work.Data == Data &&
                work.Ratings == Ratings &&
                work.Bookshelves == Bookshelves &&
                SequenceEqual(work.editions, editions);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
