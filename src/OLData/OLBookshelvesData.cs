using Newtonsoft.Json;

namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an OpenLibrary Bookshelves request.
    /// </summary>
    public sealed record OLBookshelvesData : OLContainer
    {
        [JsonProperty("want_to_read")]
        public int WantToRead { get; init; } = -1;
        [JsonProperty("currently_reading")]
        public int CurrentlyReading { get; init; } = -1;
        [JsonProperty("already_read")]
        public int AlreadyRead { get; init; } = -1;

        public bool Equals(OLBookshelvesData? data)
        {
            return data != null &&
                CompareExtensionData(data.extensionData) &&
                this.WantToRead == data.WantToRead &&
                this.AlreadyRead == data.AlreadyRead &&
                this.CurrentlyReading == data.CurrentlyReading;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
