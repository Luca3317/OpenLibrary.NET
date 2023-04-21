using Newtonsoft.Json;
using OpenLibraryNET.Utility;
using System.Collections.ObjectModel;

namespace OpenLibraryNET.Data
{
    public sealed record OLRecentChangesData : OLContainer
    {
        [JsonProperty("id")]
        public string ID { get; init; } = "";
        [JsonProperty("kind")]
        public string Kind { get; init; } = "";
        [JsonProperty("author")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.RecentChangesAuthorConverter))]
        public string Author { get; init; } = "";
        [JsonProperty("ip")]
        public string IP { get; init; } = "";
        [JsonProperty("timestamp")]
        public string Timestamp { get; init; } = "";
        [JsonProperty("comment")]
        public string Comment { get; init; } = "";

        [JsonIgnore]
        public IReadOnlyList<OLChangeData> Changes
        {
            get => new ReadOnlyCollection<OLChangeData>(_changes);
            init => _changes = value.ToArray();
        }

        [JsonProperty("changes")]
        private OLChangeData[] _changes { get; init; } = Array.Empty<OLChangeData>();

        public sealed record OLChangeData
        {
            [JsonProperty("key")]
            public string Key { get; init; } = "";
            [JsonProperty("revision")]
            public int Revision { get; init; } = -1;
        }

        public bool Equals(OLRecentChangesData? data)
        {
            return
                data != null &&
                CompareExtensionData(data.extensionData) &&
                this.ID == data.ID &&
                this.Kind == data.Kind &&
                this.Author == data.Author &&
                this.IP == data.IP &&
                this.Timestamp == data.Timestamp &&
                this.Comment == data.Comment &&
                GeneralUtility.SequenceEqual(this._changes, data._changes);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
