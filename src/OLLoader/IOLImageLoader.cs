using System.Text;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Covers and AuthorPhotos API.
    /// </summary>
    public interface IOLImageLoader
    {
        /// <summary>
        /// Attempt to get cover by its ID.
        /// </summary>
        /// <param name="idType">The <see cref="CoverIdType"/> of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, byte[]?)> TryGetCoverAsync(
            CoverIdType idType,
            string id,
            ImageSize size
        );

        /// <summary>
        /// Get cover by its ID.
        /// </summary>
        /// <param name="idType">The <see cref="CoverIdType"/> of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<byte[]> GetCoverAsync(CoverIdType idType, string id, ImageSize size);

        /// <summary>
        /// Attempt to get cover by its key.
        /// </summary>
        /// <param name="key">The key of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, byte[]?)> TryGetCoverAsync(string key);

        /// <summary>
        /// Get cover by its key.
        /// </summary>
        /// <param name="key">The key of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<byte[]> GetCoverAsync(string key);

        /// <summary>
        /// Attempt to get cover by its ID.
        /// </summary>
        /// <param name="idType">The type of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, byte[]?)> TryGetCoverAsync(string idType, string id, string size);

        /// <summary>
        /// Get cover by its ID.
        /// </summary>
        /// <param name="idType">The type of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<byte[]> GetCoverAsync(string idType, string id, string size);

        /// <summary>
        /// Attempt to get an author's photo by its ID.
        /// </summary>
        /// <param name="idType">The <see cref="AuthorPhotoIdType"/> of ID.</param>
        /// <param name="id">The ID of the photo.</param>
        /// <param name="size">The size of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(
            AuthorPhotoIdType idType,
            string id,
            ImageSize size
        ) => await TryGetAuthorPhotoAsync(idType.GetString(), id, size.GetString());

        /// <summary>
        /// Get an author's photo by its ID.
        /// </summary>
        /// <param name="idType">The <see cref="AuthorPhotoIdType"/> of ID.</param>
        /// <param name="id">The ID of the photo.</param>
        /// <param name="size">The size of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<byte[]> GetAuthorPhotoAsync(
            AuthorPhotoIdType idType,
            string id,
            ImageSize size
        );

        /// <summary>
        /// Attempt to get an author's photo by its key.
        /// </summary>
        /// <param name="key">The key of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(string key);

        /// <summary>
        /// Get an author's photo by its key.
        /// </summary>
        /// <param name="key">The key of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<byte[]> GetAuthorPhotoAsync(string key);

        /// <summary>
        /// Attempt to get an author's photo by its ID.
        /// </summary>
        /// <param name="idType">The type of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(string idType, string id, string size);

        /// <summary>
        /// Get an author's photo by its ID.
        /// </summary>
        /// <param name="idType">The type of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Task<byte[]> GetAuthorPhotoAsync(string idType, string id, string size);
    }
}
