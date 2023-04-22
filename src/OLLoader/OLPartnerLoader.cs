using Newtonsoft.Json.Linq;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    public class OLPartnerLoader
    {
        internal OLPartnerLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        public async Task<(bool, OLPartnerData?)> TryGetDataAsync(PartnerIdType idType, string id)
            => await TryGetDataAsync(_client, idType, id);
        public async Task<OLPartnerData?> GetDataAsync(PartnerIdType idType, string id)
            => await GetDataAsync(_client, idType, id);

        public async Task<(bool, OLPartnerData?)> TryGetDataAsync(string idType, string id)
            => await TryGetDataAsync(_client, idType, id);
        public async Task<OLPartnerData?> GetDataAsync(string idType, string id)
            => await GetDataAsync(_client, idType, id);

        public async Task<(bool, OLPartnerData[]?)> TryGetMulitDataAsync(params string[] ids)
            => await TryGetMultiDataAsync(_client, ids);
        public async Task<OLPartnerData[]?> GetMultiDataAsync(params string[] ids)
            => await GetMultiDataAsync(_client, ids);

        public async static Task<(bool, OLPartnerData?)> TryGetDataAsync(HttpClient client, PartnerIdType idType, string id)
            => await TryGetDataAsync(client, idType.GetString(), id);
        public async static Task<OLPartnerData?> GetDataAsync(HttpClient client, PartnerIdType idType, string id)
            => await GetDataAsync(client, idType.GetString(), id);

        public async static Task<(bool, OLPartnerData?)> TryGetDataAsync(HttpClient client, string idType, string id)
        {
            try { return (true, await GetDataAsync(client, idType, id)); }
            catch { return (false, null); }
        }
        public async static Task<OLPartnerData?> GetDataAsync(HttpClient client, string idType, string id)
        {
            JObject root = JObject.Parse(await OpenLibraryUtility.RequestAsync
            (
                OpenLibraryUtility.BuildPartnerUri
                (
                    idType,
                    id
                ),
                client
            ));

            var prop = root.First;
            var child = prop!.First;
            var data = child!.First!.First!.ToObject<OLPartnerData>();

            return data! with { Items = root["items"]!.ToObject<OLPartnerData.Item[]>()! };
        }

        public async static Task<(bool, OLPartnerData[]?)> TryGetMultiDataAsync(HttpClient client, params string[] ids)
        {
            try { return (true, await GetMultiDataAsync(client, ids)); }
            catch { return (false, null); }
        }
        public async static Task<OLPartnerData[]?> GetMultiDataAsync(HttpClient client, params string[] ids)
        {
            JObject root = JObject.Parse(await OpenLibraryUtility.RequestAsync
            (
                OpenLibraryUtility.BuildPartnerMultiUri(ids),
                client
            ));

            List<JToken> tokens = root.Children().ToList();
            List<OLPartnerData> partnerData = new List<OLPartnerData>();

            for (int i = 0; i < tokens.Count; i++)
            {
                JToken token = tokens[i];
                var prop = token!.First!.First!.First!.First!.First;
                partnerData.Add(prop!.ToObject<OLPartnerData>()! with { Items = token.First["items"]!.ToObject<OLPartnerData.Item[]>()! });
            }

            return partnerData.ToArray();
        }

    }
}
