using OpenLibraryNET.Utility;
using OpenLibraryNET.Data;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Lists API.
    /// </summary>
    public class OLListLoader
    {
        internal OLListLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        /// <summary>
        /// Attempt to get data about all of an user's lists.
        /// </summary>
        /// <param name="username">The user to get the lists of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLListData[]?)> TryGetUsersListsAsync(string username)
            => await TryGetUsersListAsync(_client, username);
        /// <summary>
        /// Get data about all of an user's lists.
        /// </summary>
        /// <param name="username">The user to get the lists of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLListData[]?> GetUserListsAsync(string username)
            => await GetUserListsAsync(_client, username);

        /// <summary>
        /// Attempt to get data about a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLListData?)> TryGetListAsync(string username, string id)
            => await TryGetListAsync(_client, username, id);
        /// <summary>
        /// Get data about a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLListData?> GetListAsync(string username, string id)
            => await GetListAsync(_client, username, id);

        /// <summary>
        /// Attempt to get editions contained in a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLEditionData[]?)> TryGetListEditionsAsync(string username, string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetListEditionsAsync(_client, username, id, parameters);
        /// <summary>
        /// Get editions contained in a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLEditionData[]?> GetListEditionsAsync(string username, string id, params KeyValuePair<string, string>[] parameters)
            => await GetListEditionsAsync(_client, username, id, parameters);

        /// <summary>
        /// Attempt to get data about all of an user's lists.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the lists of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLListData[]?)> TryGetUsersListAsync(HttpClient client, string username)
        {
            try { return (true, await GetUserListsAsync(client, username)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about all of an user's lists.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the lists of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLListData[]?> GetUserListsAsync(HttpClient client, string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                client,
                OpenLibraryUtility.BuildListsUri(username),
                "entries"
            );
        }

        /// <summary>
        /// Attempts to get data about a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLListData?)> TryGetListAsync(HttpClient client, string username, string id)
        {
            try { return (true, await GetListAsync(client, username, id)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLListData?> GetListAsync(HttpClient client, string username, string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData>
            (
                client,
                OpenLibraryUtility.BuildListsUri(username, id)
            );
        }

        /// <summary>
        /// Attempts to get editions contained in a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLEditionData[]?)> TryGetListEditionsAsync(HttpClient client, string username, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListEditionsAsync(client, username, id, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get editions contained in a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLEditionData[]?> GetListEditionsAsync(HttpClient client, string username, string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData[]>
            (
                client,
                OpenLibraryUtility.BuildListsUri(username, id, "editions", parameters),
                "entries"
            );
        }
    }

}
