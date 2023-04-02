using Newtonsoft.Json;

namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an (OpenLibrary) edition.
    /// </summary>
    public sealed record OLEdition : OLContainer
    {
        [JsonProperty("id")]
        public string ID { get; private set; }
        [JsonProperty("data")]
        public OLEditionData? Data { get; init; } = null;

        public OLEdition(string id) => ID = id;

        public bool Equals(OLEdition? edition)
        {
            return edition != null &&
                CompareExtensionData(edition.extensionData) &&
                edition.ID == ID &&
                edition.Data == Data;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}