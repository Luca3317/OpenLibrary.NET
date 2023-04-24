using Newtonsoft.Json.Linq;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Partner API.
    /// </summary>
    public class OLPartnerLoader
    {
        internal OLPartnerLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        /// <summary>
        /// Attempt to get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="idType">The <see cref="PartnerIdType"/> of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLPartnerData?)> TryGetDataAsync(PartnerIdType idType, string id)
            => await TryGetDataAsync(_client, idType, id);
        /// <summary>
        /// Get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="idType">The <see cref="PartnerIdType"/> of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLPartnerData?> GetDataAsync(PartnerIdType idType, string id)
            => await GetDataAsync(_client, idType, id);

        /// <summary>
        /// Attempt to get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLPartnerData?)> TryGetDataAsync(string idType, string id)
            => await TryGetDataAsync(_client, idType, id);
        /// <summary>
        /// Get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLPartnerData?> GetDataAsync(string idType, string id)
            => await GetDataAsync(_client, idType, id);

        /// <summary>
        /// Attempt to get data about multiple online-readable or borrowable books.
        /// </summary>
        /// <param name="ids">The bibkeys of the books. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(PartnerIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLPartnerData[]?)> TryGetMulitDataAsync(params string[] ids)
            => await TryGetMultiDataAsync(_client, ids);
        /// <summary>
        /// Get data about multiple online-readable or borrowable books.
        /// </summary>
        /// <param name="ids">The bibkeys of the books. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(PartnerIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLPartnerData[]?> GetMultiDataAsync(params string[] ids)
            => await GetMultiDataAsync(_client, ids);

        /// <summary>
        /// Attempt to get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The <see cref="PartnerIdType"/> of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLPartnerData?)> TryGetDataAsync(HttpClient client, PartnerIdType idType, string id)
            => await TryGetDataAsync(client, idType.GetString(), id);
        /// <summary>
        /// Get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The <see cref="PartnerIdType"/> of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLPartnerData?> GetDataAsync(HttpClient client, PartnerIdType idType, string id)
            => await GetDataAsync(client, idType.GetString(), id);

        /// <summary>
        /// Attempt to get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLPartnerData?)> TryGetDataAsync(HttpClient client, string idType, string id)
        {
            try { return (true, await GetDataAsync(client, idType, id)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLPartnerData?> GetDataAsync(HttpClient client, string idType, string id)
        {
            JObject root = JObject.Parse(await OpenLibraryUtility.RequestAsync
            (
                client,
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

        /// <summary>
        /// Attempt to get data about multiple online-readable or borrowable books.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="ids">The bibkeys of the books. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(PartnerIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLPartnerData[]?)> TryGetMultiDataAsync(HttpClient client, params string[] ids)
        {
            try { return (true, await GetMultiDataAsync(client, ids)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about multiple online-readable or borrowable books.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="ids">The bibkeys of the books. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(PartnerIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLPartnerData[]?> GetMultiDataAsync(HttpClient client, params string[] ids)
        {
            JObject root = JObject.Parse(await OpenLibraryUtility.RequestAsync
            (
                client,
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
