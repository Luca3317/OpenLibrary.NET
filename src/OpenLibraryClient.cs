using System.Formats.Asn1;
using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenLibraryNET.Data;
using OpenLibraryNET.Loader;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET
{
    /// <summary>
    /// The main interface to OpenLibrary.<br/>
    /// Instantiates HttpClient internally, as OpenLibraryClient uses cookies.<br/>
    /// To reuse the backing HttpClient, reuse the OpenLibrary instance.
    /// </summary>
    public class OpenLibraryClient
    {
        /// <summary>
        /// Does this instance hold a session token?
        /// </summary>
        public bool LoggedIn => _httpHandler.CookieContainer.GetCookies(OpenLibraryUtility.BaseUri).SingleOrDefault(cookie => cookie.Name == "session") != null;
        /// <summary>
        /// The username of the currently logged in account. Null if not logged in.
        /// </summary>
        public string? Username => ExtractUsernameFromSessionCookie(_httpHandler.CookieContainer.GetCookies(OpenLibraryUtility.BaseUri).Single(cookie => cookie.Name == "session"));

        /// <summary>
        /// Interface to the Works API
        /// </summary>
        public OLWorkLoader Work => _work;
        /// <summary>
        /// Interface to the Authors API
        /// </summary>
        public OLAuthorLoader Author => _author;
        /// <summary>
        /// Interface to the Edition/ISBN/Works API
        /// </summary>
        public OLEditionLoader Edition => _edition;
        /// <summary>
        /// Interface to the Covers/AuthorPhotos API
        /// </summary>
        public OLImageLoader Image => _image;
        /// <summary>
        /// Interface to the Lists API
        /// </summary>
        public OLListLoader List => _list;
        /// <summary>
        /// Interface to the Search API
        /// </summary>
        public OLSearchLoader Search => _search;
        /// <summary>
        /// Interface to the Subjects API
        /// </summary>
        public OLSubjectLoader Subject => _subject;
        /// <summary>
        /// Interface to the RecentChanges API
        /// </summary>
        public OLRecentChangesLoader RecentChanges => _recentChanges;
        /// <summary>
        /// Interface to the Partner API
        /// </summary>
        public OLPartnerLoader Partner => _partner;
        /// <summary>
        /// Interface to the MyBooks API
        /// </summary>
        public OLMyBooksLoader MyBooks => _myBooks;

        /// <summary>
        /// <see cref="HttpClient"/> instance created by this OpenLibraryClient instance.
        /// </summary>
        public HttpClient BackingClient => _httpClient;

        /// <summary>
        /// Create a new instance of the OpenLibraryClient.<br/>
        /// Instantiates HttpClient internally, as OpenLibraryClient uses cookies.<br/>
        /// To reuse the backing HttpClient, reuse the OpenLibrary instance.
        /// </summary>
        public OpenLibraryClient()
        {
            _httpHandler = new HttpClientHandler() { AllowAutoRedirect = true, UseCookies = true };
            _httpClient = new HttpClient(_httpHandler);

            _work = new OLWorkLoader(_httpClient);
            _author = new OLAuthorLoader(_httpClient);
            _edition = new OLEditionLoader(_httpClient);
            _image = new OLImageLoader(_httpClient);
            _list = new OLListLoader(this);
            _search = new OLSearchLoader(_httpClient);
            _subject = new OLSubjectLoader(_httpClient);
            _recentChanges = new OLRecentChangesLoader(_httpClient);
            _partner = new OLPartnerLoader(_httpClient);
            _myBooks = new OLMyBooksLoader(this);
        }

        #region LogIn/LogOut
        /// <summary>
        /// Attempt to login to an OpenLibrary account.
        /// </summary>
        /// <param name="email">The email of the account to login with</param>
        /// <param name="password">The password of the account to login with</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryLoginAsync(string email, string password)
        {
            try { await LoginAsync(email, password); return true; }
            catch { return false; }
        }
        /// <summary>
        /// Login to an OpenLibrary account.
        /// </summary>
        /// <param name="email">The email of the account to login with</param>
        /// <param name="password">The password of the account to login with</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task LoginAsync(string email, string password)
        {
            await LogoutAsync();

            var loginData = new KeyValuePair<string, string>[]
            {
                new ("username", email),
                new ("password", password)
            };

            Uri uri = new Uri(OpenLibraryUtility.BaseUri, "account/login");
            FormUrlEncodedContent content = new FormUrlEncodedContent(loginData);

            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();

            // Check that there is now a session cookie present
            if (!LoggedIn)
            {
                throw new System.Net.Http.HttpRequestException("Failed to authenticate; did you correctly input your email and password?");
            }
        }

        /// <summary>
        /// Attempt to logout of the currently logged in OpenLibrary account.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryLogoutAsync()
        {
            try { await LogoutAsync(); return true; }
            catch { return false; }
        }
        /// <summary>
        /// Logout of the currently logged in OpenLibrary account.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task LogoutAsync()
        {
            if (!LoggedIn) return;

            Uri uri = new Uri(OpenLibraryUtility.BaseUri, "account/logout");
            var response = await _httpClient.PostAsync(uri, null);
            response.EnsureSuccessStatusCode();

            if (LoggedIn)
            {
                throw new System.Net.Http.HttpRequestException("Failed to log out of logged in account for unknown reasons");
            }
        }
        #endregion

        #region Get Requests
        /// <summary>
        /// Get a work with all its information.<br/>
        /// Makes multiple web requests (up to 4).
        /// </summary>
        /// <param name="id">The OLID of the work</param>
        /// <param name="editionsCount">The amount of editions to load</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task<OLWork> GetWorkAsync(string id, int editionsCount = 0)
        {
            return new OLWork()
            {
                ID = id,
                Data = await _work.GetDataAsync(id),
                Ratings = await _work.GetRatingsAsync(id),
                Bookshelves = await _work.GetBookshelvesAsync(id),
                Editions = editionsCount > 0 ?
                    await _work.GetEditionsAsync(id, new KeyValuePair<string, string>("limit", editionsCount.ToString())) : null
            };
        }


        /// <summary>
        /// Get an author with all their information.<br/>
        /// Makes multiple web requests (up to 2).
        /// </summary>
        /// <param name="id">The OLID of the author</param>
        /// <param name="worksCount">The amount of works to load</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task<OLAuthor> GetAuthorAsync(string id, int worksCount = 0)
        {
            return new OLAuthor()
            {
                ID = id,
                Data = await _author.GetDataAsync(id),
                Works = worksCount > 0 ?
                    await _author.GetWorksAsync(id, new KeyValuePair<string, string>("limit", worksCount.ToString())) : null
            };
        }

        /// <summary>
        /// Get an edition.
        /// </summary>
        /// <param name="id">The id of the edition. Should be in bibkey format</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task<OLEdition> GetEditionAsync(string id) => await GetEditionAsync(id, (string?)null, (string?)null);
        /// <summary>
        /// Get an edition and its cover.<br/>
        /// Makes multiple web requests (up to 2).
        /// </summary>
        /// <param name="id">The id of the edition</param>
        /// <param name="idType">The type of the id</param>
        /// <param name="coverSize">The size of the cover. Leave empty if you dont want to load the cover</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task<OLEdition> GetEditionAsync(string id, BookIdType? idType = null, ImageSize? coverSize = null)
            => await GetEditionAsync(id, idType == null ? "" : idType.Value.GetString(), coverSize == null ? "" : coverSize.Value.GetString());
        /// <summary>
        /// Get an edition and its cover.<br/>
        /// Makes multiple web requests (up to 2).
        /// </summary>
        /// <param name="id">The id of the edition</param>
        /// <param name="idType">The type of the id</param>
        /// <param name="coverSize">The size of the cover. Leave empty if you dont want to load the cover</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task<OLEdition> GetEditionAsync(string id, string? idType = null, string? coverSize = null)
        {
            string bibkey;
            if (idType != null) bibkey = OpenLibraryUtility.SetBibkeyPrefix(idType, id);
            else bibkey = id;

            OLEdition edition = new OLEdition()
            {
                ID = id,
                Data = await _edition.GetDataByBibkeyAsync(bibkey),
            };

            if (!string.IsNullOrWhiteSpace(coverSize))
            {
                string type, coverId;
                if (idType == null)
                {
                    type = "ID";
                    coverId = edition.Data!.CoverIDs[0].ToString();
                }
                else
                {
                    type = idType;
                    coverId = id;
                }

                switch (coverSize.ToUpper().Trim())
                {
                    case "S": return edition with { CoverS = await _image.GetCoverAsync(type, coverId, "S") };
                    case "M": return edition with { CoverM = await _image.GetCoverAsync(type, coverId, "M") };
                    case "L": return edition with { CoverL = await _image.GetCoverAsync(type, coverId, "L") };
                    default: throw new System.Exception();
                }
            }

            return edition;
        }
        #endregion

        #region Post Requests
        /// <summary>
        /// Attempt to create a new list.
        /// </summary>
        /// <param name="listName">Name of the new list</param>
        /// <param name="listDescription">Description of the new list</param>
        /// <param name="listSeeds">Seeds to include in the new list</param>
        /// <param name="listTags">Tags for the new list</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, string)> TryCreateListAsync(string listName, string listDescription, IEnumerable<string>? listSeeds = null, IEnumerable<string>? listTags = null)
        {
            try { return (true, await CreateListAsync(listName, listDescription, listSeeds, listTags)); }
            catch { return (false, ""); }
        }
        /// <summary>
        /// Create a new list.
        /// </summary>
        /// <param name="listName">Name of the new list</param>
        /// <param name="listDescription">Description of the new list</param>
        /// <param name="listSeeds">Seeds to include in the new list</param>
        /// <param name="listTags">Tags for the new list</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task<string> CreateListAsync(string listName, string listDescription = "", IEnumerable<string>? listSeeds = null, IEnumerable<string>? listTags = null)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Creating a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri uri = new Uri("https://openlibrary.org/people/" + username.ToLower() + "/lists.json");
            var response = await _httpClient.PostAsJsonAsync(uri, new
            {
                name = listName,
                description = listDescription,
                tags = listTags ?? new List<string>(),
                seeds = listSeeds ?? new List<string>()
            });
            response.EnsureSuccessStatusCode();

            JToken token = JToken.Parse(await response.Content.ReadAsStringAsync());
            return OpenLibraryUtility.ExtractIdFromKey(token["key"]!.ToObject<string>()!);
        }

        /// <summary>
        /// Attempt to delete a list.
        /// </summary>
        /// <param name="listID">The id of the list to be deleted.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryDeleteListAsync(string listID)
        {
            try { await DeleteListAsync(listID); return true; }
            catch { return false; };
        }
        /// <summary>
        /// Delete a list.
        /// </summary>
        /// <param name="listID">The ID of the list to be deleted.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task DeleteListAsync(string listID)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Deleting a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri uri = new Uri("https://openlibrary.org/people/" + username.ToLower() + "/lists/" + listID + "/delete.json");
            var response = await _httpClient.PostAsync(uri, null);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Attempt to add editions to a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="editionOlids">The OLIDs of the editions.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryAddEditionsToListAsync(string listOlid, params string[] editionOlids)
        {
            try { await AddEditionsToListAsync(listOlid, editionOlids); return true; }
            catch { return false; };
        }
        /// <summary>
        /// Add editions to a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="editionOlids">The OLIDs of the editions.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task AddEditionsToListAsync(string listOlid, params string[] editionOlids)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Adding editions to a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri posturi = new Uri(OpenLibraryUtility.BaseUri + $"people/{username.ToLower()}/lists/{listOlid}/seeds.json");

            var requestData = new
            {
                add = editionOlids.Select(olid => new { key = "/books/" + OpenLibraryUtility.ExtractIdFromKey(olid) })
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(requestData),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(posturi, jsonContent);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Attempt to remove editions from a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="editionOlids">The OLIDs of the editions.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryRemoveEditionsFromListAsync(string listOlid, params string[] editionOlids)
        {
            try { await RemoveEditionsFromListAsync(listOlid, editionOlids); return true; }
            catch { return false; };
        }
        /// <summary>
        /// Remove editions from a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="editionOlids">The OLIDs of the editions.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task RemoveEditionsFromListAsync(string listOlid, params string[] editionOlids)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Removing editions from a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri posturi = new Uri(OpenLibraryUtility.BaseUri + $"people/{username.ToLower()}/lists/{listOlid}/seeds.json");

            var requestData = new
            {
                remove = editionOlids.Select(olid => new { key = "/books/" + OpenLibraryUtility.ExtractIdFromKey(olid) })
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(requestData),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(posturi, jsonContent);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Attempt to add works to a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="workOlids">The OLIDs of the works.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryAddWorksToListAsync(string listOlid, params string[] workOlids)
        {
            try { await AddWorksToListAsync(listOlid, workOlids); return true; }
            catch { return false; };
        }
        /// <summary>
        /// Add works to a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="workOlids">The OLIDs of the works.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task AddWorksToListAsync(string listOlid, params string[] workOlids)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Adding works to a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri posturi = new Uri(OpenLibraryUtility.BaseUri + $"people/{username.ToLower()}/lists/{listOlid}/seeds.json");

            var requestData = new
            {
                add = workOlids.Select(olid => new { key = "/works/" + OpenLibraryUtility.ExtractIdFromKey(olid) })
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(requestData),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(posturi, jsonContent);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Attempt to remove works from a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="workOlids">The OLIDs of the works.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryRemoveWorksFromListAsync(string listOlid, params string[] workOlids)
        {
            try { await RemoveWorksFromListAsync(listOlid, workOlids); return true; }
            catch { return false; };
        }
        /// <summary>
        /// Remove works from a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="workOlids">The OLIDs of the works.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task RemoveWorksFromListAsync(string listOlid, params string[] workOlids)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Removing works from a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri posturi = new Uri(OpenLibraryUtility.BaseUri + $"people/{username.ToLower()}/lists/{listOlid}/seeds.json");

            var requestData = new
            {
                remove = workOlids.Select(olid => new { key = "/works/" + OpenLibraryUtility.ExtractIdFromKey(olid) })
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(requestData),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(posturi, jsonContent);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Attempt to add subjects to a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="subjects">The subjects.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryAddSubjectsToListAsync(string listOlid, params string[] subjects)
        {
            try { await AddSubjectsToListAsync(listOlid, subjects); return true; }
            catch { return false; };
        }
        /// <summary>
        /// Add subjects to a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="subjects">The subjects.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task AddSubjectsToListAsync(string listOlid, params string[] subjects)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Adding subjects to a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri posturi = new Uri(OpenLibraryUtility.BaseUri + $"people/{username.ToLower()}/lists/{listOlid}/seeds.json");

            var requestData = new
            {
                add = subjects.Select(olid => new { key = "/subjects/" + OpenLibraryUtility.ExtractIdFromKey(olid) })
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(requestData),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(posturi, jsonContent);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Attempt to remove subjects from a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="subjects">The subjects.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryRemoveSubjectsFromListAsync(string listOlid, params string[] subjects)
        {
            try { await RemoveSubjectsFromListAsync(listOlid, subjects); return true; }
            catch { return false; };
        }
        /// <summary>
        /// Remove subjects from a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="subjects">The subjects.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task RemoveSubjectsFromListAsync(string listOlid, params string[] subjects)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Removing subjects from a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri posturi = new Uri(OpenLibraryUtility.BaseUri + $"people/{username.ToLower()}/lists/{listOlid}/seeds.json");

            var requestData = new
            {
                remove = subjects.Select(olid => new { key = "/subjects/" + OpenLibraryUtility.ExtractIdFromKey(olid) })
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(requestData),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(posturi, jsonContent);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Add seeds to a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="seeds">The seeds. Must be in key format, e.g. "/works/OL123W", "/subjects/place:los_angeles"</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task<bool> TryAddSeedsToListAsync(string listOlid, params string[] seeds)
        {
            try { await AddSeedsToListAsync(listOlid, seeds); return true; }
            catch { return false; };
        }
        /// <summary>
        /// Attempt to add seeds to a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="seeds">The seeds. Must be in key format, e.g. "/works/OL123W", "/subjects/place:los_angeles"</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task AddSeedsToListAsync(string listOlid, params string[] seeds)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Adding seeds to a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri posturi = new Uri(OpenLibraryUtility.BaseUri + $"people/{username.ToLower()}/lists/{listOlid}/seeds.json");

            var requestData = new
            {
                add = seeds.Select(seed => new { key = seed })
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(requestData),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(posturi, jsonContent);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Attempt to remove seeds from a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="seeds">The seeds. Must be in key format, e.g. "/works/OL123W", "/subjects/place:los_angeles"</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryRemoveSeedsFromListAsync(string listOlid, params string[] seeds)
        {
            try { await RemoveSeedsFromListAsync(listOlid, seeds); return true; }
            catch { return false; };
        }
        /// <summary>
        /// Remove seeds from a list.
        /// </summary>
        /// <param name="listOlid">The OLID of the list.</param>
        /// <param name="seeds">The seeds. Must be in key format, e.g. "/works/OL123W", "/subjects/place:los_angeles"</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        public async Task RemoveSeedsFromListAsync(string listOlid, params string[] seeds)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Removing seeds from a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri posturi = new Uri(OpenLibraryUtility.BaseUri + $"people/{username.ToLower()}/lists/{listOlid}/seeds.json");

            var requestData = new
            {
                remove = seeds.Select(seed => new { key = seed })
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(requestData),
                System.Text.Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(posturi, jsonContent);
            response.EnsureSuccessStatusCode();
        }
        #endregion

        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;

        private readonly OLWorkLoader _work;
        private readonly OLAuthorLoader _author;
        private readonly OLEditionLoader _edition;
        private readonly OLImageLoader _image;
        private readonly OLListLoader _list;
        private readonly OLSearchLoader _search;
        private readonly OLSubjectLoader _subject;
        private readonly OLRecentChangesLoader _recentChanges;
        private readonly OLPartnerLoader _partner;
        private readonly OLMyBooksLoader _myBooks;

        private static string ExtractUsernameFromSessionCookie(Cookie sessionCookie)
            => Regex.Match(sessionCookie.Value, "(?<=/people/).*?(?=%)").Value;
    }
}