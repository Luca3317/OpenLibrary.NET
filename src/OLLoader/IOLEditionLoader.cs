using Newtonsoft.Json.Linq;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Editions API.
    /// </summary>
    public interface IOLEditionLoader
    {
        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLEditionData?)> TryGetDataAsync(string id, BookIdType? idType = null);

        /// <summary>
        /// Get data about an edition.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLEditionData?> GetDataAsync(string id, BookIdType? idType = null);

        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLEditionData?)> TryGetDataByOLIDAsync(string olid);

        /// <summary>
        /// Get data about an edition.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLEditionData?> GetDataByOLIDAsync(string olid);

        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="isbn">The ISBN of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLEditionData?)> TryGetDataByISBNAsync(string isbn);

        /// <summary>
        /// Get data about an edition.
        /// </summary>
        /// <param name="isbn">The ISBN of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLEditionData?> GetDataByISBNAsync(string isbn);

        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLEditionData?)> TryGetDataByBibkeyAsync(
            string id,
            BookIdType? idType = null
        );

        /// <summary>
        /// Get data about an edition.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLEditionData?> GetDataByBibkeyAsync(string id, BookIdType? idType = null);

        /// <summary>
        /// Attempt to get data about a variable amount of editions.
        /// </summary>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLEditionData[]?)> TryGetDataByBibkeyAsync(params string[] bibkeys);

        /// <summary>
        /// Get data about a variable amount of editions.
        /// </summary>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLEditionData[]?> GetDataByBibkeyAsync(params string[] bibkeys);

        /// <summary>
        /// Attempt to get an edition's ViewAPI.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(
            string id,
            BookIdType? idType = null
        );

        /// <summary>
        /// Get an edition's ViewAPI.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLBookViewAPI?> GetViewAPIAsync(string id, BookIdType? idType = null);

        /// <summary>
        /// Attempt to get an edition's ViewAPI with optional details.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="details">Indicates whether details should be included in the returned <see cref="OLBookViewAPI"/>.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(
            string id,
            bool details = false,
            BookIdType? idType = null
        );

        /// <summary>
        /// Get an edition's ViewAPI with optional details.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="details">Indicates whether details should be included in the returned <see cref="OLBookViewAPI"/>.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLBookViewAPI?> GetViewAPIAsync(
            string id,
            bool details = false,
            BookIdType? idType = null
        );

        /// <summary>
        /// Attempt to get ViewAPIs of a variable amount of editions.
        /// </summary>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLBookViewAPI[]?)> TryGetViewAPIAsync(params string[] bibkeys);

        /// <summary>
        /// Get ViewAPIs of a variable amount of editions.
        /// </summary>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLBookViewAPI[]?> GetViewAPIAsync(params string[] bibkeys);

        /// <summary>
        /// Attempt to get detailed ViewAPIs of a variable amount of editions.
        /// </summary>
        /// <param name="details">Indicates whether details should be included in the returned <see cref="OLBookViewAPI"/>s.</param>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLBookViewAPI[]?)> TryGetViewAPIAsync(
            bool details,
            params string[] bibkeys
        );

        /// <summary>
        /// Get detailed ViewAPIs of a variable amount of editions.
        /// </summary>
        /// <param name="details">Indicates whether details should be included in the returned <see cref="OLBookViewAPI"/>s.</param>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLBookViewAPI[]?> GetViewAPIAsync(bool details, params string[] bibkeys);

        /// <summary>
        /// Attempt to get data about lists an edition is included in.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLListData[]?)> TryGetListsAsync(
            string olid,
            params KeyValuePair<string, string>[] parameters
        );

        /// <summary>
        /// Get data about lists an edition is included in.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
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
        /// Attempt to get amount of lists an edition is included in.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, int?)> TryGetListsCountAsync(string olid);

        /// <summary>
        /// Get amount of lists an edition is included in.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<int> GetListsCountAsync(string olid);
    }
}
