﻿using OpenLibraryNET.Data;
using OpenLibraryNET.Utility;

namespace OpenLibraryNET.Loader
{
    /// <summary>
    /// Interface to OpenLibrary's MyBooks API.
    /// </summary>
    public class OLMyBooksLoader
    {
        internal OLMyBooksLoader(HttpClient client) => _client = client;

        private readonly HttpClient _client;


        /// <summary>
        /// Attempt to get data about a user's Want-To-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLMyBooksData?)> TryGetWantToReadAsync(string username)
        {
            try { return (true, await GetWantToReadAsync(username)); }
            catch { return (false, null); }
        }

        /// <summary>
        /// Get data about a user's Want-To-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLMyBooksData?> GetWantToReadAsync(string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLMyBooksData?>
            (
                _client,
                OpenLibraryUtility.BuildMyBooksUri(username, "want-to-read")
            );
        }

        /// <summary>
        /// Attempt to get data about a user's Currently-Reading reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLMyBooksData?)> TryGetCurrentlyReadingAsync(string username)
        {
            try { return (true, await GetCurrentlyReadingAsync(username)); }
            catch { return (false, null); }
        }

        /// <summary>
        /// Get data about a user's Currently-Reading reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLMyBooksData?> GetCurrentlyReadingAsync(string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLMyBooksData?>
            (
                _client,
                OpenLibraryUtility.BuildMyBooksUri(username, "currently-reading")
            );
        }

        /// <summary>
        /// Attempt to get data about a user's Already-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<(bool, OLMyBooksData?)> TryGetAlreadyReadAsync(string username)
        {
            try { return (true, await GetAlreadyReadAsync(username)); }
            catch { return (false, null); }
        }

        /// <summary>
        /// Get data about a user's Already-Read reading log.
        /// </summary>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async Task<OLMyBooksData?> GetAlreadyReadAsync(string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLMyBooksData?>
            (
                _client,
                OpenLibraryUtility.BuildMyBooksUri(username, "already-read")
            );
        }



        /// <summary>
        /// Attempt to get data about a user's Want-To-Read reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLMyBooksData?)> TryGetWantToReadAsync(HttpClient client, string username)
        {
            try { return (true, await GetWantToReadAsync(client, username)); }
            catch { return (false, null); }
        }

        /// <summary>
        /// Get data about a user's Want-To-Read reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLMyBooksData?> GetWantToReadAsync(HttpClient client, string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLMyBooksData?>
            (
                client,
                OpenLibraryUtility.BuildMyBooksUri(username, "want-to-read")
            );
        }

        /// <summary>
        /// Attempt to get data about a user's Currently-Reading reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLMyBooksData?)> TryGetCurrentlyReadingAsync(HttpClient client, string username)
        {
            try { return (true, await GetCurrentlyReadingAsync(client, username)); }
            catch { return (false, null); }
        }

        /// <summary>
        /// Get data about a user's Currently-Reading reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLMyBooksData?> GetCurrentlyReadingAsync(HttpClient client, string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLMyBooksData?>
            (
                client,
                OpenLibraryUtility.BuildMyBooksUri(username, "currently-reading")
            );
        }

        /// <summary>
        /// Attempt to get data about a user's Already-Read reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async static Task<(bool, OLMyBooksData?)> TryGetAlreadyReadAsync(HttpClient client, string username)
        {
            try { return (true, await GetAlreadyReadAsync(client, username)); }
            catch { return (false, null); }
        }

        /// <summary>
        /// Get data about a user's Already-Read reading log.
        /// </summary>
        /// <param name="client">An HttpClient instance which will be used to make the request.</param>
        /// <param name="username">The user to get the reading log of.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public async static Task<OLMyBooksData?> GetAlreadyReadAsync(HttpClient client, string username)
        {
            return await OpenLibraryUtility.LoadAsync<OLMyBooksData?>
            (
                client,
                OpenLibraryUtility.BuildMyBooksUri(username, "already-read")
            );
        }
    }
}