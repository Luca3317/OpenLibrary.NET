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

        public async Task<(bool, OLListData[]?)> TryGetListsAsync(string subject, params KeyValuePair<string, string>[] parameters)
            => await TryGetListsAsync(_client, subject, parameters);
        public async Task<OLListData[]?> GetListsAsync(string subject, params KeyValuePair<string, string>[] parameters)
            => await GetListsAsync(_client, subject, parameters);

        public async Task<(bool, int?)> TryGetListsCountAsync(string subject)
            => await TryGetListsCountAsync(_client, subject);
        public async Task<int> GetListsCountAsync(string subject)
            => await GetListsCountAsync(_client, subject);

        public async static Task<(bool, OLSubjectData?)> TryGetDataAsync(HttpClient client, string subject, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetDataAsync(client, subject, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLSubjectData?> GetDataAsync(HttpClient client, string subject, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLSubjectData>
            (
                OpenLibraryUtility.BuildSubjectsUri(subject, parameters: parameters),
                client: client
            );
        }

        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(HttpClient client, string subject, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(client, subject, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetListsAsync(HttpClient client, string subject, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildSubjectsUri
                (
                    subject,
                    "lists",
                    parameters
                ),
                "entries",
                client: client
            );
        }

        public async static Task<(bool, int?)> TryGetListsCountAsync(HttpClient client, string subject)
        {
            try { return (true, await GetListsCountAsync(client, subject)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetListsCountAsync(HttpClient client, string subject)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildSubjectsUri
                (
                    subject,
                    "lists",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size",
                client: client
            );
        }
    }

}
