using System.Text;

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
    }

    public static class OLEditionLoader
    {
        public async static Task<(bool, OLEditionData?)> TryGetDatabyOLIDAsync(string id)
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
}
