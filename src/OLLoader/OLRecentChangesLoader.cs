using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's RecentChanges API.
    /// </summary>
    public class OLRecentChangesLoader
    {
        internal OLRecentChangesLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param> 
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(string kind = "", params KeyValuePair<string, string>[] parameters)
            => await TryGetRecentChangesAsync(_client, kind, parameters);
        /// <summary>
        /// Get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, kind, parameters);

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="year">The year the change was made in. Format: "YYYY".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(string year, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await TryGetRecentChangesAsync(_client, year, kind, parameters);
        /// <summary>
        /// Get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, year, kind, parameters);

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="month">The month the change was made in. Format = "MM".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await TryGetRecentChangesAsync(_client, year, month, kind, parameters);
        /// <summary>
        /// Get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="month">The month the change was made in. Format = "MM".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, year, month, kind, parameters);

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="month">The month the change was made in. Format = "MM".</param>
        /// <param name="day">The day the change was made in. Format = "DD".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await TryGetRecentChangesAsync(_client, year, month, day, kind, parameters);
        /// <summary>
        /// Get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="month">The month the change was made in. Format = "MM".</param>
        /// <param name="day">The day the change was made in. Format = "DD".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, year, month, day, kind, parameters);

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(HttpClient client, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetRecentChangesAsync(client, kind, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(client,OpenLibraryUtility.BuildRecentChangesUri(kind, parameters));
        }

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(HttpClient client, string year, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetRecentChangesAsync(client, year, kind, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string year, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(client, OpenLibraryUtility.BuildRecentChangesUri(year, kind, parameters));
        }

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="month">The month the change was made in. Format = "MM".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(HttpClient client, string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetRecentChangesAsync(client, year, month, kind, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="month">The month the change was made in. Format = "MM".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(client, OpenLibraryUtility.BuildRecentChangesUri(year, month, kind, parameters));
        }

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="month">The month the change was made in. Format = "MM".</param>
        /// <param name="day">The day the change was made in. Format = "DD".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(HttpClient client, string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetRecentChangesAsync(client, year, month, day, kind, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="month">The month the change was made in. Format = "MM".</param>
        /// <param name="day">The day the change was made in. Format = "DD".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(client, OpenLibraryUtility.BuildRecentChangesUri(year, month, day, kind, parameters));
        }
    }

}
