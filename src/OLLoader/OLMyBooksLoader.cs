using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's MyBooks API.
    /// </summary>
    public class OLMyBooksLoader
    {
        internal OLMyBooksLoader(OpenLibraryClient client) => _client = client;

        private readonly OpenLibraryClient _client;


        /// <summary>
        /// Attempt to get data about a user's Want-To-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLMyBooksData?)> TryGetWantToReadAsync(string? username = null)
            => await TryGetWantToReadAsync(_client.BackingClient, username == null ? _client.Username?.ToLower()! : username);
        /// <summary>
        /// Get data about a user's Want-To-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLMyBooksData?> GetWantToReadAsync(string? username = null)
            => await GetWantToReadAsync(_client.BackingClient, username == null ? _client.Username?.ToLower()! : username);

        /// <summary>
        /// Attempt to get data about a user's Currently-Reading reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLMyBooksData?)> TryGetCurrentlyReadingAsync(string? username = null)
            => await TryGetCurrentlyReadingAsync(_client.BackingClient, username == null ? _client.Username?.ToLower()! : username);
        /// <summary>
        /// Get data about a user's Currently-Reading reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLMyBooksData?> GetCurrentlyReadingAsync(string? username = null)
            => await GetCurrentlyReadingAsync(_client.BackingClient, username == null ? _client.Username?.ToLower()! : username);

        /// <summary>
        /// Attempt to get data about a user's Already-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLMyBooksData?)> TryGetAlreadyReadAsync(string? username = null)
            => await TryGetAlreadyReadAsync(_client.BackingClient, username == null ? _client.Username?.ToLower()! : username);
        /// <summary>
        /// Get data about a user's Already-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLMyBooksData?> GetAlreadyReadAsync(string? username = null)
            => await GetAlreadyReadAsync(_client.BackingClient, username == null ? _client.Username?.ToLower()! : username);

        /// <summary>
        /// Attempt to get data about a user's specified reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of.</param>
        /// <param name="id">The bookshelf id of the desired reading log.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLMyBooksData?)> TryGetReadingLogAsync(string username, BookshelfID id)
            => await TryGetReadingLogAsync(_client.BackingClient, username, id);
        /// <summary>
        /// Get data about a user's specified reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of.</param>
        /// <param name="id">The bookshelf id of the desired reading log.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLMyBooksData?> GetReadingLogAsync(string username, BookshelfID id)
            => await GetReadingLogAsync(_client.BackingClient, username, id);

        /// <summary>
        /// Attempt to get data about a reading log of the logged in account.
        /// </summary>
        /// <param name="id">The bookshelf id of the desired reading log.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLMyBooksData?)> TryGetReadingLogAsync(BookshelfID id)
        {
            try { return (true, await GetReadingLogAsync(id)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about a reading log of the logged in account.
        /// </summary>
        /// <param name="id">The bookshelf id of the desired reading log.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLMyBooksData?> GetReadingLogAsync(BookshelfID id)
        {
            string? username = _client.Username;
            if (username == null)
            {
                throw new System.InvalidOperationException("The OpenLibraryClient instance is not logged in; you must pass a valid username");
            }

            return await GetReadingLogAsync(_client.BackingClient, username.ToLower(), id);
        }



        /// <summary>
        /// Attempt to get data about a user's Want-To-Read reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLMyBooksData?)> TryGetWantToReadAsync(HttpClient client, string username)
        {
            try { return (true, await GetWantToReadAsync(client, username)); }
            catch { return (false, null); }
        }

        /// <summary>
        /// Get data about a user's Want-To-Read reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLMyBooksData?> GetWantToReadAsync(HttpClient client, string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLMyBooksData?>
            (
                client,
                OpenLibraryUtility.BuildMyBooksUri(username, "want-to-read")
            );
        }

        /// <summary>
        /// Attempt to get data about a user's Currently-Reading reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLMyBooksData?)> TryGetCurrentlyReadingAsync(HttpClient client, string username)
        {
            try { return (true, await GetCurrentlyReadingAsync(client, username)); }
            catch { return (false, null); }
        }

        /// <summary>
        /// Get data about a user's Currently-Reading reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLMyBooksData?> GetCurrentlyReadingAsync(HttpClient client, string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLMyBooksData?>
            (
                client,
                OpenLibraryUtility.BuildMyBooksUri(username, "currently-reading")
            );
        }

        /// <summary>
        /// Attempt to get data about a user's Already-Read reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLMyBooksData?)> TryGetAlreadyReadAsync(HttpClient client, string username)
        {
            try { return (true, await GetAlreadyReadAsync(client, username)); }
            catch { return (false, null); }
        }

        /// <summary>
        /// Get data about a user's Already-Read reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLMyBooksData?> GetAlreadyReadAsync(HttpClient client, string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLMyBooksData?>
            (
                client,
                OpenLibraryUtility.BuildMyBooksUri(username, "already-read")
            );
        }

        /// <summary>
        /// Attempt to get data about a user's specified reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <param name="id">The bookshelf id of the desired reading log.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLMyBooksData?)> TryGetReadingLogAsync(HttpClient client, string username, BookshelfID id)
        {
            try { return (true, await GetReadingLogAsync(client, username, id)); }
            catch { return (false, null); }
        }

        /// <summary>
        /// Get data about a user's specified reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <param name="id">The bookshelf id of the desired reading log.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLMyBooksData?> GetReadingLogAsync(HttpClient client, string username, BookshelfID id)
        {
            switch (id)
            {
                case BookshelfID.WantToRead: return await GetWantToReadAsync(client, username);
                case BookshelfID.CurrentlyReading: return await GetCurrentlyReadingAsync(client, username);
                case BookshelfID.AlreadyRead: return await GetAlreadyReadAsync(client, username);
                default: throw new System.ArgumentException(nameof(id));
            }
        }
    }
}
