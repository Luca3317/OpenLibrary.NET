using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    public class OLAuthorLoader
    {
        internal OLAuthorLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        public async Task<(bool, OLAuthorData?)> TryGetDataAsync(string id)
            => await TryGetDataAsync(_client, id);
        public async Task<OLAuthorData?> GetDataAsync(string id)
            => await GetDataAsync(_client, id);

        public async Task<(bool, OLWorkData[]?)> TryGetWorksAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetWorksAsync(_client, id, parameters);
        public async Task<OLWorkData[]?> GetWorksAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await GetWorksAsync(_client, id, parameters);

        public async Task<(bool, int?)> TryGetWorksCountAsync(string id)
            => await TryGetWorksCountAsync(_client, id);
        public async Task<int> GetWorksCountAsync(string id)
            => await GetWorksCountAsync(_client, id);

        public async Task<(bool, OLListData[]?)> TryGetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetListsAsync(_client, id, parameters);
        public async Task<OLListData[]?> GetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await GetListsAsync(_client, id, parameters);

        public async Task<(bool, int?)> TryGetListsCountAsync(string id)
            => await TryGetListsCountAsync(_client, id);
        public async Task<int> GetListsCountAsync(string id)
            => await GetListsCountAsync(_client, id);

        public async static Task<(bool, OLAuthorData?)> TryGetDataAsync(HttpClient client, string id)
        {
            try { return (true, await GetDataAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<OLAuthorData?> GetDataAsync(HttpClient client, string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLAuthorData>
            (
                OpenLibraryUtility.BuildUri(OLRequestAPI.Authors, id),
                client: client
            );
        }

        public async static Task<(bool, OLWorkData[]?)> TryGetWorksAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetWorksAsync(client, id, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLWorkData[]?> GetWorksAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLWorkData[]>
            (
                OpenLibraryUtility.BuildAuthorsUri
                (
                    id,
                    "works",
                    parameters
                ),
                "entries",
                client: client
            );
        }

        public async static Task<(bool, int?)> TryGetWorksCountAsync(HttpClient client, string id)
        {
            try { return (true, await GetWorksCountAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetWorksCountAsync(HttpClient client, string id)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildAuthorsUri
                (
                    id,
                    "works",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size",
                client: client
            );
        }

        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(client, id, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetListsAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildAuthorsUri
                (
                    id,
                    "lists",
                    parameters
                ),
                "entries",
                client: client
            );
        }

        public async static Task<(bool, int?)> TryGetListsCountAsync(HttpClient client, string id)
        {
            try { return (true, await GetListsCountAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetListsCountAsync(HttpClient client, string id)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildAuthorsUri
                (
                    id,
                    "lists",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size",
                client: client
            );
        }
    }

}
