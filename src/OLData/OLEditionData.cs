using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Represents an OpenLibrary Edition request.
    /// </summary>
    public sealed record OLEditionData : OLContainer
    {
        [JsonIgnore]
        public string ID => OpenLibraryUtility.ExtractIdFromKey(Key);

        [JsonProperty("key")]
        public string Key { get; init; } = "";
        [JsonProperty("title")]
        public string Title { get; init; } = "";
        [JsonProperty("description")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.DescriptionConverter))]
        public string Description { get; init; } = "";
        [JsonProperty("number_of_pages")]
        public int PageCount { get; init; } = -1;
        [JsonProperty("identifiers")]
        public OLEditionIdentifiers Identifiers { get; init; } = new OLEditionIdentifiers();

        [JsonIgnore]
        public IReadOnlyList<string> ISBN => new ReadOnlyCollection<string>(new List<string>(_isbn10.Concat(_isbn13)));

        [JsonIgnore]
        public IReadOnlyList<string> ISBN10
        {
            get => new ReadOnlyCollection<string>(_isbn10);
            init => _isbn10 = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> ISBN13
        {
            get => new ReadOnlyCollection<string>(_isbn13);
            init => _isbn13 = value.ToArray();
        }

        [JsonIgnore]
        public IReadOnlyList<string> AuthorKeys
        {
            get => new ReadOnlyCollection<string>(_authors);
            init => _authors = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<int> CoverKeys
        {
            get => new ReadOnlyCollection<int>(_coverKeys);
            init => _coverKeys = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> WorkKeys
        {
            get => new ReadOnlyCollection<string>(_workKeys);
            init => _workKeys = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> Subjects
        {
            get => new ReadOnlyCollection<string>(_subjects);
            init => _subjects = value.ToArray();
        }
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
        public sealed record OLEditionIdentifiers : OLContainer
        {
            [JsonIgnore]
            public IReadOnlyList<string> Goodreads
            {
                get => new ReadOnlyCollection<string>(_goodreads);
                init => _goodreads = value.ToArray();
            }
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

            public bool Equals(OLEditionIdentifiers? data)
            {
                return
                    data != null &&
                    GeneralUtility.SequenceEqual(this._goodreads, data._goodreads) &&
                    GeneralUtility.SequenceEqual(this._libraryThing, data._libraryThing);
            }

            public override int GetHashCode() => base.GetHashCode();
        }

        public bool Equals(OLEditionData? data)
        {
            return
                data != null &&
                this.Key == data.Key &&
                this.Title == data.Title &&
                this.Description == data.Description &&
                this.PageCount == data.PageCount &&
                this.Identifiers == data.Identifiers &&
                GeneralUtility.SequenceEqual(this._isbn10, data._isbn10) &&
                GeneralUtility.SequenceEqual(this._isbn13, data._isbn13) &&
                GeneralUtility.SequenceEqual(this._authors, data._authors) &&
                GeneralUtility.SequenceEqual(this._coverKeys, data._coverKeys) &&
                GeneralUtility.SequenceEqual(this._workKeys, data._workKeys) &&
                GeneralUtility.SequenceEqual(this._subjects, data._subjects) &&
                GeneralUtility.SequenceEqual(this._publishers, data._publishers);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
