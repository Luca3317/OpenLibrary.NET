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
    public class OpenLibraryClient : IOpenLibraryClient
    {
        ///<inheritdoc/>
        public bool LoggedIn => _httpHandler.CookieContainer.GetCookies(OpenLibraryUtility.BaseUri).SingleOrDefault(cookie => cookie.Name == "session") != null;
        ///<inheritdoc/>
        public string? Username => ExtractUsernameFromSessionCookie(_httpHandler.CookieContainer.GetCookies(OpenLibraryUtility.BaseUri).Single(cookie => cookie.Name == "session"));

        ///<inheritdoc/>
        public IOLWorkLoader Work => _work;
        ///<inheritdoc/>
        public IOLAuthorLoader Author => _author;
        ///<inheritdoc/>
        public IOLEditionLoader Edition => _edition;
        ///<inheritdoc/>
        public IOLImageLoader Image => _image;
        ///<inheritdoc/>
        public IOLListLoader List => _list;
        ///<inheritdoc/>
        public IOLSearchLoader Search => _search;
        ///<inheritdoc/>
        public IOLSubjectLoader Subject => _subject;
        ///<inheritdoc/>
        public IOLRecentChangesLoader RecentChanges => _recentChanges;
        ///<inheritdoc/>
        public IOLPartnerLoader Partner => _partner;
        ///<inheritdoc/>
        public IOLMyBooksLoader MyBooks => _myBooks;

        ///<inheritdoc/>
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
        ///<inheritdoc/>
        public async Task<bool> TryLoginAsync(string email, string password)
        {
            try { await LoginAsync(email, password); return true; }
            catch { return false; }
        }
        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<bool> TryLogoutAsync()
        {
            try { await LogoutAsync(); return true; }
            catch { return false; }
        }
        ///<inheritdoc/>
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
        ///<inheritdoc/>
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


        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<OLEdition> GetEditionAsync(string id) => await GetEditionAsync(id, (string?)null, (string?)null);
        ///<inheritdoc/>
        public async Task<OLEdition> GetEditionAsync(string id, BookIdType? idType = null, ImageSize? coverSize = null)
            => await GetEditionAsync(id, idType == null ? "" : idType.Value.GetString(), coverSize == null ? "" : coverSize.Value.GetString());
        ///<inheritdoc/>
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
        ///<inheritdoc/>
        public async Task<(bool, string)> TryCreateListAsync(string listName, string listDescription, IEnumerable<string>? listSeeds = null, IEnumerable<string>? listTags = null)
        {
            try { return (true, await CreateListAsync(listName, listDescription, listSeeds, listTags)); }
            catch { return (false, ""); }
        }
        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<bool> TryDeleteListAsync(string listID)
        {
            try { await DeleteListAsync(listID); return true; }
            catch { return false; };
        }
        ///<inheritdoc/>
        public async Task DeleteListAsync(string listID)
        {
            string? username = Username;
            if (username == null) throw new System.InvalidOperationException("Deleting a list requires the OpenLibraryClient instance to be logged in to an OpenLibrary account");

            Uri uri = new Uri("https://openlibrary.org/people/" + username.ToLower() + "/lists/" + listID + "/delete.json");
            var response = await _httpClient.PostAsync(uri, null);
            response.EnsureSuccessStatusCode();
        }

        ///<inheritdoc/>
        public async Task<bool> TryAddEditionsToListAsync(string listOlid, params string[] editionOlids)
        {
            try { await AddEditionsToListAsync(listOlid, editionOlids); return true; }
            catch { return false; };
        }
        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<bool> TryRemoveEditionsFromListAsync(string listOlid, params string[] editionOlids)
        {
            try { await RemoveEditionsFromListAsync(listOlid, editionOlids); return true; }
            catch { return false; };
        }
        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<bool> TryAddWorksToListAsync(string listOlid, params string[] workOlids)
        {
            try { await AddWorksToListAsync(listOlid, workOlids); return true; }
            catch { return false; };
        }
        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<bool> TryRemoveWorksFromListAsync(string listOlid, params string[] workOlids)
        {
            try { await RemoveWorksFromListAsync(listOlid, workOlids); return true; }
            catch { return false; };
        }
        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<bool> TryAddSubjectsToListAsync(string listOlid, params string[] subjects)
        {
            try { await AddSubjectsToListAsync(listOlid, subjects); return true; }
            catch { return false; };
        }
        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<bool> TryRemoveSubjectsFromListAsync(string listOlid, params string[] subjects)
        {
            try { await RemoveSubjectsFromListAsync(listOlid, subjects); return true; }
            catch { return false; };
        }
        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<bool> TryAddSeedsToListAsync(string listOlid, params string[] seeds)
        {
            try { await AddSeedsToListAsync(listOlid, seeds); return true; }
            catch { return false; };
        }
        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<bool> TryRemoveSeedsFromListAsync(string listOlid, params string[] seeds)
        {
            try { await RemoveSeedsFromListAsync(listOlid, seeds); return true; }
            catch { return false; };
        }
        ///<inheritdoc/>
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