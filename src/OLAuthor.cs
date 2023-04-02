using Newtonsoft.Json;
using System.Collections.ObjectModel;

/*
 * TODO
 * Add builders to OLAuthor/Work
 * Maybe: Make loaders sub classes of the objects
 */
namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an (OpenLibrary) author.
    /// </summary>
    public sealed record OLAuthor : OLContainer
    {
        [JsonProperty("id")]
        public string ID { get; private set; }
        [JsonProperty("data")]
        public OLAuthorData? Data { get; init; } = null;
        [JsonIgnore]
        public IReadOnlyList<OLWorkData>? Works
        {
            get => works == null ? null : new ReadOnlyCollection<OLWorkData>(works);
            init { if (value != null) works = value.ToArray(); }
        }

        [JsonProperty("works")]
        private OLWorkData[]? works = null;

        public OLAuthor(string id) => ID = id;

        public bool Equals(OLAuthor? author)
        {
            return author != null &&
                CompareExtensionData(author.extensionData) &&
                author.ID == ID &&
                author.Data == Data &&
                SequenceEqual(author.Works!, Works!);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}