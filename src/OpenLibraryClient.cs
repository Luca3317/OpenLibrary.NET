using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using OpenLibraryNET.Loader;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET
{
    public class OpenLibraryClient
    {
        public bool LoggedIn => _httpHandler.CookieContainer.GetCookies(new Uri(OpenLibraryUtility.BaseURL)).SingleOrDefault(cookie => cookie.Name == "session") != null;
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
            _httpHandler = new HttpClientHandler();
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

        public async Task<bool> TryLogin(string email, string password)
        {
            try { await Login(email, password); return true; }
            catch { return false; }
        }
        public async Task Login(string email, string password)
        {
            // Clear cookies TODO: maybe only remove session cookie
            _httpHandler.CookieContainer = new CookieContainer();

            var loginData = new KeyValuePair<string, string>[]
            {
                new ("username", email),
                new ("password", password)
            };

            Uri uri = new Uri("https://openlibrary.org/account/login");
            FormUrlEncodedContent content = new FormUrlEncodedContent(loginData);

            var response = await _httpClient.PostAsync(uri, content);

            // Check that there is now a session cookie present
            try
            {
                Cookie sessionCookie = _httpHandler.CookieContainer.GetCookies(new Uri(OpenLibraryUtility.BaseURL)).Single(cookie => cookie.Name == "session");
                _loggedIn = true;
                _email = email;
                _password = password;
                _username = ExtractUsernameFromSessionCookie(sessionCookie);
            }
            catch { throw new System.Exception("Failed to authenticate; did you correctly input your email and password?"); }
        }

        public async Task<bool> TryCreateListAsync(string listName, string listDescription, IEnumerable<string>? listSeeds = null, IEnumerable<string>? listTags = null)
        {
            try { await CreateListAsync(listName, listDescription, listSeeds, listTags);  return true; }
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
        private string? _password = null;
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