using Newtonsoft.Json;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Represents an OpenLibrary Ratings request.
    /// </summary>
    public sealed record OLRatingsData : OLContainer
    {
        [JsonProperty("average")]
        public float? Average { get; init; } = null;// Nullable, as 0 ratings do not indicate a rating of 0
        [JsonProperty("count")]
        public int Count { get; init; } = -1;
        // dictionary for counts per rating? public Dictionary

        public bool Equals(OLRatingsData? data)
        {
            return data != null &&
                CompareExtensionData(data.extensionData) &&
                this.Average == data.Average &&
                this.Count == data.Count;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
