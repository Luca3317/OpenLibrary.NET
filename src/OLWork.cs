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
        public int? TotalEditions => _totalEditionsCount;

        [JsonIgnore]
        public OLWorkData? Data
        {
            get => _data;
            init => _data = value;
        }
        [JsonIgnore]
        public OLRatingsData? Ratings
        {
            get => _ratings;
            init => _ratings = value;
        }
        [JsonIgnore]
        public OLBookshelvesData? Bookshelves
        {
            get => _bookshelves;
            init => _bookshelves = value;
        }
        [JsonIgnore]
        public IReadOnlyList<OLEditionData>? Editions
        {
            get => _editions == null ? null : new ReadOnlyCollection<OLEditionData>(_editions);
            init => _editions = value?.ToList();
        }

        public OLWork(string id) => _id = id;

        public async Task<(bool, OLWorkData?)> TryGetDataAsync()
        {
            try { return (true, await GetDataAsync()); }
            catch { return (false, null); }
        }
        public async Task<OLWorkData?> GetDataAsync()
        {
            if (_data == null)
            {
                (bool success, var data) = await OLWorkLoader.TryGetDataAsync(_id);
                if (success) _data = data;
            }
            return _data;
        }

        public async Task<(bool, OLRatingsData?)> TryGetRatingsAsync()
        {
            try { return (true, await GetRatingsAsync()); }
            catch { return (false, null); }
        }
        public async Task<OLRatingsData?> GetRatingsAsync()
        {
            if (_ratings == null)
            {
                (bool success, var ratings) = await OLWorkLoader.TryGetRatingsAsync(_id);
                if (success) _ratings = ratings;
            }
            return _ratings;
        }

        public async Task<(bool, OLBookshelvesData?)> TryGetBookshelvesAsync()
        {
            try { return (true, await GetBookshelvesAsync()); }
            catch { return (false, null); }
        }
        public async Task<OLBookshelvesData?> GetBookshelvesAsync()
        {
            if (_bookshelves == null)
            {
                (bool success, var bookshelves) = await OLWorkLoader.TryGetBookshelvesAsync(_id);
                if (success) _bookshelves = bookshelves;
            }
            return _bookshelves;
        }

        public async Task<(bool, IReadOnlyList<OLEditionData>?)> TryRequestEditionsAsync(int amount)
        {
            try { return (true, await RequestEditionsAsync(amount)); }
            catch { return (false, null); }
        }
        public async Task<IReadOnlyList<OLEditionData>?> RequestEditionsAsync(int amount)
        {
            if (_totalEditionsCount != null && _totalEditionsCount > amount)
                amount = _totalEditionsCount.Value;

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
        public async Task<int?> GetTotalEditionCountAsync()
        {
            if (_totalEditionsCount == null) _totalEditionsCount = await OLWorkLoader.GetEditionsCountAsync(_id);
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
        private int? _totalEditionsCount = null;

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
