using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's MyBooks API.
    /// </summary>
    public interface IOLMyBooksLoader
    {
        /// <summary>
        /// Attempt to get data about a user's Want-To-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLMyBooksData?)> TryGetWantToReadAsync(string? username = null);

        /// <summary>
        /// Get data about a user's Want-To-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLMyBooksData?> GetWantToReadAsync(string? username = null);

        /// <summary>
        /// Attempt to get data about a user's Currently-Reading reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLMyBooksData?)> TryGetCurrentlyReadingAsync(string? username = null);

        /// <summary>
        /// Get data about a user's Currently-Reading reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLMyBooksData?> GetCurrentlyReadingAsync(string? username = null);

        /// <summary>
        /// Attempt to get data about a user's Already-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLMyBooksData?)> TryGetAlreadyReadAsync(string? username = null);

        /// <summary>
        /// Get data about a user's Already-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLMyBooksData?> GetAlreadyReadAsync(string? username = null);

        /// <summary>
        /// Attempt to get data about a user's specified reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of.</param>
        /// <param name="id">The bookshelf id of the desired reading log.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLMyBooksData?)> TryGetReadingLogAsync(string username, BookshelfID id);

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
        public Task<OLMyBooksData?> GetReadingLogAsync(string username, BookshelfID id);

        /// <summary>
        /// Attempt to get data about a reading log of the logged in account.
        /// </summary>
        /// <param name="id">The bookshelf id of the desired reading log.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLMyBooksData?)> TryGetReadingLogAsync(BookshelfID id);

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
        public Task<OLMyBooksData?> GetReadingLogAsync(BookshelfID id);
    }
}
