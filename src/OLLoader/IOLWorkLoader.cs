using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Works API.
    /// </summary>
    public interface IOLWorkLoader
    {
        /// <summary>
        /// Attempt to get data about a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLWorkData?)> TryGetDataAsync(string olid);

        /// <summary>
        /// Get data about a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLWorkData?> GetDataAsync(string olid);

        /// <summary>
        /// Attempt to get ratings of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLRatingsData?)> TryGetRatingsAsync(string olid);

        /// <summary>
        /// Get ratings of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLRatingsData?> GetRatingsAsync(string olid);

        /// <summary>
        /// Attempt to get bookshelves of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLBookshelvesData?)> TryGetBookshelvesAsync(string olid);

        /// <summary>
        /// Get bookshelves of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLBookshelvesData?> GetBookshelvesAsync(string olid);

        /// <summary>
        /// Attempt to get editions of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLEditionData[]?)> TryGetEditionsAsync(
            string olid,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLEditionData[]?> GetEditionsAsync(
            string olid,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get amount of editions of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, int?)> TryGetEditionsCountAsync(string olid);

        /// <summary>
        /// Get amount of editions of a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<int> GetEditionsCountAsync(string olid);

        /// <summary>
        /// Attempt to get lists including a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <param name="parameters">The query parameters.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLListData[]?)> TryGetListsAsync(
            string olid,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLListData[]?> GetListsAsync(
            string olid,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get amount of lists including a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, int?)> TryGetListsCountAsync(string olid);

        /// <summary>
        /// Get amount of lists including a work.
        /// </summary>
        /// <param name="olid">The OLID of the work.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<int> GetListsCountAsync(string olid);
    }
}
