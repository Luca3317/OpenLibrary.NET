using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an (OpenLibrary) work.
    /// </summary>
    public sealed record OLWork : OLContainer
    {
        [JsonIgnore]
        public string ID => _id;

        [JsonIgnore]
        public int EditionsRequested => _editions == null ? 0 : _editions.Count;
        [JsonIgnore]
        public int TotalEditions => _totalEditionsCount;

        [JsonIgnore]
        public OLWorkData? Data => _data;
        [JsonIgnore]
        public OLRatingsData? Ratings => _ratings;
        [JsonIgnore]
        public OLBookshelvesData? Bookshelves => _bookshelves;
        [JsonIgnore]
        public IReadOnlyList<OLEditionData>? Editions => _editions == null ? null : new ReadOnlyCollection<OLEditionData>(_editions);

        [JsonConstructor]
        public OLWork(string id) => _id = id;

        public OLWork(OLWorkData data)
        {
            _id = data.ID;
            _data = data;
        }

        public async Task<(bool, OLWorkData?)> TryGetDataAsync()
        {
            try { return (true, await GetDataAsync()); }
            catch { return (false, null); }
        }
        public async Task<OLWorkData?> GetDataAsync()
        {
            if (_data == null) _data = await OLWorkLoader.GetDataAsync(_id);
            return _data;
        }

        public async Task<(bool, OLRatingsData?)> TryGetRatingsAsync()
        {
            try { return (true, await GetRatingsAsync()); }
            catch { return (false, null); }
        }
        public async Task<OLRatingsData?> GetRatingsAsync()
        {
            if (_ratings == null) _ratings = await OLWorkLoader.GetRatingsAsync(_id);

            return _ratings;
        }

        public async Task<(bool, OLBookshelvesData?)> TryGetBookshelvesAsync()
        {
            try { return (true, await GetBookshelvesAsync()); }
            catch { return (false, null); }
        }
        public async Task<OLBookshelvesData?> GetBookshelvesAsync()
        {
            if (_bookshelves == null) _bookshelves = await OLWorkLoader.GetBookshelvesAsync(_id);
            return _bookshelves;
        }

        public async Task<(bool, IReadOnlyList<OLEditionData>?)> TryRequestEditionsAsync(int amount)
        {
            try { return (true, await RequestEditionsAsync(amount)); }
            catch { return (false, null); }
        }
        public async Task<IReadOnlyList<OLEditionData>?> RequestEditionsAsync(int amount)
        {
            if (_totalEditionsCount != -1 && _totalEditionsCount > amount)
                amount = _totalEditionsCount;

            if (_editions == null)
            {
                _editions = new List<OLEditionData>
                (
                    (await OLWorkLoader.GetEditionsAsync
                    (
                        _id,
                        new KeyValuePair<string, string>("limit", amount.ToString())
                    ))!
                );

                return new ReadOnlyCollection<OLEditionData>(_editions);
            }
            else if (amount > _editions.Count)
            {
                _editions.AddRange
                (
                    (await OLWorkLoader.GetEditionsAsync
                    (
                        _id,
                        new KeyValuePair<string, string>("limit", (amount - _editions.Count).ToString()),
                        new KeyValuePair<string, string>("offset", _editions.Count.ToString())
                    ))!
                );
            }

            return new ReadOnlyCollection<OLEditionData>(_editions);
        }

        public async Task<(bool, int?)> TryGetTotalEditionCountAsync()
        {
            try { return (true, await GetTotalEditionCountAsync()); }
            catch { return (false, null); }
        }
        public async Task<int> GetTotalEditionCountAsync()
        {
            if (_totalEditionsCount == -1) _totalEditionsCount = await OLWorkLoader.GetEditionsCountAsync(_id);
            return _totalEditionsCount;
        }

        [JsonProperty("id")]
        private string _id;
        [JsonProperty("data")]
        private OLWorkData? _data = null;
        [JsonProperty("ratings")]
        private OLRatingsData? _ratings = null;
        [JsonProperty("bookshelves")]
        private OLBookshelvesData? _bookshelves = null;
        [JsonProperty("editions")]
        private List<OLEditionData>? _editions = null;
        [JsonProperty("total_editions")]
        private int _totalEditionsCount = -1;

        public bool Equals(OLWork? work)
        {
            return work != null &&
                CompareExtensionData(work.extensionData) &&
                work._id == _id &&
                work._data == _data &&
                work._ratings == _ratings &&
                work._bookshelves == _bookshelves &&
                SequenceEqual(work._editions, _editions);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
