using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader   
{
    public class OLWorkLoader
    {
        internal OLWorkLoader(HttpClient client)
        {
            _client = client;
        }

        private HttpClient _client;

        public async Task<(bool, OLWorkData?)> TryGetDataAsync(string id)
            => await TryGetDataAsync(_client, id);
        public async Task<OLWorkData?> GetDataAsync(string id)
            => await GetDataAsync(_client, id);

        public async Task<(bool, OLRatingsData?)> TryGetRatingsAsync(string id)
            => await TryGetRatingsAsync(_client, id);
        public async Task<OLRatingsData?> GetRatingsAsync(string id)
            => await GetRatingsAsync(_client, id);

        public async Task<(bool, OLBookshelvesData?)> TryGetBookshelvesAsync(string id)
            => await TryGetBookshelvesAsync(_client, id);
        public async Task<OLBookshelvesData?> GetBookshelvesAsync(string id)
            => await GetBookshelvesAsync(_client, id);

        public async Task<(bool, OLEditionData[]?)> TryGetEditionsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetEditionsAsync(_client, id, parameters);
        public async Task<OLEditionData[]?> GetEditionsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await GetEditionsAsync(_client, id, parameters);

        public async Task<(bool, int?)> TryGetEditionsCountAsync(string id)
            => await TryGetEditionsCountAsync(_client, id);
        public async Task<int> GetEditionsCountAsync(string id)
            => await GetEditionsCountAsync(_client, id);

        public async Task<(bool, OLListData[]?)> TryGetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetListsAsync(_client, id, parameters);
        public async Task<OLListData[]?> GetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await GetListsAsync(_client, id, parameters);

        public async Task<(bool, int?)> TryGetListsCountAsync(string id)
            => await TryGetListsCountAsync(_client, id);
        public async Task<int> GetListsCountAsync(string id)
            => await GetListsCountAsync(_client, id);

        public async static Task<(bool, OLWorkData?)> TryGetDataAsync(HttpClient client, string id)
        {
            try { return (true, await GetDataAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<OLWorkData?> GetDataAsync(HttpClient client, string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLWorkData>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Books_Works, id),
                client: client
            );
        }

        public async static Task<(bool, OLRatingsData?)> TryGetRatingsAsync(HttpClient client, string id)
        {
            try { return (true, await GetRatingsAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<OLRatingsData?> GetRatingsAsync(HttpClient client, string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLRatingsData>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Books_Works, id, "ratings"),
                "summary",
                client: client
            );
        }

        public async static Task<(bool, OLBookshelvesData?)> TryGetBookshelvesAsync(HttpClient client, string id)
        {
            try { return (true, await GetBookshelvesAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<OLBookshelvesData?> GetBookshelvesAsync(HttpClient client, string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLBookshelvesData>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Books_Works, id, "bookshelves"),
                "counts",
                client: client
            );
        }

        public async static Task<(bool, OLEditionData[]?)> TryGetEditionsAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetEditionsAsync(client, id, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData[]?> GetEditionsAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData[]>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Works,
                    id,
                    "editions",
                    parameters
                ),
                "entries",
                client: client
            );
        }

        public async static Task<(bool, int?)> TryGetEditionsCountAsync(HttpClient client, string id)
        {
            try { return (true, await GetEditionsCountAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetEditionsCountAsync(HttpClient client, string id)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Works,
                    id,
                    "editions",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size",
                client: client
            );
        }

        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetListsAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Works,
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
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Works,
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
