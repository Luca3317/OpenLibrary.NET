using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    public class OLSearchLoader
    {
        internal OLSearchLoader(HttpClient client) => _client = client;

        HttpClient _client;

        public async Task<(bool, OLWorkData[]?)> TryGetSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await TryGetSearchResultsAsync(_client, query, parameters);
        public async Task<OLWorkData[]?> GetSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await GetSearchResultsAsync(_client, query, parameters);

        public async Task<(bool, OLAuthorData[]?)> TryGetAuthorSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await TryGetAuthorSearchResultsAsync(_client, query, parameters);
        public async Task<OLAuthorData[]?> GetAuthorSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await GetAuthorSearchResultsAsync(_client, query, parameters);

        public async Task<(bool, OLSubjectData[]?)> TryGetSubjectSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await TryGetSubjectSearchResultsAsync(_client, query, parameters);
        public async Task<OLSubjectData[]?> GetSubjectSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await GetSubjectSearchResultsAsync(_client, query, parameters);

        public async Task<(bool, OLListData[]?)> TryGetListSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await TryGetListSearchResultsAsync(_client, query, parameters);
        public async Task<OLListData[]?> GetListSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await GetListSearchResultsAsync(_client, query, parameters);

        public async Task<(bool, OLContainer?)> TryGetInsideSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await TryGetInsideSearchResultsAsync(_client, query, parameters);
        public async Task<OLContainer?> GetInsideSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await GetInsideSearchResultsAsync(_client, query, parameters);


        public async static Task<(bool, OLWorkData[]?)> TryGetSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLWorkData[]?> GetSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLWorkData[]>
            (
                OpenLibraryUtility.BuildSearchUri(query, parameters: parameters),
                "docs",
                client: client
            );
        }

        public async static Task<(bool, OLAuthorData[]?)> TryGetAuthorSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetAuthorSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLAuthorData[]?> GetAuthorSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLAuthorData[]>
            (
                OpenLibraryUtility.BuildSearchUri(query, "authors", parameters),
                "docs",
                client: client
            );
        }

        public async static Task<(bool, OLSubjectData[]?)> TryGetSubjectSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetSubjectSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLSubjectData[]?> GetSubjectSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLSubjectData[]>
            (
                OpenLibraryUtility.BuildSearchUri(query,"subjects", parameters),
                "docs",
                client: client
            );
        }

        public async static Task<(bool, OLListData[]?)> TryGetListSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetListSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildSearchUri(query, "lists", parameters),
                "docs",
                client: client
            );
        }

        public async static Task<(bool, OLContainer?)> TryGetInsideSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetInsideSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLContainer?> GetInsideSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLContainer>
            (
                OpenLibraryUtility.BuildSearchUri(query, "inside", parameters),
                client: client
            );
        }
    }

}
