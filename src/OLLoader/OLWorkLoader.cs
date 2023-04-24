using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader   
{
    /// <summary>
    /// Interface to OpenLibrary's Works API.
    /// </summary>
    public class OLWorkLoader
    {
        internal OLWorkLoader(HttpClient client)
        {
            _client = client;
        }

        private readonly HttpClient _client;

        /// <summary>
        /// Attempt to get data about a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLWorkData?)> TryGetDataAsync(string olid)
            => await TryGetDataAsync(_client, olid);
        /// <summary>
        /// Get data about a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLWorkData?> GetDataAsync(string olid)
            => await GetDataAsync(_client, olid);

        /// <summary>
        /// Attempt to get ratings of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLRatingsData?)> TryGetRatingsAsync(string olid)
            => await TryGetRatingsAsync(_client, olid);
        /// <summary>
        /// Get ratings of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLRatingsData?> GetRatingsAsync(string olid)
            => await GetRatingsAsync(_client, olid);

        /// <summary>
        /// Attempt to get bookshelves of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLBookshelvesData?)> TryGetBookshelvesAsync(string olid)
            => await TryGetBookshelvesAsync(_client, olid);
        /// <summary>
        /// Get bookshelves of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLBookshelvesData?> GetBookshelvesAsync(string olid)
            => await GetBookshelvesAsync(_client, olid);

        /// <summary>
        /// Attempt to get editions of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLEditionData[]?)> TryGetEditionsAsync(string olid, params KeyValuePair<string, string>[] parameters)
            => await TryGetEditionsAsync(_client, olid, parameters);
        /// <summary>
        /// Get editions of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLEditionData[]?> GetEditionsAsync(string olid, params KeyValuePair<string, string>[] parameters)
            => await GetEditionsAsync(_client, olid, parameters);

        /// <summary>
        /// Attempt to get amount of editions of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, int?)> TryGetEditionsCountAsync(string olid)
            => await TryGetEditionsCountAsync(_client, olid);
        /// <summary>
        /// Get amount of editions of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<int> GetEditionsCountAsync(string olid)
            => await GetEditionsCountAsync(_client, olid);

        /// <summary>
        /// Attempt to get lists including a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLListData[]?)> TryGetListsAsync(string olid, params KeyValuePair<string, string>[] parameters)
            => await TryGetListsAsync(_client, olid, parameters);
        /// <summary>
        /// Get lists including a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLListData[]?> GetListsAsync(string olid, params KeyValuePair<string, string>[] parameters)
            => await GetListsAsync(_client, olid, parameters);

        /// <summary>
        /// Attempt to get amount of lists including a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, int?)> TryGetListsCountAsync(string olid)
            => await TryGetListsCountAsync(_client, olid);
        /// <summary>
        /// Get amount of lists including a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<int> GetListsCountAsync(string olid)
            => await GetListsCountAsync(_client, olid);

        /// <summary>
        /// Attempt to get data about a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLWorkData?)> TryGetDataAsync(HttpClient client, string olid)
        {
            try { return (true, await GetDataAsync(client, olid)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLWorkData?> GetDataAsync(HttpClient client, string olid)
        {
            return await OpenLibraryUtility.LoadAsync<OLWorkData>
            (
                client,
                OpenLibraryUtility.BuildWorksUri(olid)
            );
        }

        /// <summary>
        /// Attempt to get ratings of a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLRatingsData?)> TryGetRatingsAsync(HttpClient client, string olid)
        {
            try { return (true, await GetRatingsAsync(client, olid)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get ratings of a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLRatingsData?> GetRatingsAsync(HttpClient client, string olid)
        {
            return await OpenLibraryUtility.LoadAsync<OLRatingsData>
            (
                client,
                OpenLibraryUtility.BuildWorksUri(olid, "ratings"),
                "summary"
            );
        }

        /// <summary>
        /// Attempt to get ratings of a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLBookshelvesData?)> TryGetBookshelvesAsync(HttpClient client, string olid)
        {
            try { return (true, await GetBookshelvesAsync(client, olid)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get ratings of a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLBookshelvesData?> GetBookshelvesAsync(HttpClient client, string olid)
        {
            return await OpenLibraryUtility.LoadAsync<OLBookshelvesData>
            (
                client,
                OpenLibraryUtility.BuildWorksUri(olid, "bookshelves"),
                "counts"
            );
        }

        /// <summary>
        /// Attempt to get editions of a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLEditionData[]?)> TryGetEditionsAsync(HttpClient client, string olid, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetEditionsAsync(client, olid, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get editions of a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLEditionData[]?> GetEditionsAsync(HttpClient client, string olid, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData[]>
            (
                client,
                OpenLibraryUtility.BuildWorksUri
                (
                    olid,
                    "editions",
                    parameters
                ),
                "entries"
            );
        }

        /// <summary>
        /// Attempt to get amount of editions of a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, int?)> TryGetEditionsCountAsync(HttpClient client, string olid)
        {
            try { return (true, await GetEditionsCountAsync(client, olid)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get amount of editions of a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<int> GetEditionsCountAsync(HttpClient client, string olid)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                client,
                OpenLibraryUtility.BuildWorksUri
                (
                    olid,
                    "editions",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
            );
        }

        /// <summary>
        /// Attempt to get lists including a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(HttpClient client, string olid, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(client, olid, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get lists including a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLListData[]?> GetListsAsync(HttpClient client, string olid, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                client,
                OpenLibraryUtility.BuildWorksUri
                (
                    olid,
                    "lists",
                    parameters
                ),
                "entries"
            );
        }

        /// <summary>
        /// Attempt to get amount of lists including a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, int?)> TryGetListsCountAsync(HttpClient client, string olid)
        {
            try { return (true, await GetListsCountAsync(client, olid)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get amount of lists including a work.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<int> GetListsCountAsync(HttpClient client, string olid)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                client,
                OpenLibraryUtility.BuildWorksUri
                (
                    olid,
                    "lists",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
            );
        }
    }

}
