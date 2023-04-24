using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Authors API.
    /// </summary>
    public class OLAuthorLoader
    {
        internal OLAuthorLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        /// <summary>
        /// Attempt to get data about an author.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLAuthorData?)> TryGetDataAsync(string olid)
            => await TryGetDataAsync(_client, olid);
        /// <summary>
        /// Get data about an author.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLAuthorData?> GetDataAsync(string olid)
            => await GetDataAsync(_client, olid);

        /// <summary>
        /// Attempt to get data about an author's works.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <param name="parameters">The query parameters of the request.</param>       
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLWorkData[]?)> TryGetWorksAsync(string olid, params KeyValuePair<string, string>[] parameters)
            => await TryGetWorksAsync(_client, olid, parameters);
        /// <summary>
        /// Get data about an author's works.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLWorkData[]?> GetWorksAsync(string olid, params KeyValuePair<string, string>[] parameters)
            => await GetWorksAsync(_client, olid, parameters);

        /// <summary>
        /// Attempt to get amount of works written by an author.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, int?)> TryGetWorksCountAsync(string olid)
            => await TryGetWorksCountAsync(_client, olid);
        /// <summary>
        /// Get amount of works written by an author.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<int> GetWorksCountAsync(string olid)
            => await GetWorksCountAsync(_client, olid);

        /// <summary>
        /// Attempt to get data about lists an author is included in.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLListData[]?)> TryGetListsAsync(string olid, params KeyValuePair<string, string>[] parameters)
            => await TryGetListsAsync(_client, olid, parameters);
        /// <summary>
        /// Get data about lists an author is included in.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLListData[]?> GetListsAsync(string olid, params KeyValuePair<string, string>[] parameters)
            => await GetListsAsync(_client, olid, parameters);

        /// <summary>
        /// Attempt to get amount of lists an author is included in.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, int?)> TryGetListsCountAsync(string olid)
            => await TryGetListsCountAsync(_client, olid);
        /// <summary>
        /// Get amount of lists an author is included in.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<int> GetListsCountAsync(string olid)
            => await GetListsCountAsync(_client, olid);

        /// <summary>
        /// Attempt to get data about an author.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLAuthorData?)> TryGetDataAsync(HttpClient client, string olid)
        {
            try { return (true, await GetDataAsync(client, olid)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about an author.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLAuthorData?> GetDataAsync(HttpClient client, string olid)
        {
            return await OpenLibraryUtility.LoadAsync<OLAuthorData>
            (
                client,
                OpenLibraryUtility.BuildUri(OLRequestAPI.Authors, olid)
            );
        }

        /// <summary>
        /// Attempt to get data about an author's works.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the author.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLWorkData[]?)> TryGetWorksAsync(HttpClient client, string olid, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetWorksAsync(client, olid, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about an author's works.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the author.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLWorkData[]?> GetWorksAsync(HttpClient client, string olid, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLWorkData[]>
            (
                client,
                OpenLibraryUtility.BuildAuthorsUri
                (
                    olid,
                    "works",
                    parameters
                ),
                "entries"
            );
        }

        /// <summary>
        /// Attempt to get amount of works written by an author.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, int?)> TryGetWorksCountAsync(HttpClient client, string olid)
        {
            try { return (true, await GetWorksCountAsync(client, olid)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get amount of works written by an author.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<int> GetWorksCountAsync(HttpClient client, string olid)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                client,
                OpenLibraryUtility.BuildAuthorsUri
                (
                    olid,
                    "works",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
            );
        }

        /// <summary>
        /// Attempt to get data about lists an author is included in.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the author.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(HttpClient client, string olid, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(client, olid, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about lists an author is included in.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the author.</param>
        /// <param name="parameters">The query parameters of the request.</param>
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
                OpenLibraryUtility.BuildAuthorsUri
                (
                    olid,
                    "lists",
                    parameters
                ),
                "entries"
            );
        }

        /// <summary>
        /// Attempt to get amount of lists an author is included in.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, int?)> TryGetListsCountAsync(HttpClient client, string olid)
        {
            try { return (true, await GetListsCountAsync(client, olid)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get amount of lists an author is included in.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the author.</param>
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
                OpenLibraryUtility.BuildAuthorsUri
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
