using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Authors API.
    /// </summary>
    public interface IOLAuthorLoader
    {
        /// <summary>
        /// Attempt to get data about an author.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLAuthorData?)> TryGetDataAsync(string olid);

        /// <summary>
        /// Get data about an author.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLAuthorData?> GetDataAsync(string olid);

        /// <summary>
        /// Attempt to get data about an author's works.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLWorkData[]?)> TryGetWorksAsync(
            string olid,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLWorkData[]?> GetWorksAsync(
            string olid,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get amount of works written by an author.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, int?)> TryGetWorksCountAsync(string olid);

        /// <summary>
        /// Get amount of works written by an author.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<int> GetWorksCountAsync(string olid);

        /// <summary>
        /// Attempt to get data about lists an author is included in.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLListData[]?)> TryGetListsAsync(
            string olid,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLListData[]?> GetListsAsync(
            string olid,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get amount of lists an author is included in.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, int?)> TryGetListsCountAsync(string olid);

        /// <summary>
        /// Get amount of lists an author is included in.
        /// </summary>
        /// <param name="olid">The OLID of the author.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<int> GetListsCountAsync(string olid);
    }
}
