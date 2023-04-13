using OpenLibraryNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibraryNET
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

        public async static Task<(bool, OLWorkData[]?)> TryGetSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLWorkData[]?> GetSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            parameters = parameters.Append(new KeyValuePair<string, string>("q", query)).ToArray();
            return await OpenLibraryUtility.LoadAsync<OLWorkData[]>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Search, parameters: parameters),
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
            parameters = parameters.Append(new KeyValuePair<string, string>("q", query)).ToArray();
            return await OpenLibraryUtility.LoadAsync<OLAuthorData[]>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Search, path: "authors", parameters: parameters),
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
            parameters = parameters.Append(new KeyValuePair<string, string>("q", query)).ToArray();
            return await OpenLibraryUtility.LoadAsync<OLSubjectData[]>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Search, path: "subjects", parameters: parameters),
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
            parameters = parameters.Append(new KeyValuePair<string, string>("q", query)).ToArray();
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Search, path: "lists", parameters: parameters),
                "docs",
                client: client
            );
        }
    }

}
