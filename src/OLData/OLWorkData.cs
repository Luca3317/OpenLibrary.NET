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
}
