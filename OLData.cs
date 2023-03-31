using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * OLData records each represent exactly one OpenLibrary request's response.
 * 
 * TODO
 *  Make backing list arrays
 */

namespace OpenLibrary.NET
{
    /// <summary>
    /// Base record for OLData and OLObject records.
    /// Implements ExtensionData.
    /// </summary>
    public abstract record OLContainer
    {
        [JsonIgnore]
        public ReadOnlyDictionary<string, JToken>? ExtensionData =>
            extensionData == null ? null : new ReadOnlyDictionary<string, JToken>(extensionData);

        [JsonExtensionData]
        [JsonProperty("extension_data")]
        protected Dictionary<string, JToken>? extensionData = null;

        protected bool CompareExtensionData(Dictionary<string, JToken>? data)
        {
            if (extensionData == null && data == null) return true;
            if (extensionData == null || data == null) return false;
            return Enumerable.SequenceEqual(extensionData.Keys, data.Keys);
        }

        protected bool SequenceEqual<T>(IEnumerable<T>? first, IEnumerable<T>? second)
        {
            if (first == null && second == null) return true;
            if (first == null || second == null) return false;
            return Enumerable.SequenceEqual(first, second);
        }
    }

    /// <summary>
    /// Represents an OpenLibrary Subjects request.
    /// </summary>
    public sealed record OLSubjectData : OLContainer
    {
        [JsonProperty("name")]
        public string Name { get; init; } = "";
        [JsonProperty("work_count")]
        public int WorkCount { get; init; } = -1;

        [JsonIgnore]
        public ReadOnlyCollection<OLWorkData> Works => works.AsReadOnly();

        [JsonProperty("works")]
        private List<OLWorkData> works { get; init; } = new List<OLWorkData>();

        public bool Equals(OLSubjectData? data)
        {
            return
                data != null &&
                CompareExtensionData(data.extensionData) &&
                SequenceEqual(this.extensionData, data.extensionData) &&
                SequenceEqual(this.Works, data.Works) &&
                this.Name == data.Name &&
                this.WorkCount == data.WorkCount;
        }

        public override int GetHashCode() => base.GetHashCode();
    }

    /// <summary>
    /// Represents an OpenLibrary Author request.
    /// </summary>
    public sealed record OLAuthorData : OLContainer
    {
        [JsonProperty("key")]
        public string Key { get; init; } = "";
        [JsonProperty("name")]
        public string Name { get; init; } = "";
        [JsonProperty("birth_date")]
        public string BirthDate { get; init; } = "";
        [JsonProperty("death_date")]
        public string DeathDate { get; init; } = "";
        [JsonProperty("bio")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.BioConverter))]
        public string Bio { get; init; } = "";

        [JsonProperty("photos")]
        private List<int> photosIDs { get; init; } = new List<int>();

        [JsonIgnore]
        public ReadOnlyCollection<int> PhotosIDs => photosIDs.AsReadOnly();

        public bool Equals(OLAuthorData? data)
        {
            return
                data != null &&
                CompareExtensionData(data.extensionData) &&
                this.Key == data.Key &&
                this.Name == data.Name &&
                this.BirthDate == data.BirthDate &&
                this.DeathDate == data.DeathDate &&
                this.Bio == data.Bio &&
                SequenceEqual(this.photosIDs, data.photosIDs);
        }

        public override int GetHashCode() => base.GetHashCode();
    }

    /// <summary>
    /// Represents an OpenLibrary Work request.
    /// </summary>
    public sealed record OLWorkData : OLContainer
    {
        [JsonProperty("key")]
        public string Key { get; init; } = "";
        [JsonProperty("title")]
        public string Title { get; init; } = "";
        [JsonProperty("description")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.DescriptionConverter))]
        public string Description { get; init; } = "";

        [JsonIgnore]
        public ReadOnlyCollection<string> Subjects => subjects.AsReadOnly();
        [JsonIgnore]
        public ReadOnlyCollection<string> AuthorKeys => authors.AsReadOnly();
        [JsonIgnore]
        public ReadOnlyCollection<int> CoverKeys => coverKeys.AsReadOnly();

        [JsonProperty("subjects")]
        public List<string> subjects { get; set; } = new List<string>();
        [JsonProperty("authors")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.AuthorsKeysConverter))]
        private List<string> authors { get; set; } = new List<string>();
        [JsonProperty("covers")]
        private List<int> coverKeys { get; init; } = new List<int>();

        // Aliases
        [JsonProperty("subject")]
        private List<string> subjectsSubjects { init => subjects = value; }
        //[JsonProperty("cover_id")]
        //private int subjectsCover { init => coverKeys.Add(value); }

        public bool Equals(OLWorkData? data)
        {
            return
                data != null &&
                CompareExtensionData(data.extensionData) &&
                this.Key == data.Key &&
                this.Title == data.Title &&
                this.Description == data.Description &&
                SequenceEqual(this.subjects, data.subjects) &&
                SequenceEqual(this.coverKeys, data.coverKeys) &&
                SequenceEqual(this.authors, data.authors);
        }

        public override int GetHashCode() => base.GetHashCode();
    }

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

    /// <summary>
    /// Represents an OpenLibrary Ratings request.
    /// </summary>
    public sealed record OLRatingsData : OLContainer
    {
        [JsonProperty("average")]
        public float? Average { get; init; } = null;// Nullable, as 0 ratings do not indicate a rating of 0
        [JsonProperty("count")]
        public int Count { get; init; } = -1;
        // dictionary for counts per rating? public Dictionary

        public bool Equals(OLRatingsData? data)
        {
            return data != null &&
                CompareExtensionData(data.extensionData) &&
                this.Average == data.Average &&
                this.Count == data.Count;
        }
        public override int GetHashCode() => base.GetHashCode();
    }

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

    /// <summary>
    /// Represents an OpenLibrary List request.
    /// </summary>
    public sealed record OLListData : OLContainer
    {
        [JsonProperty("name")]
        public string Name { get; init; } = "";
        [JsonProperty("url")]
        public string URL { get; init; } = "";
        [JsonProperty("last_update")]
        public string LastUpdate { get; init; } = "";

        public bool Equals(OLListData? data)
        {
            return data != null &&
                CompareExtensionData(data.extensionData) &&
                this.Name == data.Name &&
                this.URL == data.URL &&
                this.LastUpdate == data.LastUpdate;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
