using Newtonsoft.Json.Linq;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Partner API.
    /// </summary>
    public interface IOLPartnerLoader
    {
        /// <summary>
        /// Attempt to get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="idType">The <see cref="PartnerIdType"/> of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLPartnerData?)> TryGetDataAsync(PartnerIdType idType, string id);

        /// <summary>
        /// Get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="idType">The <see cref="PartnerIdType"/> of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLPartnerData?> GetDataAsync(PartnerIdType idType, string id);

        /// <summary>
        /// Attempt to get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLPartnerData?)> TryGetDataAsync(string idType, string id);

        /// <summary>
        /// Get data about an online-readable or borrowable book.
        /// </summary>
        /// <param name="idType">The type of the ID.</param>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLPartnerData?> GetDataAsync(string idType, string id);

        /// <summary>
        /// Attempt to get data about multiple online-readable or borrowable books.
        /// </summary>
        /// <param name="ids">The bibkeys of the books. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(PartnerIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, OLPartnerData[]?)> TryGetMulitDataAsync(params string[] ids);

        /// <summary>
        /// Get data about multiple online-readable or borrowable books.
        /// </summary>
        /// <param name="ids">The bibkeys of the books. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(PartnerIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<OLPartnerData[]?> GetMultiDataAsync(params string[] ids);
    }
}
