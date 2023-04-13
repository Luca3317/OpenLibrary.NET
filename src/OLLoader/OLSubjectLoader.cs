using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    public class OLSubjectLoader
    {
        internal OLSubjectLoader(HttpClient client) => _client = client;

        private HttpClient _client;

        public async Task<(bool, OLSubjectData?)> TryGetDataAsync(string subject, params KeyValuePair<string, string>[] parameters)
            => await TryGetDataAsync(_client, subject, parameters);
        public async Task<OLSubjectData?> GetDataAsync(string subject, params KeyValuePair<string, string>[] parameters)
            => await GetDataAsync(_client, subject, parameters);

        public async Task<(bool, OLListData[]?)> TryGetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetListsAsync(_client, id, parameters);
        public async Task<OLListData[]?> GetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await GetListsAsync(_client, id, parameters);

        public async Task<(bool, int?)> TryGetListsCountAsync(string id)
            => await TryGetListsCountAsync(_client, id);
        public async Task<int> GetListsCountAsync(string id)
            => await GetListsCountAsync(_client, id);

        public async static Task<(bool, OLSubjectData?)> TryGetDataAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetDataAsync(client, id, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLSubjectData?> GetDataAsync(HttpClient client, string subject, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLSubjectData>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Subjects, subject, "", parameters),
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
                    OLRequestAPI.Subjects,
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
                    OLRequestAPI.Subjects,
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
