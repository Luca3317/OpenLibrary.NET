﻿using Newtonsoft.Json;
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
        public IReadOnlyList<string> Subjects
        {
            get => new ReadOnlyCollection<string>(subjects);
            init => subjects = value.ToArray();
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

        [JsonProperty("subjects")]
        private string[] subjects { get; init; } = new string[0];
        [JsonProperty("authors")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.AuthorsKeysConverter))]
        private string[] authors { get; init; } = new string[0];
        [JsonProperty("covers")]
        private int[] coverKeys { get; init; } = new int[0];

        // Aliases
        [JsonProperty("subject")]
        private string[] subjectsSubjects { init => subjects = value; }
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
