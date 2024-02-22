using Newtonsoft.Json.Linq;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Editions API.
    /// </summary>
    public class OLEditionLoader
    {
        internal OLEditionLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLEditionData?)> TryGetDataAsync(string id, BookIdType? idType = null)
            => await TryGetDataAsync(_client, id, idType);
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
        public async Task<OLEditionData?> GetDataAsync(string id, BookIdType? idType = null)
            => await GetDataAsync(_client, id, idType);

        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLEditionData?)> TryGetDataByOLIDAsync(string olid)
            => await TryGetDataByOLIDAsync(_client, olid);
        /// <summary>
        /// Get data about an edition.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLEditionData?> GetDataByOLIDAsync(string olid)
            => await GetDataByOLIDAsync(_client, olid);

        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="isbn">The ISBN of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLEditionData?)> TryGetDataByISBNAsync(string isbn)
            => await TryGetDataByISBNAsync(_client, isbn);
        /// <summary>
        /// Get data about an edition.
        /// </summary>
        /// <param name="isbn">The ISBN of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLEditionData?> GetDataByISBNAsync(string isbn)
            => await GetDataByISBNAsync(_client, isbn);

        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLEditionData?)> TryGetDataByBibkeyAsync(string id, BookIdType? idType = null)
            => await TryGetDataByBibkeyAsync(_client, id, idType);
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
        public async Task<OLEditionData?> GetDataByBibkeyAsync(string id, BookIdType? idType = null)
            => await GetDataByBibkeyAsync(_client, id, idType);

        /// <summary>
        /// Attempt to get data about a variable amount of editions.
        /// </summary>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLEditionData[]?)> TryGetDataByBibkeyAsync(params string[] bibkeys)
            => await TryGetDataByBibkeyAsync(_client, bibkeys);
        /// <summary>
        /// Get data about a variable amount of editions.
        /// </summary>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLEditionData[]?> GetDataByBibkeyAsync(params string[] bibkeys)
            => await GetDataByBibkeyAsync(_client, bibkeys);

        /// <summary>
        /// Attempt to get an edition's ViewAPI.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(string id, BookIdType? idType = null)
            => await TryGetViewAPIAsync(_client, id, false, idType);
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
        public async Task<OLBookViewAPI?> GetViewAPIAsync(string id, BookIdType? idType = null)
            => await GetViewAPIAsync(_client, id, false, idType);

        /// <summary>
        /// Attempt to get an edition's ViewAPI with optional details.
        /// </summary>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="details">Indicates whether details should be included in the returned <see cref="OLBookViewAPI"/>.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(string id, bool details = false, BookIdType? idType = null)
            => await TryGetViewAPIAsync(_client, id, details, idType);
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
        public async Task<OLBookViewAPI?> GetViewAPIAsync(string id, bool details = false, BookIdType? idType = null)
            => await GetViewAPIAsync(_client, id, details, idType);

        /// <summary>
        /// Attempt to get ViewAPIs of a variable amount of editions.
        /// </summary>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLBookViewAPI[]?)> TryGetViewAPIAsync(params string[] bibkeys)
            => await TryGetViewAPIAsync(_client, false, bibkeys);
        /// <summary>
        /// Get ViewAPIs of a variable amount of editions.
        /// </summary>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLBookViewAPI[]?> GetViewAPIAsync(params string[] bibkeys)
            => await GetViewAPIAsync(_client, false, bibkeys);

        /// <summary>
        /// Attempt to get detailed ViewAPIs of a variable amount of editions.
        /// </summary>
        /// <param name="details">Indicates whether details should be included in the returned <see cref="OLBookViewAPI"/>s.</param>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLBookViewAPI[]?)> TryGetViewAPIAsync(bool details, params string[] bibkeys)
            => await TryGetViewAPIAsync(_client, details, bibkeys);
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
        public async Task<OLBookViewAPI[]?> GetViewAPIAsync(bool details, params string[] bibkeys)
            => await GetViewAPIAsync(_client, details, bibkeys);

        /// <summary>
        /// Attempt to get data about lists an edition is included in.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLListData[]?)> TryGetListsAsync(string olid, params KeyValuePair<string, string>[] parameters)
            => await TryGetListsAsync(_client, olid, parameters);
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
        public async Task<OLListData[]?> GetListsAsync(string olid, params KeyValuePair<string, string>[] parameters)
            => await GetListsAsync(_client, olid, parameters);

        /// <summary>
        /// Attempt to get amount of lists an edition is included in.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, int?)> TryGetListsCountAsync(string olid)
            => await TryGetListsCountAsync(_client, olid);
        /// <summary>
        /// Get amount of lists an edition is included in.
        /// </summary>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<int> GetListsCountAsync(string olid)
            => await GetListsCountAsync(_client, olid);

        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLEditionData?)> TryGetDataAsync(HttpClient client, string id, BookIdType? idType = null)
        {
            try { return (true, await GetDataAsync(client, id, idType)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about an edition.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLEditionData?> GetDataAsync(HttpClient client, string id, BookIdType? idType = null)
        {
            if (idType == null)
            {
                return await GetDataByBibkeyAsync(client, id);
            }

            return await GetDataByBibkeyAsync(client, OpenLibraryUtility.SetBibkeyPrefix(idType.Value, id));
        }

        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLEditionData?)> TryGetDataByOLIDAsync(HttpClient client, string olid)
        {
            try { return (true, await GetDataByOLIDAsync(client, olid)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about an edition.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLEditionData?> GetDataByOLIDAsync(HttpClient client, string olid)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData>
            (
                client,
                OpenLibraryUtility.BuildEditionsUri
                (
                    olid
                )
            );
        }
        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="isbn">The ISBN of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLEditionData?)> TryGetDataByISBNAsync(HttpClient client, string isbn)
        {
            try { return (true, await GetDataByISBNAsync(client, isbn)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about an edition.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="isbn">The ISBN of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLEditionData?> GetDataByISBNAsync(HttpClient client, string isbn)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData>
            (
                client,
                OpenLibraryUtility.BuildISBNUri
                (
                    isbn
                )
            );
        }

        /// <summary>
        /// Attempt to get data about an edition.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLEditionData?)> TryGetDataByBibkeyAsync(HttpClient client, string id, BookIdType? idType = null)
        {
            try { return (true, await GetDataByBibkeyAsync(client, id, idType)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about an edition.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLEditionData?> GetDataByBibkeyAsync(HttpClient client, string id, BookIdType? idType = null)
        {
            if (idType != null) id = OpenLibraryUtility.SetBibkeyPrefix(idType.Value, id);

            return await OpenLibraryUtility.LoadAsync<OLEditionData>
            (
                client,
                OpenLibraryUtility.BuildBooksUri
                (
                    "", "",
                    new KeyValuePair<string, string>("bibkeys", id),
                    new KeyValuePair<string, string>("jscmd", "data"),
                    new KeyValuePair<string, string>("format", "json")
                ),
                id
            );
        }

        /// <summary>
        /// Attempt to get data about a variable amount of editions.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLEditionData[]?)> TryGetDataByBibkeyAsync(HttpClient client, params string[] bibkeys)
        {
            try { return (true, await GetDataByBibkeyAsync(client, bibkeys)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about a variable amount of editions.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLEditionData[]?> GetDataByBibkeyAsync(HttpClient client, params string[] bibkeys)
        {
            string response = await OpenLibraryUtility.RequestAsync
            (
                client,
                OpenLibraryUtility.BuildBooksUri
                (
                    bibkeys, "",
                    new KeyValuePair<string, string>("jscmd", "data"),
                    new KeyValuePair<string, string>("format", "json")
                )
            );

            JToken token = JToken.Parse(response);
            List<OLEditionData> data = new();
            foreach (string bibkey in bibkeys)
            {
                data.Add(token[bibkey]!.ToObject<OLEditionData>()!);
            }

            return data.ToArray();
        }

        /// <summary>
        /// Attempt to get an edition's ViewAPI.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(HttpClient client, string id, BookIdType? idType = null)
            => await TryGetViewAPIAsync(client, id, false, idType);
        /// <summary>
        /// Get an edition's ViewAPI.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLBookViewAPI?> GetViewAPIAsync(HttpClient client, string id, BookIdType? idType = null)
            => await GetViewAPIAsync(client, id, false, idType);

        /// <summary>
        /// Attempt to get an edition's ViewAPI with optional details.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="details">Indicates whether to include details in the returned <see cref="OLBookViewAPI"/></param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(HttpClient client, string id, bool details = false, BookIdType? idType = null)
        {
            try { return (true, await GetViewAPIAsync(client, id, details, idType)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get an edition's ViewAPI with optional details.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="id">The ID of the edition.</param>
        /// <param name="details">Indicates whether to include details in the returned <see cref="OLBookViewAPI"/></param>
        /// <param name="idType">The <see cref="BookIdType"/> of the ID.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLBookViewAPI?> GetViewAPIAsync(HttpClient client, string id, bool details = false, BookIdType? idType = null)
        {
            if (idType != null) id = OpenLibraryUtility.SetBibkeyPrefix(idType.Value, id);

            return await OpenLibraryUtility.LoadAsync<OLBookViewAPI>
            (
                client,
                OpenLibraryUtility.BuildBooksUri
                (
                    "", "",
                    new KeyValuePair<string, string>("bibkeys", id),
                    new KeyValuePair<string, string>("jscmd", details ? "details" : "viewapi"),
                    new KeyValuePair<string, string>("format", "json")
                ),
                id
            );
        }

        /// <summary>
        /// Attempt to get ViewAPIs of a variable amount of editions.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLBookViewAPI[]?)> TryGetViewAPIAsync(HttpClient client, params string[] bibkeys)
            => await TryGetViewAPIAsync(client, false, bibkeys);
        /// <summary>
        /// Get ViewAPIs of a variable amount of editions.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLBookViewAPI[]?> GetViewAPIAsync(HttpClient client, params string[] bibkeys)
            => await GetViewAPIAsync(client, false, bibkeys);

        /// <summary>
        /// Attempt to get ViewAPIs of a variable amount of editions with optional details.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="details">Indicates whether to include details in the returned <see cref="OLBookViewAPI"/></param>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLBookViewAPI[]?)> TryGetViewAPIAsync(HttpClient client, bool details = false, params string[] bibkeys)
        {
            try { return (true, await GetViewAPIAsync(client, details, bibkeys)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get ViewAPIs of a variable amount of editions with optional details.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="details">Indicates whether to include details in the returned <see cref="OLBookViewAPI"/></param>
        /// <param name="bibkeys">The editions' bibkeys. Must already be in bibkey format (see <see cref="OpenLibraryUtility.SetBibkeyPrefix(BookIdType, string)"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLBookViewAPI[]?> GetViewAPIAsync(HttpClient client, bool details = false, params string[] bibkeys)
        {
            string response = await OpenLibraryUtility.RequestAsync
            (
                client,
                OpenLibraryUtility.BuildBooksUri
                (
                    bibkeys, "",
                    new KeyValuePair<string, string>("jscmd", details ? "details" : "viewapi"),
                    new KeyValuePair<string, string>("format", "json")
                )
            );

            JToken token = JToken.Parse(response);
            List<OLBookViewAPI> data = new List<OLBookViewAPI>();
            foreach (string bibkey in bibkeys)
            {
                data.Add(token[bibkey]!.ToObject<OLBookViewAPI>()!);
            }

            return data.ToArray();
        }

        /// <summary>
        /// Attempt to get data about lists an edition is included in.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the edition.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(HttpClient client, string olid, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(client, olid, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about lists an edition is included in.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the edition.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLListData[]?> GetListsAsync(HttpClient client, string olid, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                client,
                OpenLibraryUtility.BuildEditionsUri
                (
                    olid,
                    "lists",
                    parameters
                ),
                "entries"
            );
        }

        /// <summary>
        /// Attempt to get amount of lists an edition is included in.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, int?)> TryGetListsCountAsync(HttpClient client, string olid)
        {
            try { return (true, await GetListsCountAsync(client, olid)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get amount of lists an edition is included in.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="olid">The OLID of the edition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<int> GetListsCountAsync(HttpClient client, string olid)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                client,
                OpenLibraryUtility.BuildEditionsUri
                (
                    olid,
                    "lists",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
            );
        }
    }

}
