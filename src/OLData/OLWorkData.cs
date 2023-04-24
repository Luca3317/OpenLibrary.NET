using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Represents an OpenLibrary Work request.
    /// </summary>
    public sealed record OLWorkData : OLContainer
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

        [JsonIgnore]
        public IReadOnlyList<string> Subjects
        {
            get => new ReadOnlyCollection<string>(_subjects);
            init => _subjects = value.ToArray();
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

        [JsonProperty("subjects")]
        private string[] _subjects { get; init; } = Array.Empty<string>();
        [JsonProperty("authors")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.AuthorsKeysConverter))]
        private string[] _authors { get; init; } = Array.Empty<string>();
        [JsonProperty("covers")]
        private int[] _coverKeys { get; init; } = Array.Empty<int>();

        // Aliases
        [JsonProperty("subject")]
        private string[] _subjectsSubjects { init => _subjects = value; }
        //[JsonProperty("cover_id")]
        //private int subjectsCover { init => coverKeys.Add(value); }

        public bool Equals(OLWorkData? data)
        {
            return
                data != null &&
                this.Key == data.Key &&
                this.Title == data.Title &&
                this.Description == data.Description &&
                GeneralUtility.SequenceEqual(this._subjects, data._subjects) &&
                GeneralUtility.SequenceEqual(this._coverKeys, data._coverKeys) &&
                GeneralUtility.SequenceEqual(this._authors, data._authors);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
