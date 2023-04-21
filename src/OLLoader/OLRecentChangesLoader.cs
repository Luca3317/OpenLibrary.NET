using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    public class OLRecentChangesLoader
    {
        internal OLRecentChangesLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;

        public async Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(string kind = "", params KeyValuePair<string, string>[] parameters)
            => await TryGetRecentChangesAsync(_client, kind, parameters);
        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, kind, parameters);

        public async Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(string year, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await TryGetRecentChangesAsync(_client, year, kind, parameters);
        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, year, kind, parameters);

        public async Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await TryGetRecentChangesAsync(_client, year, month, kind, parameters);
        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, year, month, kind, parameters);

        public async Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await TryGetRecentChangesAsync(_client, year, month, day, kind, parameters);
        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, year, month, day, kind, parameters);

        public async static Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(HttpClient client, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetRecentChangesAsync(client, kind, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesUri(kind, parameters), client: client);
        }

        public async static Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(HttpClient client, string year, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetRecentChangesAsync(client, year, kind, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string year, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesUri(year, kind, parameters), client: client);
        }

        public async static Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(HttpClient client, string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetRecentChangesAsync(client, year, month, kind, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesUri(year, month, kind, parameters), client: client);
        }

        public async static Task<(bool, OLRecentChangesData[]?)> TryGetRecentChangesAsync(HttpClient client, string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetRecentChangesAsync(client, year, month, day, kind, parameters)); }
            catch { return (false, null); }
        }
        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesUri(year, month, day, kind, parameters), client: client);
        }
    }

}
