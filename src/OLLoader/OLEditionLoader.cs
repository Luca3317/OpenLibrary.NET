using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;
using System.Text.RegularExpressions;


namespace OpenLibraryNET.Loader
{
    public class OLEditionLoader
    {
        internal OLEditionLoader(HttpClient client) => _client = client;

        private HttpClient _client;

        public async Task<(bool, OLEditionData?)> TryGetDataAsync(string id, EditionIdType? editionIdType = null)
            => await TryGetDataAsync(_client, id, editionIdType);
        public async Task<OLEditionData?> GetDataAsync(string id, EditionIdType? editionIdType = null)
            => await GetDataAsync(_client, id, editionIdType);

        public async Task<(bool, OLEditionData?)> TryGetDataByOLIDAsync(string id)
            => await TryGetDataByOLIDAsync(_client, id);
        public async Task<OLEditionData?> GetDataByOLIDAsync(string id)
            => await GetDataByOLIDAsync(_client, id);

        public async Task<(bool, OLEditionData?)> TryGetDataByISBNAsync(string isbn)
            => await TryGetDataByISBNAsync(_client, isbn);
        public async Task<OLEditionData?> GetDataByISBNAsync(string id)
            => await GetDataByISBNAsync(_client, id);

        public async Task<(bool, OLEditionData?)> TryGetDataByBibkeyAsync(string id, EditionIdType? editionIdType = null)
            => await TryGetDataByBibkeyAsync(_client, id, editionIdType);
        public async Task<OLEditionData?> GetDataByBibkeyAsync(string id, EditionIdType? editionIdType = null)
            => await GetDataByBibkeyAsync(_client, id, editionIdType);

        public async Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(string id, EditionIdType? editionIdType = null)
            => await TryGetViewAPIAsync(_client, id, false, editionIdType);
        public async Task<OLBookViewAPI?> GetViewAPIAsync(string id, EditionIdType? editionIdType = null)
            => await GetViewAPIAsync(_client, id, false, editionIdType);

        public async Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(string id, bool details = false, EditionIdType? editionIdType = null)
            => await TryGetViewAPIAsync(_client, id, details, editionIdType);
        public async Task<OLBookViewAPI?> GetViewAPIAsync(string id, bool details = false, EditionIdType? editionIdType = null)
            => await GetViewAPIAsync(_client, id, details, editionIdType);

        public async Task<(bool, OLListData[]?)> TryGetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetListsAsync(_client, id, parameters);
        public async Task<OLListData[]?> GetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
            => await GetListsAsync(_client, id, parameters);

        public async Task<(bool, int?)> TryGetListsCountAsync(string id)
            => await TryGetListsCountAsync(_client, id);
        public async Task<int> GetListsCountAsync(string id)
            => await GetListsCountAsync(_client, id);

        public async static Task<(bool, OLEditionData?)> TryGetDataAsync(HttpClient client, string id, EditionIdType? editionIdType = null)
        {
            try { return (true, await GetDataAsync(client, id, editionIdType)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData?> GetDataAsync(HttpClient client, string id, EditionIdType? editionIdType = null)
        {
            if (editionIdType == null) editionIdType = InferEditionIdType(id);
            switch (editionIdType)
            {
                case EditionIdType.OLID: return await GetDataByOLIDAsync(client, id);
                case EditionIdType.ISBN: return await GetDataByISBNAsync(client, id);
                case EditionIdType.LCCN: return await GetDataByBibkeyAsync(client, "LCCN:" + GetPureID(id));
                case EditionIdType.OCLC: return await GetDataByBibkeyAsync(client, "OCLC:" + GetPureID(id));
                default: return await GetDataByBibkeyAsync(client, id);
            }
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

        public async static Task<(bool, OLEditionData?)> TryGetDataByBibkeyAsync(HttpClient client, string id, EditionIdType? editionIdType = null)
        {
            try { return (true, await GetDataByBibkeyAsync(client, id, editionIdType)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData?> GetDataByBibkeyAsync(HttpClient client, string id, EditionIdType? editionIdType = null)
        {
            if (editionIdType == null) editionIdType = InferEditionIdType(id);
            switch (editionIdType)
            {
                case EditionIdType.OLID: id = "OLID:" + GetPureID(id); break;
                case EditionIdType.ISBN: id = "ISBN:" + GetPureID(id); break;
                case EditionIdType.LCCN: id = "LCCN:" + GetPureID(id); break;
                case EditionIdType.OCLC: id = "OCLC:" + GetPureID(id); break;
            }

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

        public async static Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(HttpClient client, string id, EditionIdType? editionIdType = null)
            => await TryGetViewAPIAsync(client, id, false, editionIdType);
        public async static Task<OLBookViewAPI?> GetViewAPIAsync(HttpClient client, string id, EditionIdType? editionIdType = null)
            => await GetViewAPIAsync(client, id, false, editionIdType);

        public async static Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(HttpClient client, string id, bool details = false, EditionIdType? editionIdType = null)
        {
            try { return (true, await GetViewAPIAsync(client, id, details, editionIdType)); }
            catch { return (false, null); }
        }
        public async static Task<OLBookViewAPI?> GetViewAPIAsync(HttpClient client, string id, bool details = false, EditionIdType? editionIdType = null)
        {
            if (editionIdType == null) editionIdType = InferEditionIdType(id);
            switch (editionIdType)
            {
                case EditionIdType.OLID: id = "OLID:" + GetPureID(id); break;
                case EditionIdType.ISBN: id = "ISBN:" + GetPureID(id); break;
                case EditionIdType.LCCN: id = "LCCN:" + GetPureID(id); break;
                case EditionIdType.OCLC: id = "OCLC:" + GetPureID(id); break;
            }

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

        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(HttpClient client, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(client, id)); }
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

        private static string GetPureID(string id) => Regex.Match(id, "(?<=:)[^:]*$|^[^:]*$").ToString();
        private static string GetIDPrefix(string id) => Regex.Match(id, "^[^:]*").ToString();

        private static EditionIdType? InferEditionIdType(string id)
        {
            string pureID = GetPureID(id);
            string idPrefix = GetIDPrefix(id);

            if (idPrefix != null && idPrefix != "")
            {
                switch (idPrefix)
                {
                    case "ISBN": return EditionIdType.ISBN;
                    case "LCCN": return EditionIdType.LCCN;
                    case "OCLC": return EditionIdType.OCLC;
                    case "OLID": return EditionIdType.OLID;
                }
            }
            if (Regex.Match(pureID, "^OL[0-9]*[A-Z]").Success)
                return EditionIdType.OLID;

            return null;
        }
    }

}
