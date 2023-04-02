using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an (OpenLibrary) author.
    /// </summary>
    public sealed record OLAuthor : OLContainer
    {
        [JsonIgnore]
        public string ID => _id;

        [JsonIgnore]
        public int WorksRequested => _works == null ? 0 : _works.Count;
        [JsonIgnore]
        public int? TotalWorks => _totalWorks;

        [JsonIgnore]
        public OLAuthorData? Data
        {
            get => _data;
            init => _data = value;
        }
        [JsonIgnore]
        public IReadOnlyList<OLWorkData>? Works
        {
            get => _works == null ? null : new ReadOnlyCollection<OLWorkData>(_works);
            init { _works = value?.ToList(); }
        }

        public OLAuthor(string id) => _id = id;

        public async Task<(bool, OLAuthorData?)> TryGetDataAsync()
        {
            try { return (true, await GetDataAsync()); }
            catch { return (false, null); }
        }
        public async Task<OLAuthorData?> GetDataAsync()
        {
            if (_data == null)
            {
                (bool success, var data) = await OLAuthorLoader.TryGetDataAsync(_id);
                if (success) _data = data;
            }
            return _data;
        }

        public async Task<(bool, IReadOnlyList<OLWorkData>?)> TryRequestWorksAsync(int amount)
        {
            try { return (true, await RequestWorksAsync(amount)); }
            catch { return (false, null); }
        }
        public async Task<IReadOnlyList<OLWorkData>?> RequestWorksAsync(int amount)
        {
            if (_totalWorks != null && _totalWorks > amount)
                amount = _totalWorks.Value;

            if (_works == null)
            {
                _works = new List<OLWorkData>
                (
                    (await OLAuthorLoader.GetWorksAsync
                    (
                        _id,
                        new KeyValuePair<string, string>("limit", amount.ToString())
                    ))!
                );

                return new ReadOnlyCollection<OLWorkData>(_works);
            }
            else if (amount > _works.Count)
            {
                _works.AddRange
                (
                    (await OLAuthorLoader.GetWorksAsync
                    (
                        _id,
                        new KeyValuePair<string, string>("limit", (amount - _works.Count).ToString()),
                        new KeyValuePair<string, string>("offset", _works.Count.ToString())
                    ))!
                );
            }

            return new ReadOnlyCollection<OLWorkData>(_works);
        }

        public async Task<(bool, int?)> TryGetTotalWorksCountAsync()
        {
            try { return (true, await GetTotalWorksCountAsync()); }
            catch { return (false, null); }
        }
        public async Task<int?> GetTotalWorksCountAsync()
        {
            if (_totalWorks == null) _totalWorks = await OLAuthorLoader.GetWorksCountAsync(_id);
            return _totalWorks;
        }

        [JsonProperty("id")]
        private string _id;
        [JsonProperty("data")]
        private OLAuthorData? _data = null;
        [JsonProperty("works")]
        private List<OLWorkData>? _works = null;
        [JsonProperty("total_works")]
        private int? _totalWorks = null;

        public bool Equals(OLAuthor? author)
        {
            return author != null &&
                CompareExtensionData(author.extensionData) &&
                author._id == _id &&
                author._data == _data &&
                SequenceEqual(author._works, _works);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}