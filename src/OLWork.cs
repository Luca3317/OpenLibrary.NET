using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an (OpenLibrary) work.
    /// </summary>
    public sealed record OLWork : OLContainer
    {
        [JsonProperty("ID")]
        public string ID { get; private set; }
        [JsonProperty("Data")]
        public OLWorkData? Data { get; init; } = null;
        [JsonProperty("Ratings")]
        public OLRatingsData? Ratings { get; init; } = null;
        [JsonProperty("Bookshelves")]
        public OLBookshelvesData? Bookshelves { get; init; } = null;
        [JsonProperty("Editions")]
        public ReadOnlyCollection<OLEditionData>? Editions { get; init; } = null;

        public OLWork(string id) => ID = id;

        public bool Equals(OLWork? work)
        {
            return work != null &&
                work.ID == ID &&
                work.Data == Data &&
                work.Ratings == Ratings &&
                work.Bookshelves == Bookshelves &&
                Enumerable.SequenceEqual(work.Editions!, Editions!);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
