using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using OpenLibraryNET.Loader;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET
{
    public class OpenLibraryClient
    {
        public bool LoggedIn => _httpHandler.CookieContainer.GetCookies(OpenLibraryUtility.BaseUri).SingleOrDefault(cookie => cookie.Name == "session") != null;
        public string? Username => _username;
        public string? Email => _email;

        public OLWorkLoader Work => _work;
        public OLAuthorLoader Author => _author;
        public OLEditionLoader Edition => _edition;
        public OLImageLoader Image => _image;
        public OLListLoader List => _list;
        public OLSearchLoader Search => _search;
        public OLSubjectLoader Subject => _subject;
        public OLRecentChangesLoader RecentChanges => _recentChanges;

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
        }

        public async Task<bool> TryLoginAsync(string email, string password)
        {
            try { await LoginAsync(email, password); return true; }
            catch { return false; }
        }
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
            catch { throw new System.Exception("Failed to authenticate; did you correctly input your email and password?"); }
        }

        public async Task<bool> TryLogoutAsync()
        {
            try { await LogoutAsync(); return true; }
            catch { return false; }
        }
        public async Task LogoutAsync()
        {
            if (!LoggedIn) return;

            Uri uri = new Uri(OpenLibraryUtility.BaseUri, "account/logout");
            var response = await _httpClient.PostAsync(uri, null);

            _loggedIn = false;
            _email = null;
            _username = null;
        }

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

        public async Task<OLEdition> GetEditionAsync(string id, EditionIdType? idType = null, ImageSize? coverSize = null)
            => await GetEditionAsync(id, idType, coverSize == null ? "" : coverSize.Value.GetString());
        public async Task<OLEdition> GetEditionAsync(string id, EditionIdType? idType = null, string? coverSize = null)
        {
            OLEdition edition = new OLEdition()
            {
                ID = id,
                Data = await _edition.GetDataAsync(id, idType),
            };

            if (!string.IsNullOrWhiteSpace(coverSize))
            {
                string type, coverId;
                if (idType == null)
                {
                    type = "ID";
                    coverId = edition.Data!.CoverKeys[0].ToString();
                }
                else
                {
                    type = idType.Value.GetString();
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

        public async Task<bool> TryCreateListAsync(string listName, string listDescription, IEnumerable<string>? listSeeds = null, IEnumerable<string>? listTags = null)
        {
            try { await CreateListAsync(listName, listDescription, listSeeds, listTags); return true; }
            catch { return false; }
        }
        public async Task CreateListAsync(string listName, string listDescription = "", IEnumerable<string>? listSeeds = null, IEnumerable<string>? listTags = null)
        {
            if (!_loggedIn) throw new System.InvalidOperationException();

            Uri uri = new Uri("https://openlibrary.org/people/" + _username!.ToLower() + "/lists.json");
            var response = await _httpClient.PostAsJsonAsync(uri, new
            {
                name = listName,
                description = listDescription,
                tags = listTags != null ? listTags : new List<string>(),
                seeds = listSeeds != null ? listSeeds : new List<string>()
            });
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> TryDeleteListAsync(string listID)
        {
            try { await DeleteListAsync(listID); return true; }
            catch { return false; };
        }
        public async Task DeleteListAsync(string listID)
        {
            Uri uri = new Uri("https://openlibrary.org/people/" + _username!.ToLower() + "/lists/" + listID + "/delete.json");
            var response = await _httpClient.PostAsync(uri, null);
            response.EnsureSuccessStatusCode();
        }

        private HttpClient _httpClient;
        private HttpClientHandler _httpHandler;

        private bool _loggedIn = false;
        private string? _username = null;
        private string? _email = null;

        private OLWorkLoader _work;
        private OLAuthorLoader _author;
        private OLEditionLoader _edition;
        private OLImageLoader _image;
        private OLListLoader _list;
        private OLSearchLoader _search;
        private OLSubjectLoader _subject;
        private OLRecentChangesLoader _recentChanges;

        private string ExtractUsernameFromSessionCookie(Cookie sessionCookie)
            => Regex.Match(sessionCookie.Value, "(?<=/people/).*?(?=%)").Value;
    }
}