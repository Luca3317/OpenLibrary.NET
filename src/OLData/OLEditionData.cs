using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an OpenLibrary Edition request.
    /// </summary>
    public sealed record OLEditionData : OLContainer
    {
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

        public ReadOnlyCollection<string> ISBN { get => new ReadOnlyCollection<string>(new List<string>(isbn10.Concat(isbn13))); }

        [JsonIgnore]
        public ReadOnlyCollection<string> ISBN10 => isbn10.AsReadOnly();
        [JsonIgnore]
        public ReadOnlyCollection<string> ISBN13 => isbn13.AsReadOnly();

        [JsonProperty("isbn_10")]
        private List<string> isbn10 { get; init; } = new List<string>();
        [JsonProperty("isbn_13")]
        private List<string> isbn13 { get; init; } = new List<string>();

        [JsonIgnore]
        public ReadOnlyCollection<string> AuthorKeys => new ReadOnlyCollection<string>(authors);
        [JsonIgnore]
        public ReadOnlyCollection<int> CoverKeys => new ReadOnlyCollection<int>(coverKeys);
        [JsonIgnore]
        public ReadOnlyCollection<string> WorkKeys => new ReadOnlyCollection<string>(workKeys);
        [JsonIgnore]
        public ReadOnlyCollection<string> Subjects => new ReadOnlyCollection<string>(subjects);
        [JsonIgnore]
        public ReadOnlyCollection<string> Publishers => new ReadOnlyCollection<string>(publishers);

        [JsonProperty("authors")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.EditionsAuthorsKeysConverter))]
        private List<string> authors { get; init; } = new List<string>();
        [JsonProperty("covers")]
        private List<int> coverKeys { get; init; } = new List<int>();
        [JsonProperty("works")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.WorksKeysConverter))]
        private List<string> workKeys { get; init; } = new List<string>();
        [JsonProperty("subjects")]
        private List<string> subjects { get; init; } = new List<string>();
        [JsonProperty("publishers")]
        private List<string> publishers { get; init; } = new List<string>();

        public bool Equals(OLEditionData? data)
        {
            return
                data != null &&
                CompareExtensionData(data.extensionData) &&
                this.Key == data.Key &&
                this.Title == data.Title &&
                this.Description == data.Description &&
                this.PageCount == data.PageCount &&
                this.Identifiers == data.Identifiers &&
                SequenceEqual(this.isbn10, data.isbn10) &&
                SequenceEqual(this.isbn13, data.isbn13) &&
                SequenceEqual(this.authors, data.authors) &&
                SequenceEqual(this.coverKeys, data.coverKeys) &&
                SequenceEqual(this.workKeys, data.workKeys) &&
                SequenceEqual(this.subjects, data.subjects) &&
                SequenceEqual(this.publishers, data.publishers);
        }

        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Holds the various identifiers of an edition.
        /// </summary>
        public sealed record OLEditionIdentifiers : OLContainer
        {
            public ReadOnlyCollection<string> Goodreads => goodreads.AsReadOnly();
            public ReadOnlyCollection<string> LibraryThing => libraryThing.AsReadOnly();

            [JsonProperty("goodreads")]
            private List<string> goodreads { get; init; } = new List<string>();
            [JsonProperty("librarything")]
            private List<string> libraryThing { get; init; } = new List<string>();

            public bool Equals(OLEditionIdentifiers? data)
            {
                return
                    data != null &&
                    CompareExtensionData(data.extensionData) &&
                    SequenceEqual(this.goodreads, data.goodreads) &&
                    SequenceEqual(this.libraryThing, data.libraryThing);
            }

            public override int GetHashCode() => base.GetHashCode();
        }
    }
}
