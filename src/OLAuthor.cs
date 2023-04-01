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
        [JsonProperty("ID")]
        public string ID { get; private set; }
        [JsonProperty("Data")]
        public OLAuthorData? Data { get; init; } = null;
        [JsonProperty("Works")]
        public ReadOnlyCollection<OLWorkData>? Works { get; init; } = null;

        public OLAuthor(string id) => ID = id;

        public bool Equals(OLAuthor? author)
        {
            return author != null &&
                author.ID == ID &&
                author.Data == Data &&
                Enumerable.SequenceEqual(author.Works!, Works!);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}