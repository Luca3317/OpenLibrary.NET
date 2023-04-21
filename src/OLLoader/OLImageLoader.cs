﻿using OpenLibraryNET.Utility;
using System.Text;

namespace OpenLibraryNET.Loader
{
    public class OLImageLoader
    {
        internal OLImageLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        public async Task<(bool, byte[]?)> TryGetCoverAsync(CoverIdType idType, string id, ImageSize size)
            => await TryGetCoverAsync(idType.ToString(), id, size.GetString());
        public async Task<byte[]> GetCoverAsync(CoverIdType idType, string id, ImageSize size)
            => await GetCoverAsync(idType.ToString(), id, size.GetString());

        public async Task<(bool, byte[]?)> TryGetCoverAsync(string id)
            => await TryGetCoverAsync(_client, id);
        public async Task<byte[]> GetCoverAsync(string id)
            => await GetCoverAsync(_client, id);

        public async Task<(bool, byte[]?)> TryGetCoverAsync(string idType, string id, string size)
            => await TryGetCoverAsync(_client, idType + "/" + id + "-" + size);
        public async Task<byte[]> GetCoverAsync(string idType, string id, string size)
            => await GetCoverAsync(_client, idType + "/" + id + "-" + size);

        public async Task<byte[]> GetAuthorPhotoAsync(AuthorPhotoIdType idType, string id, ImageSize size)
            => await GetAuthorPhotoAsync(idType.ToString(), id, size.GetString());
        public async Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(AuthorPhotoIdType idType, string id, ImageSize size)
            => await TryGetAuthorPhotoAsync(idType.ToString(), id, size.GetString());

        public async Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(string id)
            => await TryGetAuthorPhotoAsync(_client, id);
        public async Task<byte[]> GetAuthorPhotoAsync(string id)
            => await GetAuthorPhotoAsync(_client, id);

        public async Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(string idType, string id, string size)
            => await TryGetAuthorPhotoAsync(_client, idType + "/" + id + "-" + size);
        public async Task<byte[]> GetAuthorPhotoAsync(string idType, string id, string size)
            => await GetAuthorPhotoAsync(_client, idType + "/" + id + "-" + size);


        public async static Task<(bool, byte[]?)> TryGetCoverAsync(HttpClient client, CoverIdType idType, string id, ImageSize size)
            => await TryGetCoverAsync(client, idType.ToString(), id, size.GetString());
        public async static Task<byte[]> GetCoverAsync(HttpClient client, CoverIdType idType, string id, ImageSize size)
            => await GetCoverAsync(client, idType.ToString(), id, size.GetString());

        public async static Task<(bool, byte[]?)> TryGetCoverAsync(HttpClient client, string idType, string id, string size)
            => await TryGetCoverAsync(client, idType + "/" + id + "-" + size);
        public async static Task<byte[]> GetCoverAsync(HttpClient client, string idType, string id, string size)
            => await GetCoverAsync(client, idType + "/" + id + "-" + size);

        public async static Task<(bool, byte[]?)> TryGetCoverAsync(HttpClient client, string id)
        {
            try { return (true, await GetCoverAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<byte[]> GetCoverAsync(HttpClient client, string id)
        {
            return Encoding.ASCII.GetBytes
            (
                await OpenLibraryUtility.RequestAsync(OpenLibraryUtility.BuildCoversUri
                (
                    id,
                    new KeyValuePair<string, string>("default", "false")
                ), client: client
                )
            );
        }


        public async static Task<byte[]> GetAuthorPhotoAsync(HttpClient client, AuthorPhotoIdType idType, string id, ImageSize size)
            => await GetAuthorPhotoAsync(client, idType.ToString(), id, size.GetString());
        public async static Task<(bool,byte[]?)> TryGetAuthorPhotoAsync(HttpClient client, AuthorPhotoIdType idType, string id, ImageSize size)
            => await TryGetAuthorPhotoAsync(client, idType.ToString(), id, size.GetString());

        public async static Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(HttpClient client, string idType, string id, string size)
            => await TryGetAuthorPhotoAsync(client, idType + "/" + id + "-" + size);
        public async static Task<byte[]> GetAuthorPhotoAsync(HttpClient client, string idType, string id, string size)
            => await GetAuthorPhotoAsync(client, idType + "/" + id + "-" + size);

        public async static Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(HttpClient client, string id)
        {
            try { return (true, await GetAuthorPhotoAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<byte[]> GetAuthorPhotoAsync(HttpClient client, string id)
        {
            return Encoding.ASCII.GetBytes
            (
                await OpenLibraryUtility.RequestAsync(OpenLibraryUtility.BuildAuthorPhotosUri
                (
                    id,
                    parameters: new KeyValuePair<string, string>("default", "false")
                ), client: client
                )
            );
        }
    }

}
