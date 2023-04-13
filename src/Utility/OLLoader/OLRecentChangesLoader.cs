using OpenLibraryNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibraryNET
{
    public class OLRecentChangesLoader
    {
        internal OLRecentChangesLoader(HttpClient client) => _client = client;

        HttpClient _client;

        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, kind, parameters);

        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, year, kind, parameters);

        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, year, month, kind, parameters);

        public async Task<OLRecentChangesData[]?> GetRecentChangesAsync(string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
            => await GetRecentChangesAsync(_client, year, month, day, kind, parameters);

        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesURL(kind, parameters), client: client);
        }

        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string year, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesURL(year, kind, parameters), client: client);
        }

        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string year, string month, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesURL(year, month, kind, parameters), client: client);
        }

        public async static Task<OLRecentChangesData[]?> GetRecentChangesAsync(HttpClient client, string year, string month, string day, string kind = "", params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLRecentChangesData[]>(OpenLibraryUtility.BuildRecentChangesURL(year, month, day, kind, parameters), client: client);
        }
    }

}
