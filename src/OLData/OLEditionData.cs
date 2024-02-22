using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;
using CodeGeneration_Attributes;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about an edition.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLEditionData : OLContainer
    {
        /// <summary>
        /// The edition's ID.
        /// </summary>
        [JsonIgnore]
        public string ID => OpenLibraryUtility.ExtractIdFromKey(Key);

        /// <summary>
        /// The edition's key.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; init; } = "";
        /// <summary>
        /// The edition's title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; init; } = "";
        /// <summary>
        /// The edition's description.
        /// </summary>
        [JsonProperty("description")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.DescriptionConverter))]
        public string Description { get; init; } = "";
        /// <summary>
        /// The edition's page count.
        /// </summary>
        [JsonProperty("number_of_pages")]
        public int PageCount { get; init; } = -1;
        /// <summary>
        /// The edition's identifiers.
        /// </summary>
        [JsonProperty("identifiers")]
        public OLEditionIdentifiers Identifiers { get; init; } = new OLEditionIdentifiers();

        /// <summary>
        /// The edition's ISBNs.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> ISBN => new ReadOnlyCollection<string>(new List<string>(_isbn10.Concat(_isbn13)));
        /// <summary>
        /// The edition's ISBN10s.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> ISBN10
        {
            get => new ReadOnlyCollection<string>(_isbn10);
            init => _isbn10 = value.ToArray();
        }
        /// <summary>
        /// The edition's ISBNs13s.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> ISBN13
        {
            get => new ReadOnlyCollection<string>(_isbn13);
            init => _isbn13 = value.ToArray();
        }

        /// <summary>
        /// The edition's author keys.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> AuthorKeys
        {
            get => new ReadOnlyCollection<string>(_authors);
            init => _authors = value.ToArray();
        }
        /// <summary>
        /// The edition's covers IDs.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<int> CoverIDs
        {
            get => new ReadOnlyCollection<int>(_coverKeys);
            init => _coverKeys = value.ToArray();
        }
        /// <summary>
        /// The edition's work keys.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> WorkKeys
        {
            get => new ReadOnlyCollection<string>(_workKeys);
            init => _workKeys = value.ToArray();
        }
        /// <summary>
        /// The edition's subjects.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> Subjects
        {
            get => new ReadOnlyCollection<string>(_subjects);
            init => _subjects = value.ToArray();
        }
        /// <summary>
        /// The edition's publishers.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> Publishers
        {
            get => new ReadOnlyCollection<string>(_publishers);
            init => _publishers = value.ToArray();
        }

        [JsonProperty("isbn_10")]
        private string[] _isbn10 { get; init; } = Array.Empty<string>();
        [JsonProperty("isbn_13")]
        private string[] _isbn13 { get; init; } = Array.Empty<string>();

        [JsonProperty("authors")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.EditionsAuthorsKeysConverter))]
        private string[] _authors { get; init; } = Array.Empty<string>();
        [JsonProperty("covers")]
        private int[] _coverKeys { get; init; } = Array.Empty<int>();
        [JsonProperty("works")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.WorksKeysConverter))]
        private string[] _workKeys { get; init; } = Array.Empty<string>();
        [JsonProperty("subjects")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.EditionSubjectsConverter))]
        private string[] _subjects { get; init; } = Array.Empty<string>();
        [JsonProperty("publishers")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.EditionPublishersConverter))]
        private string[] _publishers { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Holds the various identifiers of an edition.
        /// </summary>
        [CollectionValueEquality]
        public sealed partial record OLEditionIdentifiers : OLContainer
        {
            /// <summary>
            /// The Goodreads identifiers of the corresponding edition.
            /// </summary>
            [JsonIgnore]
            public IReadOnlyList<string> Goodreads
            {
                get => new ReadOnlyCollection<string>(_goodreads);
                init => _goodreads = value.ToArray();
            }
            /// <summary>
            /// The LibraryThing identifiers of the corresponding edition.
            /// </summary>
            [JsonIgnore]
            public IReadOnlyList<string> LibraryThing
            {
                get => new ReadOnlyCollection<string>(_libraryThing);
                init => _libraryThing = value.ToArray();
            }

            [JsonProperty("goodreads")]
            private string[] _goodreads { get; init; } = Array.Empty<string>();
            [JsonProperty("librarything")]
            private string[] _libraryThing { get; init; } = Array.Empty<string>();
        }
    }
}
