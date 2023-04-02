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

        [JsonIgnore]
        public IReadOnlyList<string> ISBN => new ReadOnlyCollection<string>(new List<string>(isbn10.Concat(isbn13)));

        [JsonIgnore]
        public IReadOnlyList<string> ISBN10
        {
            get => new ReadOnlyCollection<string>(isbn10);
            init => isbn10 = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> ISBN13
        {
            get => new ReadOnlyCollection<string>(isbn13);
            init => isbn13 = value.ToArray();
        }

        [JsonIgnore]
        public IReadOnlyList<string> AuthorKeys
        {
            get => new ReadOnlyCollection<string>(authors);
            init => authors = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<int> CoverKeys
        {
            get => new ReadOnlyCollection<int>(coverKeys);
            init => coverKeys = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> WorkKeys
        {
            get => new ReadOnlyCollection<string>(workKeys);
            init => workKeys = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> Subjects
        {
            get => new ReadOnlyCollection<string>(subjects);
            init => subjects = value.ToArray();
        }
        [JsonIgnore]
        public IReadOnlyList<string> Publishers
        {
            get => new ReadOnlyCollection<string>(publishers);
            init => publishers = value.ToArray();
        }

        [JsonProperty("isbn_10")]
        private string[] isbn10 { get; init; } = new string[0];
        [JsonProperty("isbn_13")]
        private string[] isbn13 { get; init; } = new string[0];

        [JsonProperty("authors")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.EditionsAuthorsKeysConverter))]
        private string[] authors { get; init; } = new string[0];
        [JsonProperty("covers")]
        private int[] coverKeys { get; init; } = new int[0];
        [JsonProperty("works")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.WorksKeysConverter))]
        private string[] workKeys { get; init; } = new string[0];
        [JsonProperty("subjects")]
        private string[] subjects { get; init; } = new string[0];
        [JsonProperty("publishers")]
        private string[] publishers { get; init; } = new string[0];

        /// <summary>
        /// Holds the various identifiers of an edition.
        /// </summary>
        public sealed record OLEditionIdentifiers : OLContainer
        {
            [JsonIgnore]
            public ReadOnlyCollection<string> Goodreads => goodreads.AsReadOnly();
            [JsonIgnore]
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
    }
}
