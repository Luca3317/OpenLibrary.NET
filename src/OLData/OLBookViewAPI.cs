using Newtonsoft.Json;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a book's ViewAPI.
    /// </summary>
    public sealed record OLBookViewAPI : OLContainer
    {
        /// <summary>
        /// The book's bibkey.
        /// </summary>
        [JsonProperty("bib_key")]
        public string Bibkey { get; init; } = "";
        /// <summary>
        /// The book's information URL.
        /// </summary>
        [JsonProperty("info_url")]
        public string InfoURL { get; init; } = "";
        /// <summary>
        /// The book's preview state.
        /// </summary>
        [JsonProperty("preview")]
        public string Preview { get; init; } = "";
        /// <summary>
        /// The book's thumbnail URL.
        /// </summary>
        [JsonProperty("thumbnail_url")]
        public string ThumbnailURL { get; init; } = "";

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.<br/>
        /// Compares purely by value, including collections.
        /// </summary>
        /// <param name="data">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(OLBookViewAPI? data)
        {
            return
                data != null &&
                this.Bibkey == data.Bibkey &&
                this.InfoURL == data.InfoURL &&
                this.Preview == data.Preview &&
                this.ThumbnailURL == data.ThumbnailURL;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}
