using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Authors API.
    /// </summary>
    public class OLSearchLoader
    {
        internal OLSearchLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        /// <summary>
        /// Attempt to search for works and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLWorkData[]?)> TryGetSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await TryGetSearchResultsAsync(_client, query, parameters);
        /// <summary>
        /// Search for works and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLWorkData[]?> GetSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await GetSearchResultsAsync(_client, query, parameters);

        /// <summary>
        /// Attempt to search for authors and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLAuthorData[]?)> TryGetAuthorSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await TryGetAuthorSearchResultsAsync(_client, query, parameters);
        /// <summary>
        /// Search for authors and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLAuthorData[]?> GetAuthorSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await GetAuthorSearchResultsAsync(_client, query, parameters);

        /// <summary>
        /// Attempt to search for subjects and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLSubjectData[]?)> TryGetSubjectSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await TryGetSubjectSearchResultsAsync(_client, query, parameters);
        /// <summary>
        /// Search for subjects and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLSubjectData[]?> GetSubjectSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await GetSubjectSearchResultsAsync(_client, query, parameters);

        /// <summary>
        /// Attempt to search for lists and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLListData[]?)> TryGetListSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await TryGetListSearchResultsAsync(_client, query, parameters);
        /// <summary>
        /// Search for lists and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLListData[]?> GetListSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await GetListSearchResultsAsync(_client, query, parameters);

        /// <summary>
        /// Attempt to search inside books and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLContainer?)> TryGetInsideSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await TryGetInsideSearchResultsAsync(_client, query, parameters);
        /// <summary>
        /// Search inside books and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLContainer?> GetInsideSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
            => await GetInsideSearchResultsAsync(_client, query, parameters);

        /// <summary>
        /// Attempt to search for books and get results.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLWorkData[]?)> TryGetSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Search for books and get results.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLWorkData[]?> GetSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLWorkData[]>
            (
                client,
                OpenLibraryUtility.BuildSearchUri(query, parameters: parameters),
                "docs"
            );
        }

        /// <summary>
        /// Attempt to search for authors and get results.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLAuthorData[]?)> TryGetAuthorSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetAuthorSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Search for authors and get results.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLAuthorData[]?> GetAuthorSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLAuthorData[]>
            (
                client,
                OpenLibraryUtility.BuildSearchUri(query, "authors", parameters),
                "docs"
            );
        }

        /// <summary>
        /// Attempt to search for subjects and get results.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLSubjectData[]?)> TryGetSubjectSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetSubjectSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Search for subjects and get results.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLSubjectData[]?> GetSubjectSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLSubjectData[]>
            (
                client,
                OpenLibraryUtility.BuildSearchUri(query,"subjects", parameters),
                "docs"
            );
        }

        /// <summary>
        /// Attempt to search for lists and get results.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLListData[]?)> TryGetListSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Search for lists and get results.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLListData[]?> GetListSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                client,
                OpenLibraryUtility.BuildSearchUri(query, "lists", parameters),
                "docs"
            );
        }

        /// <summary>
        /// Attempt to search inside books and get results.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLContainer?)> TryGetInsideSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetInsideSearchResultsAsync(client, query, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Search inside books and get results.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLContainer?> GetInsideSearchResultsAsync(HttpClient client, string query, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLContainer>
            (
                client,
                OpenLibraryUtility.BuildSearchUri(query, "inside", parameters)
            );
        }
    }

}
