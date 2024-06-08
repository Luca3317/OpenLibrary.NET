using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's RecentChanges API.
    /// </summary>
    public interface IOLRecentChangesLoader
    {
        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(
            string kind = "",
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLRecentChangesData[]?> GetRecentChangesAsync(
            string kind = "",
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="year">The year the change was made in. Format: "YYYY".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(
            string year,
            string kind = "",
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLRecentChangesData[]?> GetRecentChangesAsync(
            string year,
            string kind = "",
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="month">The month the change was made in. Format = "MM".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(
            string year,
            string month,
            string kind = "",
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLRecentChangesData[]?> GetRecentChangesAsync(
            string year,
            string month,
            string kind = "",
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get recent changes made to OpenLibrary's data.
        /// </summary>
        /// <param name="year">The year the change was made in. Format = "YYYY".</param>
        /// <param name="month">The month the change was made in. Format = "MM".</param>
        /// <param name="day">The day the change was made in. Format = "DD".</param>
        /// <param name="kind">The kind of change made.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(
            string year,
            string month,
            string day,
            string kind = "",
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLRecentChangesData[]?> GetRecentChangesAsync(
            string year,
            string month,
            string day,
            string kind = "",
            params KeyValuePair<string, string>[] parameters
        );
    }
}
