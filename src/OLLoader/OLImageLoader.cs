using OpenLibraryNET.Utility;
using System.Text;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Covers and AuthorPhotos API.
    /// </summary>
    public class OLImageLoader
    {
        internal OLImageLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        /// <summary>
        /// Attempt to get cover by its ID.
        /// </summary>
        /// <param name="idType">The <see cref="CoverIdType"/> of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, byte[]?)> TryGetCoverAsync(CoverIdType idType, string id, ImageSize size)
            => await TryGetCoverAsync(idType.GetString(), id, size.GetString());
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
        public async Task<byte[]> GetCoverAsync(CoverIdType idType, string id, ImageSize size)
            => await GetCoverAsync(idType.GetString(), id, size.GetString());

        /// <summary>
        /// Attempt to get cover by its key.
        /// </summary>
        /// <param name="key">The key of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, byte[]?)> TryGetCoverAsync(string key)
            => await TryGetCoverAsync(_client, key);
        /// <summary>
        /// Get cover by its key.
        /// </summary>
        /// <param name="key">The key of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<byte[]> GetCoverAsync(string key)
            => await GetCoverAsync(_client, key);

        /// <summary>
        /// Attempt to get cover by its ID.
        /// </summary>
        /// <param name="idType">The type of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, byte[]?)> TryGetCoverAsync(string idType, string id, string size)
            => await TryGetCoverAsync(_client, idType + "/" + id + "-" + size);
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
        public async Task<byte[]> GetCoverAsync(string idType, string id, string size)
            => await GetCoverAsync(_client, idType + "/" + id + "-" + size);

        /// <summary>
        /// Attempt to get an author's photo by its ID.
        /// </summary>
        /// <param name="idType">The <see cref="AuthorPhotoIdType"/> of ID.</param>
        /// <param name="id">The ID of the photo.</param>
        /// <param name="size">The size of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(AuthorPhotoIdType idType, string id, ImageSize size)
            => await TryGetAuthorPhotoAsync(idType.GetString(), id, size.GetString());
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
        public async Task<byte[]> GetAuthorPhotoAsync(AuthorPhotoIdType idType, string id, ImageSize size)
            => await GetAuthorPhotoAsync(idType.GetString(), id, size.GetString());

        /// <summary>
        /// Attempt to get an author's photo by its key.
        /// </summary>
        /// <param name="key">The key of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(string key)
            => await TryGetAuthorPhotoAsync(_client, key);
        /// <summary>
        /// Get an author's photo by its key.
        /// </summary>
        /// <param name="key">The key of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<byte[]> GetAuthorPhotoAsync(string key)
            => await GetAuthorPhotoAsync(_client, key);

        /// <summary>
        /// Attempt to get an author's photo by its ID.
        /// </summary>
        /// <param name="idType">The type of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(string idType, string id, string size)
            => await TryGetAuthorPhotoAsync(_client, idType + "/" + id + "-" + size);
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
        public async Task<byte[]> GetAuthorPhotoAsync(string idType, string id, string size)
            => await GetAuthorPhotoAsync(_client, idType + "/" + id + "-" + size);

        /// <summary>
        /// Attempt to get cover by its ID.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The <see cref="CoverIdType"/> of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, byte[]?)> TryGetCoverAsync(HttpClient client, CoverIdType idType, string id, ImageSize size)
            => await TryGetCoverAsync(client, idType.GetString(), id, size.GetString());
        /// <summary>
        /// Get cover by its ID.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The <see cref="CoverIdType"/> of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<byte[]> GetCoverAsync(HttpClient client, CoverIdType idType, string id, ImageSize size)
            => await GetCoverAsync(client, idType.GetString(), id, size.GetString());

        /// <summary>
        /// Attempt to get cover by its ID.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The type of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, byte[]?)> TryGetCoverAsync(HttpClient client, string idType, string id, string size)
            => await TryGetCoverAsync(client, idType + "/" + id + "-" + size);
        /// <summary>
        /// Get cover by its ID.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The type of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<byte[]> GetCoverAsync(HttpClient client, string idType, string id, string size)
            => await GetCoverAsync(client, idType + "/" + id + "-" + size);

        /// <summary>
        /// Attempt to get cover by its key.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="key">The key of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, byte[]?)> TryGetCoverAsync(HttpClient client, string key)
        {
            try { return (true, await GetCoverAsync(client, key)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get cover by its key.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="key">The key of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<byte[]> GetCoverAsync(HttpClient client, string key)
        {
            return await client.GetByteArrayAsync(OpenLibraryUtility.BuildCoversUri
            (
                key,
                new KeyValuePair<string, string>("default", "false")
            ));
        }

        /// <summary>
        /// Attempt to get an author's photo by its ID.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The <see cref="AuthorPhotoIdType"/> of ID.</param>
        /// <param name="id">The ID of the photo.</param>
        /// <param name="size">The size of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(HttpClient client, AuthorPhotoIdType idType, string id, ImageSize size)
            => await TryGetAuthorPhotoAsync(client, idType.GetString(), id, size.GetString());
        /// <summary>
        /// Get an author's photo by its ID.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The <see cref="AuthorPhotoIdType"/> of ID.</param>
        /// <param name="id">The ID of the photo.</param>
        /// <param name="size">The size of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<byte[]> GetAuthorPhotoAsync(HttpClient client, AuthorPhotoIdType idType, string id, ImageSize size)
            => await GetAuthorPhotoAsync(client, idType.GetString(), id, size.GetString());

        /// <summary>
        /// Attempt to get an author's photo by its ID.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The type of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(HttpClient client, string idType, string id, string size)
            => await TryGetAuthorPhotoAsync(client, idType + "/" + id + "-" + size);
        /// <summary>
        /// Get an author's photo by its ID.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="idType">The type of ID.</param>
        /// <param name="id">The ID of the cover.</param>
        /// <param name="size">The size of the cover.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<byte[]> GetAuthorPhotoAsync(HttpClient client, string idType, string id, string size)
            => await GetAuthorPhotoAsync(client, idType + "/" + id + "-" + size);

        /// <summary>
        /// Attempt to get an author's photo by its key.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="key">The key of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(HttpClient client, string key)
        {
            try { return (true, await GetAuthorPhotoAsync(client, key)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get an author's photo by its key.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="key">The key of the photo.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<byte[]> GetAuthorPhotoAsync(HttpClient client, string key)
        {
            return await client.GetByteArrayAsync(OpenLibraryUtility.BuildAuthorPhotosUri
            (
                key,
                new KeyValuePair<string, string>("default", "false")
            ));
        }
    }

}
