using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get => new ReadOnlyCollection<Item>(items);
            init => items = value.ToArray();
        }

        [JsonProperty("items")]
        private Item[] items = new Item[0];

        public sealed record Item : OLContainer
        {
            [JsonProperty("enumcron")]
            public bool? Enumcorn { get; init; } = null;
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
    }
}
