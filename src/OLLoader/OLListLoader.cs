using OpenLibraryNET.Utility;
using OpenLibraryNET.Data;

namespace OpenLibraryNET.Loader
{
    public class OLListLoader
    {
        internal OLListLoader(HttpClient client) => _client = client;

        HttpClient _client;

        public async Task<(bool, OLListData[]?)> TryGetUsersListsAsync(string username)
            => await TryGetUsersListAsync(_client, username);
        public async Task<OLListData[]?> GetUserListsAsync(string username)
            => await GetUserListsAsync(_client, username);

        public async Task<(bool, OLListData?)> TryGetListAsync(string username, string id)
            => await TryGetListAsync(_client, username, id);
        public async Task<OLListData?> GetListAsync(string username, string id)
            => await GetListAsync(_client, username, id);

        public async Task<(bool, OLEditionData[]?)> TryGetListEditionsAsync(string username, string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetListEditionsAsync(_client, username, id, parameters);
        public async Task<OLEditionData[]?> GetListEditionsAsync(string username, string id, params KeyValuePair<string, string>[] parameters)
            => await GetListEditionsAsync(_client, username, id, parameters);


        public async static Task<(bool, OLListData[]?)> TryGetUsersListAsync(HttpClient client, string username)
        {
            try { return (true, await GetUserListsAsync(client, username)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData[]?> GetUserListsAsync(HttpClient client, string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                OpenLibraryUtility.BuildListsURL(username),
                "entries",
                client: client
            );
        }

        public async static Task<(bool, OLListData?)> TryGetListAsync(HttpClient client, string username, string id)
        {
            try { return (true, await GetListAsync(client, username, id)); }
            catch { return (false, null); }
        }
        public async static Task<OLListData?> GetListAsync(HttpClient client, string username, string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData>
            (
                OpenLibraryUtility.BuildListsURL(username, id),
                client: client
            );
        }

        public async static Task<(bool, OLEditionData[]?)> TryGetListEditionsAsync(HttpClient client, string username, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListEditionsAsync(client, username, id, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLEditionData[]?> GetListEditionsAsync(HttpClient client, string username, string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData[]>
            (
                OpenLibraryUtility.BuildListsURL(username, id, "editions", parameters),
                "entries",
                client: client
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

}
