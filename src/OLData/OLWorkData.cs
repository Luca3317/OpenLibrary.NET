using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;
using CodeGeneration_Attributes;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about a work.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLWorkData : OLContainer
    {
        /// <summary>
        /// The ID of the work.
        /// </summary>
        [JsonIgnore]
        public string ID => OpenLibraryUtility.ExtractIdFromKey(Key);

        /// <summary>
        /// The key of the work.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; init; } = "";
        /// <summary>
        /// The title of the work.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; init; } = "";
        /// <summary>
        /// The work's description.
        /// </summary>
        [JsonProperty("description")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.DescriptionConverter))]
        public string Description { get; init; } = "";

        /// <summary>
        /// The work's associated subjects.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> Subjects
        {
            get => new ReadOnlyCollection<string>(_subjects);
            init => _subjects = value.ToArray();
        }
        /// <summary>
        /// The keys of the authors of the work.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> AuthorKeys
        {
            get => new ReadOnlyCollection<string>(_authors);
            init => _authors = value.ToArray();
        }
        /// <summary>
        /// The IDs of the covers of the work.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<int> CoverIDs
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
    }
}
