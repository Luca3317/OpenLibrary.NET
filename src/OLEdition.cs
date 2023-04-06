using Newtonsoft.Json;

namespace OpenLibraryNET
{
    /// <summary>
    /// Represents an (OpenLibrary) edition.
    /// </summary>
    public sealed record OLEdition
    {
        [JsonIgnore]
        public string ID => _id;
        [JsonIgnore]
        public OLEditionData? Data => _data;

        [JsonConstructor]
        public OLEdition(string id) => _id = id;

        public OLEdition(OLEditionData data)
        {
            _id = data.ID;
            _data = data;
        }

        public async Task<(bool, OLEditionData?)> TryGetDataAsync()
        {
            try { return (true, await GetDataAsync()); }
            catch { return (false, null); }
        }
        public async Task<OLEditionData?> GetDataAsync()
        {
            if (_data == null) _data = await OLEditionLoader.GetDataByOLIDAsync(_id);
            return _data;
        }

        public async Task<byte[]?> GetCoverAsync(string size)
        {
            switch (size.Trim().ToLower())
            {
                case "s":
                    if (_coverS == null) _coverS = await OLImageLoader.GetCoverAsync("olid", _id, "S");
                    return _coverS;

                case "m":
                    if (_coverM == null) _coverM = await OLImageLoader.GetCoverAsync("olid", _id, "M");
                    return _coverS;

                case "l":
                    if (_coverL == null) _coverL = await OLImageLoader.GetCoverAsync("olid", _id, "L");
                    return _coverL;
            }

            return null;
        }

        [JsonProperty("id")]
        private string _id;
        [JsonProperty("data")]
        private OLEditionData? _data = null;
        [JsonProperty("cover_s")]
        private byte[]? _coverS = null;
        [JsonProperty("cover_m")]
        private byte[]? _coverM = null;
        [JsonProperty("cover_l")]
        private byte[]? _coverL = null;

        public bool Equals(OLEdition? edition)
        {
            return edition != null &&
                edition.ID == ID &&
                edition.Data == Data;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}