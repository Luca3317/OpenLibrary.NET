using Newtonsoft.Json;
using System.Collections.ObjectModel;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Data
{
    /// <summary>
    /// Holds data about an author.
    /// </summary>
    public sealed record OLAuthorData : OLContainer
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

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.<br/>
        /// Compares purely by value, including collections.<br/>
        /// Does NOT consider extension data.
        /// </summary>
        /// <param name="data">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(OLAuthorData? data)
        {
            return
                data != null &&
                this.Key == data.Key &&
                this.Name == data.Name &&
                this.BirthDate == data.BirthDate &&
                this.DeathDate == data.DeathDate &&
                this.Bio == data.Bio &&
                GeneralUtility.SequenceEqual(this._photosIDs, data._photosIDs);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => base.GetHashCode();
    }
}
