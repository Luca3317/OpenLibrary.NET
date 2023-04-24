using Newtonsoft.Json;
using OpenLibraryNET.Utility;
using System.Collections.ObjectModel;

namespace OpenLibraryNET.Data
{
    public sealed record OLPartnerData : OLContainer
    {
        [JsonProperty("data")]
        public OLEditionData? Data { get; init; } = null;
        [JsonProperty("details")]
        public OLBookViewAPI? Details { get; init; } = null;

        [JsonIgnore]
        public IReadOnlyList<Item> Items
        {
            get => new ReadOnlyCollection<Item>(_items);
            init => _items = value.ToArray();
        }

        [JsonProperty("items")]
        private Item[] _items = Array.Empty<Item>();

        public sealed record Item : OLContainer
        {
            [JsonProperty("enumcron")]
            public bool? Enumcron { get; init; } = null;
            [JsonProperty("match")]
            public string Match { get; init; } = "";
            [JsonProperty("status")]
            public string Status { get; init; } = "";
            [JsonProperty("fromRecord")]
            public string FromRecord { get; init; } = "";
            [JsonProperty("ol-edition-id")]
            public string OLEditionID { get; init; } = "";
            [JsonProperty("ol-work-id")]
            public string OLWorkID { get; init; } = "";
            [JsonProperty("publishData")]
            public string PublishData { get; init; } = "";
            [JsonProperty("itemURL")]
            public string ItemURL { get; init; } = "";
            [JsonProperty("cover")]
            public CoverURL? Cover { get; init; } = null;

            public sealed record CoverURL : OLContainer
            {
                [JsonProperty("small")]
                public string Small { get; init; } = ""; 
                [JsonProperty("medium")]
                public string Medium { get; init; } = ""; 
                [JsonProperty("large")]
                public string Large { get; init; } = "";
            }
        }

        public bool Equals(OLPartnerData? data)
        {
            return
                data != null &&
                this.Data == data.Data &&
                this.Details == data.Details &&
                GeneralUtility.SequenceEqual(this._items, data._items);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
