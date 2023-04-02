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
    /// Represents an OpenLibrary Author request.
    /// </summary>
    public sealed record OLAuthorData : OLContainer
    {
        [JsonIgnore]
        public string ID => OpenLibraryUtility.ExtractIdFromKey(Key);

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

        [JsonIgnore]
        public IReadOnlyList<int> PhotosIDs
        {
            get => new ReadOnlyCollection<int>(photosIDs);
            init => photosIDs = value.ToArray();
        }

        [JsonProperty("photos")]
        private int[] photosIDs { get; init; } = new int[0];

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
}
