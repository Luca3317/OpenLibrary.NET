using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using OpenLibraryNET.Loader;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET
{
    /// <summary>
    /// The main interface to OpenLibrary.<br/>
    /// Instantiates HttpClient internally, as OpenLibraryClient uses cookies.<br/>
    /// To re-use the backing HttpClient, re-use the OpenLibrary instance.
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
        public string? Username => _username;
        /// <summary>
        /// The e-mail of the currently logged in account. Null if not logged in.
        /// </summary>
        public string? Email => _email;

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
        /// Create a new instance of the OpenLibraryClient.<br/>
        /// Instantiates HttpClient internally, as OpenLibraryClient uses cookies.<br/>
        /// To re-use the backing HttpClient, re-use the OpenLibrary instance.
        /// </summary>
        public OpenLibraryClient()
        {
            _httpHandler = new HttpClientHandler() { AllowAutoRedirect = true, UseCookies = true };
            _httpClient = new HttpClient(_httpHandler);

            _work = new OLWorkLoader(_httpClient);
            _author = new OLAuthorLoader(_httpClient);
            _edition = new OLEditionLoader(_httpClient);
            _image = new OLImageLoader(_httpClient);
            _list = new OLListLoader(_httpClient);
            _search = new OLSearchLoader(_httpClient);
            _subject = new OLSubjectLoader(_httpClient);
            _recentChanges = new OLRecentChangesLoader(_httpClient);
            _partner = new OLPartnerLoader(_httpClient);
        }

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

            // Check that there is now a session cookie present
            try
            {
                Cookie sessionCookie = _httpHandler.CookieContainer.GetCookies(OpenLibraryUtility.BaseUri).Single(cookie => cookie.Name == "session");
                _loggedIn = true;
                _email = email;
                _username = ExtractUsernameFromSessionCookie(sessionCookie);
            }
            catch { throw new System.Net.Http.HttpRequestException("Failed to authenticate; did you correctly input your email and password?"); }
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

            _loggedIn = false;
            _email = null;
            _username = null;
        }

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

        /// <summary>
        /// Attempt to create a new list.
        /// </summary>
        /// <param name="listName">Name of the new list</param>
        /// <param name="listDescription">Description of the new list</param>
        /// <param name="listSeeds">Seeds to include in the new list</param>
        /// <param name="listTags">Tags for the new list</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<bool> TryCreateListAsync(string listName, string listDescription, IEnumerable<string>? listSeeds = null, IEnumerable<string>? listTags = null)
        {
            try { await CreateListAsync(listName, listDescription, listSeeds, listTags); return true; }
            catch { return false; }
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
        public async Task CreateListAsync(string listName, string listDescription = "", IEnumerable<string>? listSeeds = null, IEnumerable<string>? listTags = null)
        {
            if (!_loggedIn) throw new System.InvalidOperationException("Creating a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri uri = new Uri("https://openlibrary.org/people/" + _username!.ToLower() + "/lists.json");
            var response = await _httpClient.PostAsJsonAsync(uri, new
            {
                name = listName,
                description = listDescription,
                tags = listTags ?? new List<string>(),
                seeds = listSeeds ?? new List<string>()
            });
            response.EnsureSuccessStatusCode();
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
            if (!_loggedIn) throw new System.InvalidOperationException("Deleting a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri uri = new Uri("https://openlibrary.org/people/" + _username!.ToLower() + "/lists/" + listID + "/delete.json");
            var response = await _httpClient.PostAsync(uri, null);
            response.EnsureSuccessStatusCode();
        }

        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;

        private bool _loggedIn = false;
        private string? _username = null;
        private string? _email = null;

        private readonly OLWorkLoader _work;
        private readonly OLAuthorLoader _author;
        private readonly OLEditionLoader _edition;
        private readonly OLImageLoader _image;
        private readonly OLListLoader _list;
        private readonly OLSearchLoader _search;
        private readonly OLSubjectLoader _subject;
        private readonly OLRecentChangesLoader _recentChanges;
        private readonly OLPartnerLoader _partner;

        private static string ExtractUsernameFromSessionCookie(Cookie sessionCookie)
            => Regex.Match(sessionCookie.Value, "(?<=/people/).*?(?=%)").Value;
    }
}