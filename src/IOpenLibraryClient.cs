using System.Formats.Asn1;
using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenLibraryNET;
using OpenLibraryNET.Data;
using OpenLibraryNET.Loader;
using OpenLibraryNET.Utility;

/// <summary>
/// The main interface to OpenLibrary.
/// </summary>
public interface IOpenLibraryClient
{
    /// <summary>
    /// Does this instance hold a session token?
    /// </summary>
    public bool LoggedIn { get; }
    /// <summary>
    /// The username of the currently logged in account. Null if not logged in.
    /// </summary>
    public string? Username { get; }

    /// <summary>
    /// Interface to the Works API
    /// </summary>
    public OLWorkLoader Work { get; }
    /// <summary>
    /// Interface to the Authors API
    /// </summary>
    public OLAuthorLoader Author { get; }
    /// <summary>
    /// Interface to the Edition/ISBN/Works API
    /// </summary>
    public OLEditionLoader Edition { get; }
    /// <summary>
    /// Interface to the Covers/AuthorPhotos API
    /// </summary>
    public OLImageLoader Image { get; }
    /// <summary>
    /// Interface to the Lists API
    /// </summary>
    public OLListLoader List { get; }
    /// <summary>
    /// Interface to the Search API
    /// </summary>
    public OLSearchLoader Search { get; }
    /// <summary>
    /// Interface to the Subjects API
    /// </summary>
    public OLSubjectLoader Subject { get; }
    /// <summary>
    /// Interface to the RecentChanges API
    /// </summary>
    public OLRecentChangesLoader RecentChanges { get; }
    /// <summary>
    /// Interface to the Partner API
    /// </summary>
    public OLPartnerLoader Partner { get; }
    /// <summary>
    /// Interface to the MyBooks API
    /// </summary>
    public OLMyBooksLoader MyBooks { get; }

    /// <summary>
    /// <see cref="HttpClient"/> instance created by this OpenLibraryClient instance.
    /// </summary>
    public HttpClient BackingClient { get; }

    /// <summary>
    /// Attempt to login to an OpenLibrary account.
    /// </summary>
    /// <param name="email">The email of the account to login with</param>
    /// <param name="password">The password of the account to login with</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<bool> TryLoginAsync(string email, string password);
    /// <summary>
    /// Login to an OpenLibrary account.
    /// </summary>
    /// <param name="email">The email of the account to login with</param>
    /// <param name="password">The password of the account to login with</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task LoginAsync(string email, string password);
    /// <summary>
    /// Attempt to logout of the currently logged in OpenLibrary account.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<bool> TryLogoutAsync();
    /// <summary>
    /// Logout of the currently logged in OpenLibrary account.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task LogoutAsync();

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
    public Task<OLWork> GetWorkAsync(string id, int editionsCount = 0);

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
    public Task<OLAuthor> GetAuthorAsync(string id, int worksCount = 0);

    /// <summary>
    /// Get an edition.
    /// </summary>
    /// <param name="id">The id of the edition. Should be in bibkey format</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task<OLEdition> GetEditionAsync(string id);
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
    public Task<OLEdition> GetEditionAsync(string id, BookIdType? idType = null, ImageSize? coverSize = null);
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
    public Task<OLEdition> GetEditionAsync(string id, string? idType = null, string? coverSize = null);

    /// <summary>
    /// Attempt to create a new list.
    /// </summary>
    /// <param name="listName">Name of the new list</param>
    /// <param name="listDescription">Description of the new list</param>
    /// <param name="listSeeds">Seeds to include in the new list</param>
    /// <param name="listTags">Tags for the new list</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<(bool, string)> TryCreateListAsync(string listName, string listDescription, IEnumerable<string>? listSeeds = null, IEnumerable<string>? listTags = null);
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
    public Task<string> CreateListAsync(string listName, string listDescription = "", IEnumerable<string>? listSeeds = null, IEnumerable<string>? listTags = null);

    /// <summary>
    /// Attempt to delete a list.
    /// </summary>
    /// <param name="listID">The id of the list to be deleted.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<bool> TryDeleteListAsync(string listID);
    /// <summary>
    /// Delete a list.
    /// </summary>
    /// <param name="listID">The ID of the list to be deleted.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task DeleteListAsync(string listID);

    /// <summary>
    /// Attempt to add editions to a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="editionOlids">The OLIDs of the editions.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<bool> TryAddEditionsToListAsync(string listOlid, params string[] editionOlids);
    /// <summary>
    /// Add editions to a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="editionOlids">The OLIDs of the editions.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task AddEditionsToListAsync(string listOlid, params string[] editionOlids);

    /// <summary>
    /// Attempt to remove editions from a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="editionOlids">The OLIDs of the editions.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<bool> TryRemoveEditionsFromListAsync(string listOlid, params string[] editionOlids);

    /// <summary>
    /// Remove editions from a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="editionOlids">The OLIDs of the editions.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task RemoveEditionsFromListAsync(string listOlid, params string[] editionOlids);

    /// <summary>
    /// Attempt to add works to a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="workOlids">The OLIDs of the works.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<bool> TryAddWorksToListAsync(string listOlid, params string[] workOlids);
    /// <summary>
    /// Add works to a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="workOlids">The OLIDs of the works.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task AddWorksToListAsync(string listOlid, params string[] workOlids);

    /// <summary>
    /// Attempt to remove works from a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="workOlids">The OLIDs of the works.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<bool> TryRemoveWorksFromListAsync(string listOlid, params string[] workOlids);
    /// <summary>
    /// Remove works from a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="workOlids">The OLIDs of the works.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task RemoveWorksFromListAsync(string listOlid, params string[] workOlids);

    /// <summary>
    /// Attempt to add subjects to a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="subjects">The subjects.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<bool> TryAddSubjectsToListAsync(string listOlid, params string[] subjects);
    /// <summary>
    /// Add subjects to a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="subjects">The subjects.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task AddSubjectsToListAsync(string listOlid, params string[] subjects);

    /// <summary>
    /// Attempt to remove subjects from a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="subjects">The subjects.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<bool> TryRemoveSubjectsFromListAsync(string listOlid, params string[] subjects);
    /// <summary>
    /// Remove subjects from a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="subjects">The subjects.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task RemoveSubjectsFromListAsync(string listOlid, params string[] subjects);

    /// <summary>
    /// Add seeds to a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="seeds">The seeds. Must be in key format, e.g. "/works/OL123W", "/subjects/place:los_angeles"</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task<bool> TryAddSeedsToListAsync(string listOlid, params string[] seeds);
    /// <summary>
    /// Attempt to add seeds to a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="seeds">The seeds. Must be in key format, e.g. "/works/OL123W", "/subjects/place:los_angeles"</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task AddSeedsToListAsync(string listOlid, params string[] seeds);

    /// <summary>
    /// Attempt to remove seeds from a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="seeds">The seeds. Must be in key format, e.g. "/works/OL123W", "/subjects/place:los_angeles"</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public Task<bool> TryRemoveSeedsFromListAsync(string listOlid, params string[] seeds);
    /// <summary>
    /// Remove seeds from a list.
    /// </summary>
    /// <param name="listOlid">The OLID of the list.</param>
    /// <param name="seeds">The seeds. Must be in key format, e.g. "/works/OL123W", "/subjects/place:los_angeles"</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    /// <exception cref="System.Net.Http.HttpRequestException"></exception>
    /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
    public Task RemoveSeedsFromListAsync(string listOlid, params string[] seeds);
}