using Newtonsoft.Json;

namespace OpenLibraryNET.Data
{
    public sealed record OLBookViewAPI : OLContainer
    {
        [JsonProperty("bib_key")]
        public string Bibkey { get; init; } = "";

        [JsonProperty("info_url")]
        public string InfoURL { get; init; } = "";

        [JsonProperty("preview")]
        public string Preview { get; init; } = "";

        [JsonProperty("thumbnail_url")]
        public string ThumbnailURL { get; init; } = "";

        public bool Equals(OLBookViewAPI? viewAPI)
        {
            return
                viewAPI != null &&
                CompareExtensionData(viewAPI.extensionData) &&
                this.Bibkey == viewAPI.Bibkey &&
                this.InfoURL == viewAPI.InfoURL &&
                this.Preview == viewAPI.Preview &&
                this.ThumbnailURL == viewAPI.ThumbnailURL;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
