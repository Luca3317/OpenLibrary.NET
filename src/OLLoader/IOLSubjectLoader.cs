using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Subjects API.
    /// </summary>
    public interface IOLSubjectLoader
    {
        /// <summary>
        /// Attempt to get data about a subject.
        /// </summary>
        /// <param name="subject">The name of the subject.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLSubjectData?)> TryGetDataAsync(
            string subject,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLSubjectData?> GetDataAsync(
            string subject,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get lists including a subject.
        /// </summary>
        /// <param name="subject">The name of the subject.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLListData[]?)> TryGetListsAsync(
            string subject,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLListData[]?> GetListsAsync(
            string subject,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get amount of lists including a subject.
        /// </summary>
        /// <param name="subject">The name of the subject.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, int?)> TryGetListsCountAsync(string subject);

        /// <summary>
        /// Get amount of lists including a subject.
        /// </summary>
        /// <param name="subject">The name of the subject.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<int> GetListsCountAsync(string subject);
    }
}
