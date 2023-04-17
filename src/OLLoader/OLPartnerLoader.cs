using Newtonsoft.Json.Linq;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    public class OLPartnerLoader
    {
        internal OLPartnerLoader(HttpClient client) => _client = client;

        HttpClient _client;

        public async Task<(bool, OLPartnerData?)> TryGetPartnerData(PartnerIdType idType, string id)
            => await TryGetPartnerDataAsync(_client, idType, id);
        public async Task<OLPartnerData?> GetPartnerDataAsync(PartnerIdType idType, string id)
            => await GetPartnerDataAsync(_client, idType, id);

        public async Task<(bool, OLPartnerData[]?)> TryGetPartnerMulitData(params string[] ids)
            => await TryGetPartnerMultiDataAsync(_client, ids);
        public async Task<OLPartnerData[]?> GetPartnerMultiDataAsync(params string[] ids)
            => await GetPartnerMultiDataAsync(_client, ids);

        public async static Task<(bool, OLPartnerData?)> TryGetPartnerDataAsync(HttpClient client, PartnerIdType idType, string id)
        {
            try { return (true, await GetPartnerDataAsync(client, idType, id)); }
            catch { return (false, null); }
        }
        public async static Task<OLPartnerData?> GetPartnerDataAsync(HttpClient client, PartnerIdType idType, string id)
        {
            JObject root = JObject.Parse(await OpenLibraryUtility.RequestAsync
            (
                OpenLibraryUtility.BuildPartnerUri
                (
                    idType,
                    id
                )
            ));

            var prop = root.First;
            var child = prop!.First;
            var data = child!.First!.First!.ToObject<OLPartnerData>();

            return data! with { Items = root["items"]!.ToObject<OLPartnerData.Item[]>()! };
        }

        public async static Task<(bool, OLPartnerData[]?)> TryGetPartnerMultiDataAsync(HttpClient client, params string[] ids)
        {
            try { return (true, await GetPartnerMultiDataAsync(client, ids)); }
            catch { return (false, null); }
        }
        public async static Task<OLPartnerData[]?> GetPartnerMultiDataAsync(HttpClient client, params string[] ids)
        {
            JObject root = JObject.Parse(await OpenLibraryUtility.RequestAsync
            (
                OpenLibraryUtility.BuildPartnerMultiUri(ids)
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
