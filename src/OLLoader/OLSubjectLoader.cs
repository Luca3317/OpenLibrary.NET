using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Subjects API.
    /// </summary>
    public class OLSubjectLoader
    {
        internal OLSubjectLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        /// <summary>
        /// Attempt to get data about a subject.
        /// </summary>
        /// <param name="subject">The name of the subject.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLSubjectData?)> TryGetDataAsync(string subject, params KeyValuePair<string, string>[] parameters)
            => await TryGetDataAsync(_client, subject, parameters);
        /// <summary>
        /// Get data about a subject.
        /// </summary>
        /// <param name="subject">The name of the subject.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLSubjectData?> GetDataAsync(string subject, params KeyValuePair<string, string>[] parameters)
            => await GetDataAsync(_client, subject, parameters);

        /// <summary>
        /// Attempt to get lists including a subject.
        /// </summary>
        /// <param name="subject">The name of the subject.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLListData[]?)> TryGetListsAsync(string subject, params KeyValuePair<string, string>[] parameters)
            => await TryGetListsAsync(_client, subject, parameters);
        /// <summary>
        /// Get lists including a subject.
        /// </summary>
        /// <param name="subject">The name of the subject.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLListData[]?> GetListsAsync(string subject, params KeyValuePair<string, string>[] parameters)
            => await GetListsAsync(_client, subject, parameters);

        /// <summary>
        /// Attempt to get amount of lists including a subject.
        /// </summary>
        /// <param name="subject">The name of the subject.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, int?)> TryGetListsCountAsync(string subject)
            => await TryGetListsCountAsync(_client, subject);
        /// <summary>
        /// Get amount of lists including a subject.
        /// </summary>
        /// <param name="subject">The name of the subject.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<int> GetListsCountAsync(string subject)
            => await GetListsCountAsync(_client, subject);

        /// <summary>
        /// Attempt to get data about a subject.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="subject">The name of the subject.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLSubjectData?)> TryGetDataAsync(HttpClient client, string subject, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetDataAsync(client, subject, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about a subject.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="subject">The name of the subject.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLSubjectData?> GetDataAsync(HttpClient client, string subject, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLSubjectData>
            (
                client,
                OpenLibraryUtility.BuildSubjectsUri(subject, parameters: parameters)
            );
        }

        /// <summary>
        /// Attempt to get lists including a subject.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="subject">The name of the subject.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(HttpClient client, string subject, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(client, subject, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get lists including a subject.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="subject">The name of the subject.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLListData[]?> GetListsAsync(HttpClient client, string subject, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                client,
                OpenLibraryUtility.BuildSubjectsUri
                (
                    subject,
                    "lists",
                    parameters
                ),
                "entries"
            );
        }

        /// <summary>
        /// Attempt to get amount of lists including a subject.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="subject">The name of the subject.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, int?)> TryGetListsCountAsync(HttpClient client, string subject)
        {
            try { return (true, await GetListsCountAsync(client, subject)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get amount of lists including a subject.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="subject">The name of the subject.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<int> GetListsCountAsync(HttpClient client, string subject)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                client,
                OpenLibraryUtility.BuildSubjectsUri
                (
                    subject,
                    "lists",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
            );
        }
    }

}
