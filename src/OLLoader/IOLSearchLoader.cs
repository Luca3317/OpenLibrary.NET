using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Authors API.
    /// </summary>
    public interface IOLSearchLoader
    {
        /// <summary>
        /// Attempt to search for works and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLWorkData[]?)> TryGetSearchResultsAsync(
            string query,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLWorkData[]?> GetSearchResultsAsync(
            string query,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to search for authors and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLAuthorData[]?)> TryGetAuthorSearchResultsAsync(
            string query,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLAuthorData[]?> GetAuthorSearchResultsAsync(
            string query,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to search for subjects and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLSubjectData[]?)> TryGetSubjectSearchResultsAsync(
            string query,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLSubjectData[]?> GetSubjectSearchResultsAsync(
            string query,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to search for lists and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLListData[]?)> TryGetListSearchResultsAsync(
            string query,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLListData[]?> GetListSearchResultsAsync(
            string query,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to search inside books and get results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLContainer?)> TryGetInsideSearchResultsAsync(
            string query,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLContainer?> GetInsideSearchResultsAsync(
            string query,
            params KeyValuePair<string, string>[] parameters
        );
    }
}
