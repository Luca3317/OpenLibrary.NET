using Newtonsoft.Json;
using OpenLibraryNET.Data;
using OpenLibraryNET.OLData;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Lists API.
    /// </summary>
    public interface IOLListLoader
    {
        /// <summary>
        /// Attempt to get data about all of a user's lists.
        /// </summary>
        /// <param name="username">The user to get the lists of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLListData[]?)> TryGetUserListsAsync(string? username = null);

        /// <summary>
        /// Get data about all of a user's lists.
        /// </summary>
        /// <param name="username">The user to get the lists of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public Task<OLListData[]?> GetUserListsAsync(string? username = null);

        /// <summary>
        /// Attempt to get data about a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLListData?)> TryGetListAsync(string username, string id);

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
        public Task<OLListData?> GetListAsync(string username, string id);

        /// <summary>
        /// Attempt to get data about a list of the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLListData?)> TryGetListAsync(string id);

        /// <summary>
        /// Get data about a list of the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public Task<OLListData?> GetListAsync(string id);

        /// <summary>
        /// Attempt to get editions contained in a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLEditionData[]?)> TryGetListEditionsAsync(
            string username,
            string id,
            params KeyValuePair<string, string>[] parameters
        );

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
        public Task<OLEditionData[]?> GetListEditionsAsync(
            string username,
            string id,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get editions contained in a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLEditionData[]?)> TryGetListEditionsAsync(
            string id,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Get editions contained in a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLEditionData[]?> GetListEditionsAsync(
            string id,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get subjects contained in a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLSubjectData[]?)> TryGetListSubjectsAsync(
            string username,
            string id,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Get subjects contained in a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLSubjectData[]?> GetListSubjectsAsync(
            string username,
            string id,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get subjects contained in a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLSubjectData[]?)> TryGetListSubjectsAsync(
            string id,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get subjects contained in a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLSubjectData[]?> GetListSubjectsAsync(
            string id,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Attempt to get seeds of a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLSeedData[]?)> TryGetListSeedsAsync(string username, string id);

        /// <summary>
        /// Get seeds of a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLSeedData[]?> GetListSeedsAsync(string username, string id);

        /// <summary>
        /// Attempt to get seeds of a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLSeedData[]?)> TryGetListSeedsAsync(string id);

        /// <summary>
        /// Get seeds of a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLSeedData[]?> GetListSeedsAsync(string id);
    }
}
