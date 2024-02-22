using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;
using CodeGeneration_Attributes;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about an author.
    /// </summary>
    [CollectionValueEquality]
    public sealed partial record OLAuthorData : OLContainer
    {
        /// <summary>
        /// The ID of the author.
        /// </summary>
        [JsonIgnore]
        public string ID => OpenLibraryUtility.ExtractIdFromKey(Key);

        /// <summary>
        /// The key of the author.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; init; } = "";
        /// <summary>
        /// The name of the author.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; init; } = "";
        /// <summary>
        /// The author's birth date.
        /// </summary>
        [JsonProperty("birth_date")]
        public string BirthDate { get; init; } = "";
        /// <summary>
        /// The author's date of death.
        /// </summary>
        [JsonProperty("death_date")]
        public string DeathDate { get; init; } = "";
        /// <summary>
        /// The author's biography.
        /// </summary>
        [JsonProperty("bio")]
        [JsonConverter(typeof(OpenLibraryUtility.Serialization.BioConverter))]
        public string Bio { get; init; } = "";
        
        /// <summary>
        /// The author's photo IDs.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<int> PhotosIDs
        {
            get => new ReadOnlyCollection<int>(_photosIDs);
            init => _photosIDs = value.ToArray();
        }

        [JsonProperty("photos")]
        private int[] _photosIDs { get; init; } = Array.Empty<int>();
    }
}
