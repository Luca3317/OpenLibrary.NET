using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace OpenLibraryNET
{
    /// <summary>
    /// Represents an (OpenLibrary) author.
    /// </summary>
    public sealed record OLAuthor
    {
        [JsonProperty("id")]
        public string ID { get; init; } = "";
        [JsonProperty("data")]
        public OLAuthorData? Data { get; init; } = null;
        [JsonIgnore]
        public IReadOnlyList<OLWorkData>? Works
        {
            get => _works == null ? null : new ReadOnlyCollection<OLWorkData>(_works);
            init { if (value == null) _works = null; else _works = value.ToArray(); }
        }

        [JsonProperty("works")]
        private OLWorkData[]? _works { get; init; } = null;

        public bool Equals(OLAuthor? author)
        {
            return author != null &&
                author.ID == ID &&
                author.Data == Data &&
                GeneralUtility.SequenceEqual(author._works, _works);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}