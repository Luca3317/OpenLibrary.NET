using Newtonsoft.Json.Linq;
using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    public class OLEditionLoader
    {
        internal OLEditionLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        public async Task<(bool, OLEditionData?)> TryGetDataAsync(string id, EditionIdType? idType = null)
            => await TryGetDataAsync(_client, id, idType);
        public async Task<OLEditionData?> GetDataAsync(string id, EditionIdType? idType = null)
            => await GetDataAsync(_client, id, idType);

        public async Task<(bool, OLEditionData?)> TryGetDataByOLIDAsync(string id)
            => await TryGetDataByOLIDAsync(_client, id);
        public async Task<OLEditionData?> GetDataByOLIDAsync(string id)
            => await GetDataByOLIDAsync(_client, id);

        public async Task<(bool, OLEditionData?)> TryGetDataByISBNAsync(string isbn)
            => await TryGetDataByISBNAsync(_client, isbn);
        public async Task<OLEditionData?> GetDataByISBNAsync(string id)
            => await GetDataByISBNAsync(_client, id);

        public async Task<(bool, OLEditionData?)> TryGetDataByBibkeyAsync(string id, EditionIdType? idType = null)
            => await TryGetDataByBibkeyAsync(_client, id, idType);
        public async Task<OLEditionData?> GetDataByBibkeyAsync(string id, EditionIdType? idType = null)
            => await GetDataByBibkeyAsync(_client, id, idType);

        public async Task<(bool, OLEditionData[]?)> TryGetDataByBibkeyAsync(params string[] bibkeys)
            => await TryGetDataByBibkeyAsync(_client, bibkeys);
        public async Task<OLEditionData[]?> GetDataByBibkeyAsync(params string[] bibkeys)
            => await GetDataByBibkeyAsync(_client, bibkeys);

        public async Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(string id, EditionIdType? idType = null)
            => await TryGetViewAPIAsync(_client, id, false, idType);
        public async Task<OLBookViewAPI?> GetViewAPIAsync(string id, EditionIdType? idType = null)
            => await GetViewAPIAsync(_client, id, false, idType);

        public async Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(string id, bool details = false, EditionIdType? idType = null)
            => await TryGetViewAPIAsync(_client, id, details, idType);
        public async Task<OLBookViewAPI?> GetViewAPIAsync(string id, bool details = false, EditionIdType? idType = null)
            => await GetViewAPIAsync(_client, id, details, idType);

        public async Task<(bool, OLBookViewAPI[]?)> TryGetViewAPIAsync(params string[] bibkeys)
            => await TryGetViewAPIAsync(_client, false, bibkeys);
        public async Task<OLBookViewAPI[]?> GetViewAPIAsync(params string[] bibkeys)
            => await GetViewAPIAsync(_client, false, bibkeys);

        public async Task<(bool, OLBookViewAPI[]?)> TryGetViewAPIAsync(bool details, params string[] bibkeys)
            => await TryGetViewAPIAsync(_client, details, bibkeys);
        public async Task<OLBookViewAPI[]?> GetViewAPIAsync(bool details, params string[] bibkeys)
            => await GetViewAPIAsync(_client, details, bibkeys);

        public async Task<(bool, OLListData[]?)> TryGetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetListsAsync(_client, id, parameters);
        public async Task<OLListData[]?> GetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await GetListsAsync(_client, id, parameters);

        public async Task<(bool, int?)> TryGetListsCountAsync(string id)
            => await TryGetListsCountAsync(_client, id);
        public async Task<int> GetListsCountAsync(string id)
            => await GetListsCountAsync(_client, id);

        public async static Task<(bool, OLEditionData?)> TryGetDataAsync(HttpClient client, string id, EditionIdType? idType = null)
        {
            try { return (true, await GetDataAsync(client, id, idType)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData?> GetDataAsync(HttpClient client, string id, EditionIdType? idType = null)
        {
            if (idType == null)
            {
                return await GetDataByBibkeyAsync(client, id);
            }

            return await GetDataByBibkeyAsync(client, OpenLibraryUtility.SetBibkeyPrefix(idType.Value, id));
        }

        public async static Task<(bool, OLEditionData?)> TryGetDataByOLIDAsync(HttpClient client, string id)
        {
            try { return (true, await GetDataByOLIDAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData?> GetDataByOLIDAsync(HttpClient client, string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData>
            (
                OpenLibraryUtility.BuildEditionsUri
                (
                    id
                ),
                client: client
            );
        }

        public async static Task<(bool, OLEditionData?)> TryGetDataByISBNAsync(HttpClient client, string isbn)
        {
            try { return (true, await GetDataByISBNAsync(client, isbn)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData?> GetDataByISBNAsync(HttpClient client, string isbn)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData>
            (
                OpenLibraryUtility.BuildISBNUri
                (
                    isbn
                ),
                client: client
            );
        }

        public async static Task<(bool, OLEditionData?)> TryGetDataByBibkeyAsync(HttpClient client, string id, EditionIdType? idType = null)
        {
            try { return (true, await GetDataByBibkeyAsync(client, id, idType)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData?> GetDataByBibkeyAsync(HttpClient client, string id, EditionIdType? idType = null)
        {
            if (idType != null) id = OpenLibraryUtility.SetBibkeyPrefix(idType.Value, id);

            return await OpenLibraryUtility.LoadAsync<OLEditionData>
            (
                OpenLibraryUtility.BuildBooksUri
                (
                    "", "",
                    new KeyValuePair<string, string>("bibkeys", id),
                    new KeyValuePair<string, string>("jscmd", "data"),
                    new KeyValuePair<string, string>("format", "json")
                ),
                id,
                client
            );
        }

        public async static Task<(bool, OLEditionData[]?)> TryGetDataByBibkeyAsync(HttpClient client, params string[] bibkeys)
        {
            try { return (true, await GetDataByBibkeyAsync(client, bibkeys)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData[]?> GetDataByBibkeyAsync(HttpClient client, params string[] bibkeys)
        {
            string response = await OpenLibraryUtility.RequestAsync
            (
                OpenLibraryUtility.BuildBooksUri
                (
                    bibkeys, "",
                    new KeyValuePair<string, string>("jscmd", "data"),
                    new KeyValuePair<string, string>("format", "json")
                ),
                client
            );

            JToken token = JToken.Parse(response);
            List<OLEditionData> data = new();
            foreach (string bibkey in bibkeys)
            {
                data.Add(token[bibkey]!.ToObject<OLEditionData>()!);
            }

            return data.ToArray();
        }

        public async static Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(HttpClient client, string id, EditionIdType? idType = null)
            => await TryGetViewAPIAsync(client, id, false, idType);
        public async static Task<OLBookViewAPI?> GetViewAPIAsync(HttpClient client, string id, EditionIdType? idType = null)
            => await GetViewAPIAsync(client, id, false, idType);

        public async static Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(HttpClient client, string id, bool details = false, EditionIdType? idType = null)
        {
            try { return (true, await GetViewAPIAsync(client, id, details, idType)); }
            catch { return (false, null); }
        }
        public async static Task<OLBookViewAPI?> GetViewAPIAsync(HttpClient client, string id, bool details = false, EditionIdType? idType = null)
        {
            if (idType != null) id = OpenLibraryUtility.SetBibkeyPrefix(idType.Value, id);

            return await OpenLibraryUtility.LoadAsync<OLBookViewAPI>
            (
                OpenLibraryUtility.BuildBooksUri
                (
                    "", "",
                    new KeyValuePair<string, string>("bibkeys", id),
                    new KeyValuePair<string, string>("jscmd", details ? "details" : "viewapi"),
                    new KeyValuePair<string, string>("format", "json")
                ),
                id,
                client
            );
        }

        public async static Task<(bool, OLBookViewAPI[]?)> TryGetViewAPIAsync(HttpClient client, params string[] bibkeys)
            => await TryGetViewAPIAsync(client, false, bibkeys);
        public async static Task<OLBookViewAPI[]?> GetViewAPIAsync(HttpClient client, params string[] bibkeys)
            => await GetViewAPIAsync(client, false, bibkeys);

        public async static Task<(bool, OLBookViewAPI[]?)> TryGetViewAPIAsync(HttpClient client, bool details = false, params string[] bibkeys)
        {
            try { return (true, await GetViewAPIAsync(client, details, bibkeys)); }
            catch { return (false, null); }
        }
        public async static Task<OLBookViewAPI[]?> GetViewAPIAsync(HttpClient client, bool details = false, params string[] bibkeys)
        {
            string response = await OpenLibraryUtility.RequestAsync
            (
                OpenLibraryUtility.BuildBooksUri
                (
                    bibkeys, "",
                    new KeyValuePair<string, string>("jscmd", details ? "details" : "viewapi"),
                    new KeyValuePair<string, string>("format", "json")
                ),
                client
            );

            JToken token = JToken.Parse(response);
            List<OLBookViewAPI> data = new List<OLBookViewAPI>();
            foreach (string bibkey in bibkeys)
            {
                data.Add(token[bibkey]!.ToObject<OLBookViewAPI>()!);
            }

            return data.ToArray();
        }

        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(client, id, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetListsAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildEditionsUri
                (
                    id,
                    "lists",
                    parameters
                ),
                "entries",
                client
            );
        }

        public async static Task<(bool, int?)> TryGetListsCountAsync(HttpClient client, string id)
        {
            try { return (true, await GetListsCountAsync(client, id)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetListsCountAsync(HttpClient client, string id)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildEditionsUri
                (
                    id,
                    "lists",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size",
                client
            );
        }
    }

}
