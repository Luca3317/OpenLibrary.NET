using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an (OpenLibrary) edition.
    /// </summary>
    public sealed record OLEdition : OLContainer
    {
        [JsonIgnore]
        public string ID => _id;
        [JsonIgnore]
        public OLEditionData? Data
        {
            get => _data;
            init => _data = value;
        }

        public OLEdition(string id) => _id = id;

        public async Task<(bool, OLEditionData?)> TryGetDataAsync()
        {
            try { return (true, await GetDataAsync()); }
            catch { return (false, null); }
        }
        public async Task<OLEditionData?> GetDataAsync()
        {
            if (_data == null) _data = await OLEditionLoader.GetDataAsync(_id);
            return _data;
        }

        [JsonProperty("id")]
        private string _id;
        [JsonProperty("data")]
        private OLEditionData? _data = null;

        public bool Equals(OLEdition? edition)
        {
            return edition != null &&
                CompareExtensionData(edition.extensionData) &&
                edition.ID == ID &&
                edition.Data == Data;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}