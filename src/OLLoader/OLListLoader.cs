using OpenLibraryNET.Utility;
using OpenLibraryNET.Data;
using OpenLibraryNET.OLData;
using Newtonsoft.Json;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's Lists API.
    /// </summary>
    public class OLListLoader
    {
        internal OLListLoader(OpenLibraryClient client) => _client = client;

        private readonly OpenLibraryClient _client;

        /// <summary>
        /// Attempt to get data about all of a user's lists.
        /// </summary>
        /// <param name="username">The user to get the lists of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLListData[]?)> TryGetUserListsAsync(string? username = null)
        {
            try { return (true, await GetUserListsAsync(username)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about all of a user's lists.
        /// </summary>
        /// <param name="username">The user to get the lists of. null to use the username of the logged in account.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task<OLListData[]?> GetUserListsAsync(string? username = null)
        {
            if (username == null)
            {
                username = _client.Username;
                if (username == null)
                {
                    throw new System.InvalidOperationException("The OpenLibraryClient instance is not logged in; you must pass a valid username");
                }
            }

            return await GetUserListsAsync(_client.BackingClient, username.ToLower());
        }

        /// <summary>
        /// Attempt to get data about a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLListData?)> TryGetListAsync(string username, string id)
            => await TryGetListAsync(_client.BackingClient, username, id);
        /// <summary>
        /// Get data about a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLListData?> GetListAsync(string username, string id)
            => await GetListAsync(_client.BackingClient, username, id);

        /// <summary>
        /// Attempt to get data about a list of the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLListData?)> TryGetListAsync(string id)
        {
            try { return (true, await GetListAsync(id)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about a list of the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task<OLListData?> GetListAsync(string id)
        {
            string? username = _client.Username;
            if (username == null)
            {
                throw new System.InvalidOperationException("The OpenLibraryClient instance is not logged in; you must pass a valid username");
            }

            return await GetListAsync(_client.BackingClient, username.ToLower(), id);
        }

        /// <summary>
        /// Attempt to get editions contained in a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLEditionData[]?)> TryGetListEditionsAsync(string username, string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetListEditionsAsync(_client.BackingClient, username, id, parameters);
        /// <summary>
        /// Get editions contained in a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLEditionData[]?> GetListEditionsAsync(string username, string id, params KeyValuePair<string, string>[] parameters)
            => await GetListEditionsAsync(_client.BackingClient, username, id, parameters);

        /// <summary>
        /// Attempt to get editions contained in a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLEditionData[]?)> TryGetListEditionsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListEditionsAsync(id, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get editions contained in a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLEditionData[]?> GetListEditionsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            string? username = _client.Username;
            if (username == null)
            {
                throw new System.InvalidOperationException("The OpenLibraryClient instance is not logged in; you must pass a valid username");
            }

            return await GetListEditionsAsync(_client.BackingClient, username.ToLower(), id, parameters);
        }

        /// <summary>
        /// Attempt to get subjects contained in a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLSubjectData[]?)> TryGetListSubjectsAsync(string username, string id, params KeyValuePair<string, string>[] parameters)
            => await TryGetListSubjectsAsync(_client.BackingClient, username, id, parameters);
        /// <summary>
        /// Get subjects contained in a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLSubjectData[]?> GetListSubjectsAsync(string username, string id, params KeyValuePair<string, string>[] parameters)
            => await GetListSubjectsAsync(_client.BackingClient, username, id, parameters);

        /// <summary>
        /// Attempt to get subjects contained in a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLSubjectData[]?)> TryGetListSubjectsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListSubjectsAsync(id, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Attempt to get subjects contained in a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLSubjectData[]?> GetListSubjectsAsync(string id, params KeyValuePair<string, string>[] parameters)
        {
            string? username = _client.Username;
            if (username == null)
            {
                throw new System.InvalidOperationException("The OpenLibraryClient instance is not logged in; you must pass a valid username");
            }

            return await GetListSubjectsAsync(_client.BackingClient, username.ToLower(), id, parameters);
        }

        /// <summary>
        /// Attempt to get seeds of a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLSeedData[]?)> TryGetListSeedsAsync(string username, string id)
            => await TryGetListSeedsAsync(_client.BackingClient, username, id);
        /// <summary>
        /// Get seeds of a list.
        /// </summary>
        /// <param name="username">The user to who the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLSeedData[]?> GetListSeedsAsync(string username, string id)
            => await GetListSeedsAsync(_client.BackingClient, username, id);

        /// <summary>
        /// Attempt to get seeds of a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLSeedData[]?)> TryGetListSeedsAsync(string id)
        {
            try { return (true, await GetListSeedsAsync(id)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get seeds of a list belonging to the logged in account.
        /// </summary>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLSeedData[]?> GetListSeedsAsync(string id)
        {
            string? username = _client.Username;
            if (username == null)
            {
                throw new System.InvalidOperationException("The OpenLibraryClient instance is not logged in; you must pass a valid username");
            }

            return await GetListSeedsAsync(_client.BackingClient, username.ToLower(), id);
        }



        /// <summary>
        /// Attempt to get data about all of an user's lists.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the lists of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLListData[]?)> TryGetUserListsAsync(HttpClient client, string username)
        {
            try { return (true, await GetUserListsAsync(client, username)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about all of an user's lists.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the lists of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLListData[]?> GetUserListsAsync(HttpClient client, string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData[]>
            (
                client,
                OpenLibraryUtility.BuildListsUri(username),
                "entries"
            );
        }

        /// <summary>
        /// Attempts to get data about a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLListData?)> TryGetListAsync(HttpClient client, string username, string id)
        {
            try { return (true, await GetListAsync(client, username, id)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get data about a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLListData?> GetListAsync(HttpClient client, string username, string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLListData>
            (
                client,
                OpenLibraryUtility.BuildListsUri(username, id)
            );
        }

        /// <summary>
        /// Attempts to get editions contained in a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLEditionData[]?)> TryGetListEditionsAsync(HttpClient client, string username, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListEditionsAsync(client, username, id, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get editions contained in a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLEditionData[]?> GetListEditionsAsync(HttpClient client, string username, string id, params KeyValuePair<string, string>[] parameters)
        {
            return await OpenLibraryUtility.LoadAsync<OLEditionData[]>
            (
                client,
                OpenLibraryUtility.BuildListsUri(username, id, "editions", parameters),
                "entries"
            );
        }

        /// <summary>
        /// Attempt to get subjects contained in a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<(bool, OLSubjectData[]?)> TryGetListSubjectsAsync(HttpClient client, string username, string id, params KeyValuePair<string, string>[] parameters)
        {
            try { return (true, await GetListSubjectsAsync(client, username, id, parameters)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get subjects contained in a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs.</param>
        /// <param name="id">The ID of the list.</param>
        /// <param name="parameters">The query parameters of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLSubjectData[]?> GetListSubjectsAsync(HttpClient client, string username, string id, params KeyValuePair<string, string>[] parameters)
        {
            var listSubject = await OpenLibraryUtility.LoadAsync<ListSubject>
            (
                client,
                OpenLibraryUtility.BuildListsUri(username, id, "subjects", parameters)
            );

            if (listSubject == null) return null;

            List<OLSubjectData> subjects = new List<OLSubjectData>();

            subjects.AddRange(listSubject.Subjects.Select(s => new OLSubjectData() { Name = s.Name, SubjectType = "subject", WorkCount = s.Count }));
            subjects.AddRange(listSubject.Places.Select(s => new OLSubjectData() { Name = s.Name, SubjectType = "place", WorkCount = s.Count }));
            subjects.AddRange(listSubject.Times.Select(s => new OLSubjectData() { Name = s.Name, SubjectType = "time", WorkCount = s.Count }));
            subjects.AddRange(listSubject.People.Select(s => new OLSubjectData() { Name = s.Name, SubjectType = "people", WorkCount = s.Count }));

            return subjects.ToArray();
        }

        /// <summary>
        /// Attempt to get seeds of a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<(bool, OLSeedData[]?)> TryGetListSeedsAsync(HttpClient client, string username, string id)
        {
            try { return (true, await GetListSeedsAsync(client, username, id)); }
            catch { return (false, null); }
        }
        /// <summary>
        /// Get seeds of a list.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to who the list belongs to.</param>
        /// <param name="id">The ID of the list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLSeedData[]?> GetListSeedsAsync(HttpClient client, string username, string id)
        {
            return await OpenLibraryUtility.LoadAsync<OLSeedData[]>
            (
                client,
                OpenLibraryUtility.BuildListsUri(username, id, "seeds"),
                "entries"
            );
        }

        private class ListSubject
        {
            [JsonProperty("subjects")]
            public ListSubjectEntry[] Subjects { get; init; } = Array.Empty<ListSubjectEntry>();
            [JsonProperty("places")]
            public ListSubjectEntry[] Places { get; init; } = Array.Empty<ListSubjectEntry>();
            [JsonProperty("people")]
            public ListSubjectEntry[] People { get; init; } = Array.Empty<ListSubjectEntry>();
            [JsonProperty("times")]
            public ListSubjectEntry[] Times { get; init; } = Array.Empty<ListSubjectEntry>();

            public class ListSubjectEntry
            {
                [JsonProperty("name")]
                public string Name { get; init; } = "";
                [JsonProperty("count")]
                public int Count { get; init; } = -1;
                [JsonProperty("url")]
                public string Url { get; init; } = "";
            }
        }
    }

}
