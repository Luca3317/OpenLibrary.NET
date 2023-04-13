using System.Text;
using System.Text.RegularExpressions;

/*
 * Helper functions for requesting specific data from OpenLibrary, and populating OLData and OLObject records
 */

namespace OpenLibraryNET
{
    public static class OLWorkLoader
    {
        public async static Task<(bool, OLWorkData?)> TryGetDataAsync(string id)
        {
            try { return (true, await GetDataAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<OLWorkData?> GetDataAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLWorkData>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Books_Works, id)
            );
        }

        public async static Task<(bool, OLRatingsData?)> TryGetRatingsAsync(string id)
        {
            try { return (true, await GetRatingsAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<OLRatingsData?> GetRatingsAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLRatingsData>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Books_Works, id, "ratings"),
                "summary"
            );
        }

        public async static Task<(bool, OLBookshelvesData?)> TryGetBookshelvesAsync(string id)
        {
            try { return (true, await GetBookshelvesAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<OLBookshelvesData?> GetBookshelvesAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLBookshelvesData>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Books_Works, id, "bookshelves"),
                "counts"
            );
        }

        public async static Task<(bool, OLEditionData[]?)> TryGetEditionsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetEditionsAsync(id, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData[]?> GetEditionsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData[]>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Works,
                    id,
                    "editions",
                    parameters
                ),
                "entries"
            );
        }

        public async static Task<(bool, int?)> TryGetEditionsCountAsync(string id)
        {
            try { return (true, await GetEditionsCountAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetEditionsCountAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Works,
                    id,
                    "editions",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
            );
        }

        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Works,
                    id,
                    "lists",
                    parameters
                ),
                "entries"
            );
        }

        public async static Task<(bool, int?)> TryGetListsCountAsync(string id)
        {
            try { return (true, await GetListsCountAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetListsCountAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Works,
                    id,
                    "lists",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
            );
        }
    }

    public static class OLEditionLoader
    {
        public async static Task<(bool, OLEditionData?)> TryGetDataAsync(string id, EditionIdType? editionIdType = null)
        {
            try { return (true, await GetDataAsync(id, editionIdType)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData?> GetDataAsync(string id, EditionIdType? editionIdType = null)
        {
            if (editionIdType == null) editionIdType = InferEditionIdType(id);
            switch (editionIdType)
            {
                case EditionIdType.OLID: return await GetDataByOLIDAsync(id);
                case EditionIdType.ISBN: return await GetDataByISBNAsync(id);
                case EditionIdType.LCCN: return await GetDataByBibkeyAsync("LCCN:" + GetPureID(id));
                case EditionIdType.OCLC: return await GetDataByBibkeyAsync("OCLC:" + GetPureID(id));
                default: return await GetDataByBibkeyAsync(id);
            }
        }

        public async static Task<(bool, OLEditionData?)> TryGetDataByOLIDAsync(string id)
        {
            try { return (true, await GetDataByOLIDAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData?> GetDataByOLIDAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Editions,
                    id
                )
            );
        }

        public async static Task<(bool, OLEditionData?)> TryGetDataByISBNAsync(string isbn)
        {
            try { return (true, await GetDataByISBNAsync(isbn)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData?> GetDataByISBNAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_ISBN,
                    id
                )
            );
        }

        public async static Task<(bool, OLEditionData?)> TryGetDataByBibkeyAsync(string id, EditionIdType? editionIdType = null)
        {
            try { return (true, await GetDataByBibkeyAsync(id, editionIdType)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData?> GetDataByBibkeyAsync(string id, EditionIdType? editionIdType = null)
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
                OpenLibraryUtility.BuildBooksURL
                (
                    new KeyValuePair<string, string>("bibkeys", id),
                    new KeyValuePair<string, string>("jscmd", "data"),
                    new KeyValuePair<string, string>("format", "json")
                ),
                id
            );
        }

        public async static Task<(bool, OLBookViewAPI?)> TryGetViewAPIAsync(string id, EditionIdType? editionIdType = null)
        {
            try { return (true, await GetViewAPIAsync(id, editionIdType)); }
            catch { return (false, null); }
        }
        public async static Task<OLBookViewAPI?> GetViewAPIAsync(string id, EditionIdType? editionIdType = null)
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
                OpenLibraryUtility.BuildBooksURL
                (
                    new KeyValuePair<string, string>("bibkeys", id),
                    new KeyValuePair<string, string>("jscmd", "viewapi"),
                    new KeyValuePair<string, string>("format", "json")
                ),
                id
            );
        }

        // TODO Maybe; add support for getting lists by isbn/bibkey
        // But: does not seem like that is possible in the OL API
        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Editions,
                    id,
                    "lists",
                    parameters
                ),
                "entries"
            );
        }

        public async static Task<(bool, int?)> TryGetListsCountAsync(string id)
        {
            try { return (true, await GetListsCountAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetListsCountAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Books_Editions,
                    id,
                    "lists",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
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

    public static class OLAuthorLoader
    {
        public async static Task<(bool, OLAuthorData?)> TryGetDataAsync(string id)
        {
            try { return (true, await GetDataAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<OLAuthorData?> GetDataAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLAuthorData>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Authors, id)
            );
        }

        public async static Task<(bool, OLWorkData[]?)> TryGetWorksAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetWorksAsync(id, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLWorkData[]?> GetWorksAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLWorkData[]>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Authors,
                    id,
                    "works",
                    parameters
                ),
                "entries"
            );
        }

        public async static Task<(bool, int?)> TryGetWorksCountAsync(string id)
        {
            try { return (true, await GetWorksCountAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetWorksCountAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Authors,
                    id,
                    "works",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
            );
        }

        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Authors,
                    id,
                    "lists",
                    parameters
                ),
                "entries"
            );
        }

        public async static Task<(bool, int?)> TryGetListsCountAsync(string id)
        {
            try { return (true, await GetListsCountAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetListsCountAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Authors,
                    id,
                    "lists",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
            );
        }
    }

    public static class OLSubjectLoader
    {
        public async static Task<(bool, OLSubjectData?)> TryGetDataAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetDataAsync(id, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLSubjectData?> GetDataAsync(string subject, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLSubjectData>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Subjects, subject, "", parameters)
            );
        }

        public async static Task<(bool, OLListData[]?)> TryGetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListsAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetListsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Subjects,
                    id,
                    "lists",
                    parameters
                ),
                "entries"
            );
        }

        public async static Task<(bool, int?)> TryGetListsCountAsync(string id)
        {
            try { return (true, await GetListsCountAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<int> GetListsCountAsync(string id)
        {
            return await OpenLibraryUtility.LoadAsync<int>
            (
                OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Subjects,
                    id,
                    "lists",
                    new KeyValuePair<string, string>("limit", "0")
                ),
                "size"
            );
        }
    }

    public static class OLImageLoader
    {
        public async static Task<(bool, byte[]?)> TryGetCoverAsync(string id)
        {
            try { return (true, await GetCoverAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<byte[]> GetCoverAsync(string id)
        {
            return Encoding.ASCII.GetBytes
            (
                await OpenLibraryUtility.RequestAsync(OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.Covers,
                    id,
                    parameters: new KeyValuePair<string, string>("default", "false")
                ))
            );
        }

        public async static Task<(bool, byte[]?)> TryGetCoverAsync(string idType, string id, string size) => await TryGetCoverAsync(idType + "/" + id + "-" + size);
        public async static Task<byte[]> GetCoverAsync(string idType, string id, string size) => await GetCoverAsync(idType + "/" + id + "-" + size);

        public async static Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(string id)
        {
            try { return (true, await GetAuthorPhotoAsync(id)); }
            catch { return (false, null); }
        }
        public async static Task<byte[]> GetAuthorPhotoAsync(string id)
        {
            return Encoding.ASCII.GetBytes
            (
                await OpenLibraryUtility.RequestAsync(OpenLibraryUtility.BuildURL
                (
                    OLRequestAPI.AuthorPhotos,
                    id,
                    parameters: new KeyValuePair<string, string>("default", "false")
                ))
            );
        }

        public async static Task<(bool, byte[]?)> TryGetAuthorPhotoAsync(string idType, string id, string size) => await TryGetAuthorPhotoAsync(idType + "/" + id + "-" + size);
        public async static Task<byte[]> GetAuthorPhotoAsync(string idType, string id, string size) => await GetAuthorPhotoAsync(idType + "/" + id + "-" + size);
    }

    public static class OLSearchLoader
    {
        public async static Task<(bool, OLWorkData[]?)> TryGetSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetSearchResultsAsync(query, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLWorkData[]?> GetSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
        {
            parameters = parameters.Append(new KeyValuePair<string, string>("q", query)).ToArray();
            return await OpenLibraryUtility.LoadAsync<OLWorkData[]>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Search, parameters: parameters),
                "docs"
            );
        }

        public async static Task<(bool, OLAuthorData[]?)> TryGetAuthorSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetAuthorSearchResultsAsync(query, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLAuthorData[]?> GetAuthorSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
        {
            parameters = parameters.Append(new KeyValuePair<string, string>("q", query)).ToArray();
            return await OpenLibraryUtility.LoadAsync<OLAuthorData[]>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Search, path: "authors", parameters: parameters),
                "docs"
            );
        }

        public async static Task<(bool, OLSubjectData[]?)> TryGetSubjectSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetSubjectSearchResultsAsync(query, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLSubjectData[]?> GetSubjectSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
        {
            parameters = parameters.Append(new KeyValuePair<string, string>("q", query)).ToArray();
            return await OpenLibraryUtility.LoadAsync<OLSubjectData[]>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Search, path: "subjects", parameters: parameters),
                "docs"
            );
        }

        public async static Task<(bool, OLListData[]?)> TryGetListSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListSearchResultsAsync(query, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetListSearchResultsAsync(string query, params KeyValuePair<string, string>[] parameters)
        {
            parameters = parameters.Append(new KeyValuePair<string, string>("q", query)).ToArray();
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildURL(OLRequestAPI.Search, path: "lists", parameters: parameters),
                "docs"
            );
        }
    }

    public static class OLListLoader
    {
        public async static Task<OLListData[]?> GetUserListsAsync(string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildListsURL(username), "entries"
            );
        }

        public async static Task<OLListData?> GetList(string username, string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData>
            (
                OpenLibraryUtility.BuildListsURL(username, id)
            );
        }

        public async static Task<OLEditionData[]?> GetListEditions(string username, string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData[]>
            (
                OpenLibraryUtility.BuildListsURL(username, id, "editions", parameters), "entries"
            );
        }

        /*
         * TODO
         * Response format seems inconsistent with other similar requests
         * 
        public async static Task<OLSubjectData[]?> GetListSubjects(string username, string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLSubjectData[]>
            (
                OpenLibraryUtility.BuildListsURL(username, id, "subjects", parameters), "entries"
            );
        }
        */
    }

    public static class OLRecentChangesLoader
    {
        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesURL(kind, parameters));
        }

        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesURL(year, kind, parameters));
        }

        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesURL(year, month, kind, parameters));
        }

        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesURL(year, month, day, kind, parameters));
        }
    }
}
