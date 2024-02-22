using CodeGeneration_Attributes;
using Newtonsoft.Json;
using OpenLibraryNET.Utility;
using System.Collections.ObjectModel;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about online-readable or borrowable books, along with corresponding <see cref="OLEditionData"/> and <see cref="OLBookViewAPI"/> entries.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLPartnerData : OLContainer
    {
        /// <summary>
        /// The corresponding <see cref="OLEditionData"/>.
        /// </summary>
        [JsonProperty("data")]
        public OLEditionData? Data { get; init; } = null;
        /// <summary>
        /// The corresponding <see cref="OLBookViewAPI"/>.
        /// </summary>
        [JsonProperty("details")]
        public OLBookViewAPI? Details { get; init; } = null;

        /// <summary>
        /// Matching or similar online-readable books.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<Item> Items
        {
            get => new ReadOnlyCollection<Item>(_items);
            init => _items = value.ToArray();
        }

        [JsonProperty("items")]
        private Item[] _items = Array.Empty<Item>();

        /// <summary>
        /// Holds data about an online-readable book.
        /// </summary>
        [CollectionValueEquality]
        public sealed partial record Item : OLContainer
        {
            /// <summary>
            /// The type of match. Either 'exact' or 'similar'.
            /// </summary>
            [JsonProperty("match")]
            public string Match { get; init; } = "";
            /// <summary>
            /// Status of the book. Either 'lendable', 'full access', 'checked out' or 'restricted'.
            /// </summary>
            [JsonProperty("status")]
            public string Status { get; init; } = "";
            /// <summary>
            /// The OLID of the corresponding edition.
            /// </summary>
            [JsonProperty("ol-edition-id")]
            public string OLEditionID { get; init; } = "";
            /// <summary>
            /// The OLID of the corresponding work.
            /// </summary>
            [JsonProperty("ol-work-id")]
            public string OLWorkID { get; init; } = "";
            /// <summary>
            /// The date of publication.
            /// </summary>
            [JsonProperty("publishDate")]
            public string PublishDate { get; init; } = "";
            /// <summary>
            /// The link pointin to either the online-readable scan or to a borrow page.
            /// </summary>
            [JsonProperty("itemURL")]
            public string ItemURL { get; init; } = "";
            /// <summary>
            /// The cover URLs.
            /// </summary>
            [JsonProperty("cover")]
            public CoverURL? Cover { get; init; } = null;

            /// <summary>
            /// Holds URLs to the cover of the corresponding <see cref="Item"/>.
            /// </summary>
            [CollectionValueEquality]
            public sealed partial record CoverURL : OLContainer
            {
                /// <summary>
                /// Link to the cover in small resolution.
                /// </summary>
                [JsonProperty("small")]
                public string Small { get; init; } = "";
                /// <summary>
                /// Link to the cover in medium resolution.
                /// </summary>
                [JsonProperty("medium")]
                public string Medium { get; init; } = "";
                /// <summary>
                /// Link to the cover in large resolution.
                /// </summary>
                [JsonProperty("large")]
                public string Large { get; init; } = "";
            }
        }
    }
}
